using RoR2;
using R2API;
using MonoMod.Cil;

namespace SuperCyanTweaks
{
    public class HuntersHarpoon
    {
        public HuntersHarpoon()
        {
            bool updated = false;
            float baseDuration = 1f;
            float stackDuration = .5f;

            // Configurable barrier gain
            if (Configs.harpoonDurationBase.Value >= 0 || Configs.harpoonDurationStack.Value >= 0)
            {
                if (Configs.harpoonDurationBase.Value >= 0)
                    baseDuration = Configs.harpoonDurationBase.Value;

                if (Configs.harpoonDurationStack.Value >= 0)
                    stackDuration = Configs.harpoonDurationStack.Value;

                bool hookFailed = true;
                IL.RoR2.GlobalEventManager.OnCharacterDeath += (il) =>
                {
                    ILCursor c = new(il);

                    if (
                        c.TryGotoNext(MoveType.After,
                        x => x.MatchLdsfld(typeof(DLC1Content.Buffs), nameof(DLC1Content.Buffs.KillMoveSpeed))) &&
                        c.TryGotoPrev(MoveType.Before,
                        x => x.MatchLdcR4(1f))
                    )
                    {
                        if (Configs.harpoonDurationBase.Value >= 0)
                        {
                            c.Next.Operand = Configs.harpoonDurationBase.Value;
                        }

                        c.Index += 3;

                        if (Configs.harpoonDurationStack.Value >= 0)
                        {
                            c.Next.Operand = Configs.harpoonDurationStack.Value;
                        }

                        hookFailed = false;
                        updated = true;
                    }

                    if (hookFailed == true)
                    {
                        Log.Error("Hunter's Harpoon duration hook failed!");
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

            LanguageAPI.Add("ITEM_MOVESPEEDONKILL_DESC",
                $"Killing an enemy increases <style=cIsUtility>movement speed</style> by <style=cIsUtility>125%</style>, fading over <style=cIsUtility>{baseDurationString}</style> <style=cStack>(+{stackDurationString} per stack)</style> seconds.",
                "en"
            );

            LanguageAPI.Add("ITEM_MOVESPEEDONKILL_DESC",
                baseDuration == 1f ?
                $"Tuer un ennemi améliore la <style=cIsUtility>vitesse de déplacement</style> de <style=cIsUtility>125 %</style>. L'effet disparaît au bout d'<style=cIsUtility>{baseDuration}</style> seconde <style=cStack>(+{stackDuration} par cumul)</style>." : // If base duration is 1s
                $"Tuer un ennemi améliore la <style=cIsUtility>vitesse de déplacement</style> de <style=cIsUtility>125 %</style>. L'effet disparaît au bout de <style=cIsUtility>{baseDuration}</style> secondes <style=cStack>(+{stackDuration} par cumul)</style>.", // Else...
                "fr"
            );
        }
    }
}
