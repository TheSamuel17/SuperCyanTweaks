using RoR2;
using R2API;
using MonoMod.Cil;

namespace SuperCyanTweaks
{
    public class EclipseLite
    {
        public EclipseLite()
        {
            bool updated = false;
            float basePercentage = 1f;
            float stackPercentage = .25f;

            // Configurable barrier gain (first stack)
            if (Configs.eclipseLiteBarrierBase.Value >= 0)
            {
                bool hookFailed = true;
                IL.RoR2.CharacterBody.OnSkillCooldown += (il) =>
                {
                    ILCursor c = new(il);

                    if (
                        c.TryGotoNext(MoveType.After,
                        x => x.MatchLdsfld(typeof(DLC3Content.Items), nameof(DLC3Content.Items.BarrierOnCooldown))
                    ))
                    {
                        if (
                            c.TryGotoNext(MoveType.Before,
                            x => x.MatchLdcR4(0.01f)
                        ))
                        {
                            c.Next.Operand = Configs.eclipseLiteBarrierBase.Value / 100;
                            hookFailed = false;
                            updated = true;
                            basePercentage = Configs.eclipseLiteBarrierBase.Value;
                        }
                    }

                    if (hookFailed == true)
                    {
                        Log.Error("Eclipse Lite (first stack) hook failed!");
                    }
                };
            }

            // Configurable barrier gain (subsequent stacks)
            if (Configs.eclipseLiteBarrierStack.Value >= 0)
            {
                bool hookFailed = true;
                IL.RoR2.CharacterBody.OnSkillCooldown += (il) =>
                {
                    ILCursor c = new(il);

                    if (
                        c.TryGotoNext(MoveType.After,
                        x => x.MatchLdsfld(typeof(DLC3Content.Items), nameof(DLC3Content.Items.BarrierOnCooldown))
                    ))
                    {
                        if (
                            c.TryGotoNext(MoveType.Before,
                            x => x.MatchLdcR4(0.0025f)
                        ))
                        {
                            c.Next.Operand = Configs.eclipseLiteBarrierStack.Value / 100;
                            hookFailed = false;
                            updated = true;
                            stackPercentage = Configs.eclipseLiteBarrierStack.Value;
                        }
                    }

                    if (hookFailed == true)
                    {
                        Log.Error("Eclipse Lite (subsequent stacks) hook failed!");
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
