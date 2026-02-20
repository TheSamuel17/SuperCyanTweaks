using RoR2;
using R2API;
using MonoMod.Cil;
using Mono.Cecil.Cil;
using System;

namespace SuperCyanTweaks
{
    public class GrowthNectar
    {
        public GrowthNectar() 
        {
            // Configurable buff count
            if (Configs.growthNectarBuffCount.Value >= 0)
            {
                bool hookFailed = true;
                IL.RoR2.CharacterBody.RecalculateStats += (il) =>
                {
                    ILCursor c = new(il);

                    if (
                        c.TryGotoNext(MoveType.Before,
                        x => x.MatchStfld(typeof(CharacterBody), nameof(CharacterBody.maxGrowthNectarBuffCount))) &&
                        c.TryGotoPrev(MoveType.Before,
                        x => x.MatchLdcI4(out _))
                    )
                    {
                        c.Remove();
                        c.Emit(OpCodes.Ldc_I4, Configs.growthNectarBuffCount.Value);

                        hookFailed = false;
                    }

                    if (hookFailed == true)
                    {
                        Log.Error("Growth Nectar buff count hook failed!");
                    }
                };

                if (hookFailed == false)
                {
                    UpdateDescription(Configs.growthNectarBuffCount.Value);
                }
            }
        }

        private void UpdateDescription(int buffCount)
        {
            string buffCountString = buffCount.ToString();

            LanguageAPI.Add("ITEM_BOOSTALLSTATS_DESC",
                $"Grants <style=cIsUtility>4%</style> increase to <style=cIsUtility>ALL stats</style> for each buff, up to a maximum of <style=cIsUtility>{buffCountString}</style> <style=cStack>(+{buffCountString} per stack)</style>.",
                "en"
            );

            LanguageAPI.Add("ITEM_BOOSTALLSTATS_DESC",
                $"Augmente <style=cIsUtility>TOUTES les statistiques</style> de <style=cIsUtility>4 %</style> pour chaque bonus, jusqu'à <style=cIsUtility>{buffCountString}</style> maximum <style=cStack>(+{buffCountString} par cumul)</style>.",
                "fr"
            );
        }
    }
}
