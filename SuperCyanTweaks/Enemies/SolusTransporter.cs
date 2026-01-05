using RoR2;
using RoR2.CharacterAI;
using R2API;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SuperCyanTweaks
{
    public class SolusTransporter
    {
        public static CharacterSpawnCard cscIronHauler = Addressables.LoadAssetAsync<CharacterSpawnCard>("RoR2/DLC3/IronHauler/cscIronHauler.asset").WaitForCompletion();

        public SolusTransporter()
        {
            // Adjust director credit cost
            if (Configs.transporterCost.Value >= 0)
            {
                cscIronHauler.directorCreditCost = Configs.transporterCost.Value;
            }
        }
    }
}
