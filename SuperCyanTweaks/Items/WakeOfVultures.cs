using RoR2;
using R2API;
using MonoMod.Cil;

namespace SuperCyanTweaks
{
    public class WakeOfVultures
    {
        public WakeOfVultures()
        {
            bool updated = false;
            float baseDuration = 8f;
            float stackDuration = 5f;

            // Configurable buff duration
            if (Configs.wakeOfVulturesDurationBase.Value >= 0 || Configs.wakeOfVulturesDurationStack.Value >= 0)
            {
                if (Configs.wakeOfVulturesDurationBase.Value >= 0) 
                    baseDuration = Configs.wakeOfVulturesDurationBase.Value;

                if (Configs.wakeOfVulturesDurationStack.Value >= 0)
                    stackDuration = Configs.wakeOfVulturesDurationStack.Value;

                bool hookFailed = true;
                IL.RoR2.GlobalEventManager.OnCharacterDeath += (il) =>
                {
                    ILCursor c = new(il);

                    if (
                        c.TryGotoNext(MoveType.After,
                        x => x.MatchLdsfld(typeof(RoR2Content.Items), nameof(RoR2Content.Items.HeadHunter))) &&
                        c.TryGotoNext(MoveType.Before,
                        x => x.MatchLdcR4(3f))
                    )
                    {
                        if (Configs.wakeOfVulturesDurationBase.Value >= 0)
                        {
                            c.Next.Operand = baseDuration - stackDuration;
                        }

                        c.Index++;

                        if (Configs.wakeOfVulturesDurationStack.Value >= 0)
                        {
                            c.Next.Operand = stackDuration;
                        }

                        hookFailed = false;
                        updated = true;
                    }

                    if (hookFailed == true)
                    {
                        Log.Error("Wake of Vultures hook failed!");
                    }
                };
            }

            if (updated == true)
            {
                UpdateDescription(baseDuration, stackDuration);
            }
        }

        private void UpdateDescription(float baseDuration, float stackDuration)
        {
            string baseDurationString = baseDuration.ToString().Replace(",", ".");
            string stackDurationString = stackDuration.ToString().Replace(",", ".");

            LanguageAPI.Add("ITEM_HEADHUNTER_DESC",
                $"Gain the <style=cIsDamage>power</style> of any killed elite monster for <style=cIsDamage>{baseDurationString}s</style> <style=cStack>(+{stackDurationString}s per stack)</style>.",
                "en"
            );
        }
    }
}
