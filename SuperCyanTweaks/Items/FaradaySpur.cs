using Mono.Cecil.Cil;
using MonoMod.Cil;
using RoR2;
using R2API;
using System;
using UnityEngine;

namespace SuperCyanTweaks
{
    public class FaradaySpur
    {
        public FaradaySpur()
        {
            // Configurable speed
            if (Configs.faradaySpurMaxSpeed.Value >= 0)
            {
                IL.RoR2.CharacterBody.RecalculateStats += (il) =>
                {
                    ILCursor c = new(il);
                    bool hookFailed = true;

                    if (
                        c.TryGotoNext(MoveType.Before,
                        x => x.MatchLdsfld(typeof(RoR2.Items.JumpDamageStrikeBodyBehavior), nameof(RoR2.Items.JumpDamageStrikeBodyBehavior.MoveSpeedVelocityPerCharge)))
                    )
                    {
                        c.Remove();
                        c.Emit(OpCodes.Ldc_R4, Configs.faradaySpurMaxSpeed.Value / 10000f);
                        hookFailed = false;
                    }

                    if (hookFailed == true)
                    {
                        Log.Error("Faraday Spur speed hook failed!");
                    }
                    else
                    {
                        UpdateDescription(Configs.faradaySpurMaxSpeed.Value);
                    }
                };
            }

            // Configurable charge rate
            if (Configs.faradaySpurChargeRate.Value >= 0)
            {
                IL.RoR2.Items.JumpDamageStrikeBodyBehavior.UpdateCharge += (il) =>
                {
                    ILCursor c = new(il);
                    bool hookFailed = true;

                    if (
                        c.TryGotoNext(MoveType.After,
                        x => x.MatchLdsfld(typeof(RoR2.Items.JumpDamageStrikeBodyBehavior), nameof(RoR2.Items.JumpDamageStrikeBodyBehavior.minDistancePerCharge)))
                    )
                    {
                        c.Emit(OpCodes.Ldc_R4, Configs.faradaySpurChargeRate.Value);
                        c.Emit(OpCodes.Div);

                        if (
                            c.TryGotoNext(MoveType.After,
                            x => x.MatchLdsfld(typeof(RoR2.Items.JumpDamageStrikeBodyBehavior), nameof(RoR2.Items.JumpDamageStrikeBodyBehavior.maxDistancePerCharge)))
                        )
                        {
                            c.Emit(OpCodes.Ldc_R4, Configs.faradaySpurChargeRate.Value);
                            c.Emit(OpCodes.Div);

                            hookFailed = false;
                        }
                    }

                    if (hookFailed == true)
                    {
                        Log.Error("Faraday Spur charge rate multiplier hook failed!");
                    }
                };
            }

            // Charge rate fix
            if (Configs.faradaySpurChargeFix.Value == true)
            {
                IL.RoR2.Items.JumpDamageStrikeBodyBehavior.UpdateCharge += (il) =>
                {
                    ILCursor c = new(il);
                    bool hookFailed = true;

                    if (
                        c.TryGotoNext(MoveType.Before,
                        x => x.MatchCallOrCallvirt("UnityEngine.Time", "get_deltaTime"))
                    )
                    {
                        c.Remove();
                        c.EmitDelegate<Func<float>>(() =>
                        {
                            return Time.fixedDeltaTime;
                        });

                        hookFailed = false;
                    }

                    if (hookFailed == true)
                    {
                        Log.Error("Faraday Spur charge rate fix hook failed!");
                    }
                };
            }

            // Anti-cratering
            if (Configs.faradaySpurAntiCrater.Value == true)
            {
                On.RoR2.Items.JumpDamageStrikeBodyBehavior.DischargeEffects += TriggerChargeJump;
                On.RoR2.GlobalEventManager.OnCharacterHitGroundServer += RegisterFallDmg;
            }
        }

        private void RegisterFallDmg(On.RoR2.GlobalEventManager.orig_OnCharacterHitGroundServer orig, GlobalEventManager self, CharacterBody characterBody, CharacterMotor.HitGroundInfo hitGroundInfo)
        {
            if (characterBody)
            {
                var component = characterBody.GetComponent<FaradaySpurFallDmgResistance>();
                if (component)
                {
                    hitGroundInfo.velocity.y = Mathf.Min(hitGroundInfo.velocity.y + (component.fallDmgResistance - characterBody.jumpPower), 0f);
                }
            }

            orig(self, characterBody, hitGroundInfo);
        }

        private void TriggerChargeJump(On.RoR2.Items.JumpDamageStrikeBodyBehavior.orig_DischargeEffects orig, RoR2.Items.JumpDamageStrikeBodyBehavior self)
        {
            int buffCount = self.body.GetBuffCount(DLC3Content.Buffs.JumpDamageStrikeCharge);
            if (buffCount >= 25)
            {
                var component = self.body.gameObject.AddComponent<FaradaySpurFallDmgResistance>();
                component.body = self.body;
                component.fallDmgResistance = self.body.jumpPower;
            }

            orig(self);
        }

        private void UpdateDescription(float maxSpeed)
        {
            string maxSpeedString = maxSpeed.ToString().Replace(",", ".");

            LanguageAPI.Add("ITEM_JUMPDAMAGESTRIKE_DESC",
                $"Moving around builds up <style=cIsUtility>charge</style>, granting up to <style=cIsUtility>+{maxSpeedString}% movement speed</style> and <style=cIsUtility>+200% jump height</style> at 100%. " +
                $"At 25% charge or higher, jumping triggers an <style=cIsDamage>explosive discharge</style> for <style=cIsDamage>400%</style> <style=cStack>(+280% per stack)</style> <style=cIsDamage>damage</style> in a 5m to 32.3m <style=cStack>(+7.5m per stack)</style> area.",
                "en"
            );

            LanguageAPI.Add("ITEM_JUMPDAMAGESTRIKE_DESC",
                $"Se déplacer accumule de la <style=cIsUtility>charge</style> qui octroie jusqu'à <style=cIsUtility>+{maxSpeed} % de vitesse de déplacement</style> et <style=cIsUtility>+200 % de hauteur de saut</style> à 100 %. " +
                $"Si vous avez au moins 25% de charge, sauter déclenche une <style=cIsDamage>décharge explosive</style> qui inflige <style=cIsDamage>400 % de dégâts</style> <style=cStack>(+280 % de dégâts par cumul)</style> dans une zone de 5 à 32,3 m <style=cStack>(+7,5 m par cumul)</style>.",
                "fr"
            );
        }
    }

    public class FaradaySpurFallDmgResistance : MonoBehaviour
    {
        public CharacterBody body;
        public float fallDmgResistance;

        private void FixedUpdate()
        {
            if (!body) Destroy(this);
            if (!body.characterMotor) Destroy(this);

            if (body.characterMotor.isGrounded)
            {
                Destroy(this);
            }
        }
    }
}
