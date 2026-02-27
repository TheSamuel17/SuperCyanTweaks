using RoR2;
using UnityEngine.AddressableAssets;

namespace SuperCyanTweaks
{
    public class LunarExploder
    {
        public static CharacterSpawnCard cscLunarExploder = Addressables.LoadAssetAsync<CharacterSpawnCard>("RoR2/Base/LunarExploder/cscLunarExploder.asset").WaitForCompletion();

        public LunarExploder()
        {
            // Adjust director credit cost
            if (Configs.lunarExploderCost.Value >= 0)
            {
                cscLunarExploder.directorCreditCost = Configs.lunarExploderCost.Value;
            }
        }
    }
}
