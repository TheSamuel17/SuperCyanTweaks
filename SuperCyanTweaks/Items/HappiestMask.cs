using RoR2;
using R2API;
using MonoMod.Cil;

namespace SuperCyanTweaks
{
    public class HappiestMask
    {
        public HappiestMask()
        {
            // Configurable proc chance
            if (Configs.happiestMaskProcChance.Value >= 0)
            {
                bool hookFailed = true;
                IL.RoR2.GlobalEventManager.OnCharacterDeath += (il) =>
                {
                    ILCursor c = new(il);

                    if (
                        c.TryGotoNext(MoveType.After,
                        x => x.MatchLdsfld(typeof(RoR2Content.Items), nameof(RoR2Content.Items.GhostOnKill))) &&
                        c.TryGotoNext(MoveType.Before,
                        x => x.MatchLdcR4(7f))
                    )
                    {
                        c.Next.Operand = Configs.happiestMaskProcChance.Value;
                        hookFailed = false;
                    }

                    if (hookFailed == true)
                    {
                        Log.Error("Happiest Mask Proc Chance hook failed!");
                    }
                };

                if (hookFailed == false)
                {
                    UpdateDescription(Configs.happiestMaskProcChance.Value);
                }
            }
        }

        private void UpdateDescription(float procChance)
        {
            string procChanceString = procChance.ToString().Replace(",", ".");

            LanguageAPI.Add("ITEM_GHOSTONKILL_DESC",
                $"Killing enemies has a <style=cIsDamage>{procChanceString}%</style> chance to <style=cIsDamage>spawn a ghost</style> of the killed enemy with <style=cIsDamage>1500%</style> damage. " +
                $"Lasts <style=cIsDamage>30s</style> <style=cStack>(+30s per stack)</style>.",
                "en"
            );
        }
    }
}
