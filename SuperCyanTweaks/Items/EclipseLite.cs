using RoR2;
using R2API;
using MonoMod.Cil;
using System;

namespace SuperCyanTweaks
{
    public class EclipseLite
    {
        public EclipseLite()
        {
            bool updated = false;
            float basePercentage = 1f;
            float stackPercentage = .25f;

            // Configurable barrier gain
            if (Configs.eclipseLiteBarrierBase.Value >= 0 || Configs.eclipseLiteBarrierStack.Value >= 0)
            {
                if (Configs.eclipseLiteBarrierBase.Value >= 0)
                    basePercentage = Configs.eclipseLiteBarrierBase.Value;

                if (Configs.eclipseLiteBarrierStack.Value >= 0)
                    stackPercentage = Configs.eclipseLiteBarrierStack.Value;

                bool hookFailed = true;
                IL.RoR2.CharacterBody.OnSkillCooldown += (il) =>
                {
                    ILCursor c = new(il);

                    if (
                        c.TryGotoNext(MoveType.After,
                        x => x.MatchLdsfld(typeof(DLC3Content.Items), nameof(DLC3Content.Items.BarrierOnCooldown))) &&
                        c.TryGotoNext(MoveType.Before,
                        x => x.MatchLdcR4(0.01f))
                    )
                    {
                        if (Configs.eclipseLiteBarrierBase.Value >= 0)
                        {
                            c.Next.Operand = Configs.eclipseLiteBarrierBase.Value / 100;
                        }

                        c.Index++;

                        if (Configs.eclipseLiteBarrierStack.Value >= 0)
                        {
                            c.Next.Operand = Configs.eclipseLiteBarrierStack.Value / 100;
                        }

                        hookFailed = false;
                        updated = true;
                    }

                    if (hookFailed == true)
                    {
                        Log.Error("Eclipse Lite barrier gain hook failed!");
                    }
                };
            }

            // Count shields for barrier gain
            if (Configs.eclipseLiteCountShields.Value == true)
            {
                bool hook2Failed = true;
                IL.RoR2.CharacterBody.OnSkillCooldown += (il) =>
                {
                    ILCursor c = new(il);

                    if (
                        c.TryGotoNext(MoveType.Before,
                        x => x.MatchCallOrCallvirt<CharacterBody>("get_maxHealth"))
                    )
                    {
                        c.Remove();
                        c.EmitDelegate<Func<CharacterBody, float>>((body) =>
                        {
                            if (body.healthComponent)
                            {
                                return body.healthComponent.fullCombinedHealth;
                            }
                            else
                            {
                                return body.maxHealth;
                            }
                        });

                        hook2Failed = false;
                    }

                    if (hook2Failed == true)
                    {
                        Log.Error("Eclipse Lite shield hook failed!");
                    }
                };
            }

            if (updated == true)
            {
                UpdateDescription(basePercentage, stackPercentage);
            }
        }

        private void UpdateDescription(float baseBarrier, float stackBarrier)
        {
            string baseBarrierString = baseBarrier.ToString().Replace(",", ".");
            string stackBarrierString = stackBarrier.ToString().Replace(",", ".");

            LanguageAPI.Add("ITEM_BARRIERONCOOLDOWN_DESC",
                $"When a skill comes off cooldown, gain a <style=cIsDamage>temporary barrier</style> for <style=cIsHealing>{baseBarrierString}%</style> <style=cStack>(+{stackBarrierString}% per stack)</style> of your maximum health per second of the skill's base cooldown.",
                "en"
            );
        }
    }
}
