using RoR2;
using RoR2.CharacterAI;
using R2API;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SuperCyanTweaks
{
    public class Geep
    {
        public static CharacterSpawnCard cscGeep = Addressables.LoadAssetAsync<CharacterSpawnCard>("RoR2/DLC1/Gup/cscGeepBody.asset").WaitForCompletion();

        public Geep()
        {
            // Adjust director credit cost
            if (Configs.geepCost.Value >= 0)
            {
                cscGeep.directorCreditCost = Configs.geepCost.Value;
            }
        }
    }
}
