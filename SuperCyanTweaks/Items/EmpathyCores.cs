using Mono.Cecil.Cil;
using MonoMod.Cil;
using R2API;
using RoR2;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SuperCyanTweaks
{
    public class EmpathyCores
    {
        // Load addressables
        public static GameObject roboBallBuddyRedPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/RoboBallBuddy/RoboBallRedBuddyBody.prefab").WaitForCompletion();
        public static GameObject roboBallBuddyGreenPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/RoboBallBuddy/RoboBallGreenBuddyBody.prefab").WaitForCompletion();
        public static ItemDef teamSizeDamageBonus = Addressables.LoadAssetAsync<ItemDef>("RoR2/Base/TeamSizeDamageBonus/TeamSizeDamageBonus.asset").WaitForCompletion();

        public EmpathyCores()
        {
            // Damage tweak
            if (Configs.empathyCoresDamageTweak.Value == true)
            {
                // Disable the vanilla effect
                bool hookFailed = true;
                IL.RoR2.CharacterBody.RecalculateStats += (il) =>
                {
                    ILCursor c = new(il);

                    if (
                        c.TryGotoNext(MoveType.Before,
                        x => x.MatchLdsfld(typeof(RoR2Content.Items), nameof(RoR2Content.Items.TeamSizeDamageBonus))
                    ))
                    {
                        c.Remove();
                        c.Emit<Main>(OpCodes.Ldsfld, nameof(Main.emptyItemDef));
                        hookFailed = false;
                    }

                    if (hookFailed == true)
                    {
                        Log.Error("Empathy Cores damage tweak hook failed!");
                    }
                };

                // New stuff
                if (hookFailed == false)
                {
                    RecalculateStatsAPI.GetStatCoefficients += GetStatCoefficients;
                    On.EntityStates.RoboBallMini.Weapon.FireEyeBeam.ModifyBullet += FireEyeBeam;
                }
            }
        }

        private void GetStatCoefficients(CharacterBody body, RecalculateStatsAPI.StatHookEventArgs args)
        {
            if (body && (BodyCatalog.GetBodyPrefab(body.bodyIndex) == roboBallBuddyRedPrefab || BodyCatalog.GetBodyPrefab(body.bodyIndex) == roboBallBuddyGreenPrefab))
            {
                if (body.inventory)
                {
                    int itemCount = body.inventory.GetItemCountEffective(teamSizeDamageBonus);
                    if (itemCount > 0)
                    {
                        int additionalMembers = Math.Max(TeamComponent.GetTeamMembers(body.teamComponent.teamIndex).Count - 1, 0);
                        float damageMultiplier = 1f + (float)(additionalMembers * itemCount);

                        var dmgMultComponent = body.GetComponent<ProbeLaserDmgMultiplier>();
                        if (dmgMultComponent)
                        {
                            dmgMultComponent.multiplier = damageMultiplier;
                        } else
                        {
                            dmgMultComponent = body.gameObject.AddComponent<ProbeLaserDmgMultiplier>();
                            dmgMultComponent.multiplier = damageMultiplier;
                        }
                    }
                }
            }
        }

        private void FireEyeBeam(On.EntityStates.RoboBallMini.Weapon.FireEyeBeam.orig_ModifyBullet orig, EntityStates.RoboBallMini.Weapon.FireEyeBeam self, BulletAttack bulletAttack)
        {
            if (self.characterBody)
            {
                var dmgMultComponent = self.characterBody.GetComponent<ProbeLaserDmgMultiplier>();
                if (dmgMultComponent)
                {
                    bulletAttack.damage *= dmgMultComponent.multiplier;
                }
            }

            orig(self, bulletAttack);
        }
    }

    public class ProbeLaserDmgMultiplier : MonoBehaviour
    {
        public float multiplier = 1f;
    }
}
