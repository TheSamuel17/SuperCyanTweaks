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
        public static EntityStateConfiguration clayGrenadierFaceSlam = Addressables.LoadAssetAsync<EntityStateConfiguration>("RoR2/DLC1/ClayGrenadier/EntityStates.ClayGrenadier.FaceSlam.asset").WaitForCompletion();

        public ClayApothecary()
        {
            // Adjust director credit cost
            if (Configs.clayApothecaryCost.Value >= 0)
            {
                cscClayGrenadier.directorCreditCost = Configs.clayApothecaryCost.Value;
            }

            // Adjustments on tar ball & ranged mortar attacks
            if (Configs.clayApothecaryTarBallRange.Value >= 0 || Configs.clayApothecaryMortarHealthThreshold.Value >= 0)
            {
                AISkillDriver[] skillDrivers = clayGrenadierMasterPrefab.GetComponents<AISkillDriver>();
                foreach (AISkillDriver skillDriver in skillDrivers)
                {
                    if (skillDriver.customName == "ThrowBarrel" && Configs.clayApothecaryTarBallRange.Value >= 0)
                    {
                        skillDriver.maxDistance = Configs.clayApothecaryTarBallRange.Value;
                    }
                    else if (skillDriver.customName == "LongDistanceMortar" && Configs.clayApothecaryMortarHealthThreshold.Value >= 0)
                    {
                        if (Configs.clayApothecaryMortarHealthThreshold.Value >= 1)
                        {
                            skillDriver.maxUserHealthFraction = float.PositiveInfinity;
                        }
                        else
                        {
                            skillDriver.maxUserHealthFraction = Configs.clayApothecaryMortarHealthThreshold.Value;
                        }
                    }
                }
            }

            // Slam/Mortar self-damage
            if (Configs.clayApothecarySlamSelfDmg.Value >= 0)
            {                
                clayGrenadierFaceSlam.TryModifyFieldValue("healthCostFraction", Configs.clayApothecarySlamSelfDmg.Value / 100f);
            }
        }
    }
}
