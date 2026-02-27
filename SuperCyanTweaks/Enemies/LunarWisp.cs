using RoR2;
using UnityEngine.AddressableAssets;

namespace SuperCyanTweaks
{
    public class LunarWisp
    {
        public static CharacterSpawnCard cscLunarWisp = Addressables.LoadAssetAsync<CharacterSpawnCard>("RoR2/Base/LunarWisp/cscLunarWisp.asset").WaitForCompletion();
        public static int lunarWispBaseCost = cscLunarWisp.directorCreditCost;

        public LunarWisp()
        {            
            // Adjust director credit cost
            if (Configs.lunarWispCostOutsideMoon.Value >= 0 || Configs.lunarWispCostOnMoon.Value >= 0)
            {
                if (Configs.lunarWispCostOutsideMoon.Value == Configs.lunarWispCostOnMoon.Value)
                {
                    cscLunarWisp.directorCreditCost = Configs.lunarWispCostOutsideMoon.Value;
                }
                else
                {
                    Stage.onStageStartGlobal += OnStageStartGlobal;
                    RoR2Application.onLoad += () => lunarWispBaseCost = cscLunarWisp.directorCreditCost;
                }
            }
        }

        private void OnStageStartGlobal(Stage stage)
        {
            if (SceneInfo.instance.sceneDef == SceneCatalog.FindSceneDef("moon2") || SceneInfo.instance.sceneDef == SceneCatalog.FindSceneDef("moon"))
            {
                cscLunarWisp.directorCreditCost = Configs.lunarWispCostOnMoon.Value >= 0 ? Configs.lunarWispCostOnMoon.Value : lunarWispBaseCost;
            }
            else
            {
                cscLunarWisp.directorCreditCost = Configs.lunarWispCostOutsideMoon.Value >= 0 ? Configs.lunarWispCostOutsideMoon.Value : lunarWispBaseCost;
            }
        }
    }
}
