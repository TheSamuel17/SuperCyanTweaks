using RoR2;
using UnityEngine.AddressableAssets;

namespace SuperCyanTweaks
{
    public class SolusDistributor
    {
        public static CharacterSpawnCard cscMinePod = Addressables.LoadAssetAsync<CharacterSpawnCard>("RoR2/DLC3/MinePod/cscMinePod.asset").WaitForCompletion();

        public SolusDistributor()
        {
            // Adjust director credit cost
            if (Configs.distributorCost.Value >= 0)
            {
                cscMinePod.directorCreditCost = Configs.distributorCost.Value;
            }
        }
    }
}
