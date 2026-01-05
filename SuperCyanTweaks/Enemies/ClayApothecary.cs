using RoR2;
using RoR2.CharacterAI;
using R2API;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SuperCyanTweaks
{
    public class ClayApothecary
    {
        public static CharacterSpawnCard cscClayGrenadier = Addressables.LoadAssetAsync<CharacterSpawnCard>("RoR2/DLC1/ClayGrenadier/cscClayGrenadier.asset").WaitForCompletion();
        public static GameObject clayGrenadierMasterPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/ClayGrenadier/ClayGrenadierMaster.prefab").WaitForCompletion();

        public ClayApothecary()
        {
            // Adjust director credit cost
            if (Configs.clayApothecaryCost.Value >= 0)
            {
                cscClayGrenadier.directorCreditCost = Configs.clayApothecaryCost.Value;
            }

            // Adjust range on tar ball attack
            if (Configs.clayApothecaryTarBallRange.Value >= 0)
            {
                AISkillDriver[] skillDrivers = clayGrenadierMasterPrefab.GetComponents<AISkillDriver>();
                foreach (AISkillDriver skillDriver in skillDrivers)
                {
                    if (skillDriver.customName == "ThrowBarrel")
                    {
                        skillDriver.maxDistance = Configs.clayApothecaryTarBallRange.Value;
                    }
                }
            }
        }
    }
}
