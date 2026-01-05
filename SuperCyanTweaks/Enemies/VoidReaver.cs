using RoR2;
using UnityEngine.AddressableAssets;

namespace SuperCyanTweaks
{
    public class VoidReaver
    {
        public static CharacterSpawnCard cscNullifier = Addressables.LoadAssetAsync<CharacterSpawnCard>("RoR2/Base/Nullifier/cscNullifier.asset").WaitForCompletion();

        public VoidReaver()
        {
            // Adjust director credit cost
            if (Configs.voidReaverCost.Value >= 0)
            {
                cscNullifier.directorCreditCost = Configs.voidReaverCost.Value;
            }
        }
    }
}
