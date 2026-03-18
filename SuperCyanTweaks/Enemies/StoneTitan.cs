using RoR2;
using RoR2.CharacterAI;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SuperCyanTweaks
{
    public class StoneTitan
    {
        public static GameObject titanMasterPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Titan/TitanMaster.prefab").WaitForCompletion();
        public static GameObject titanGoldMasterPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Titan/TitanGoldMaster.prefab").WaitForCompletion();
        public static EntityStateConfiguration titanFist = Addressables.LoadAssetAsync<EntityStateConfiguration>("RoR2/Base/Titan/EntityStates.TitanMonster.FireFist.asset").WaitForCompletion();
        public static EntityStateConfiguration titanSummonRock = Addressables.LoadAssetAsync<EntityStateConfiguration>("RoR2/Base/Titan/EntityStates.TitanMonster.RechargeRocks.asset").WaitForCompletion();

        public StoneTitan()
        {
            // Modify skill drivers
            if (Configs.titanLaserMaxRange.Value >= 0 || Configs.titanLaserMinRange.Value >= 0)
            {
                AISkillDriver[] skillDrivers = titanMasterPrefab.GetComponents<AISkillDriver>();
                foreach (AISkillDriver skillDriver in skillDrivers)
                {
                    AdjustSkillDrivers(skillDriver);
                }

                skillDrivers = titanGoldMasterPrefab.GetComponents<AISkillDriver>();
                foreach (AISkillDriver skillDriver in skillDrivers)
                {
                    AdjustSkillDrivers(skillDriver);
                }
            }

            // Adjust fist endlag
            if (Configs.titanFistEndlag.Value >= 0)
            {
                titanFist.TryModifyFieldValue("exitDuration", Configs.titanFistEndlag.Value);
            }

            // Adjust rock summon duration
            if (Configs.titanSummonDuration.Value >= 0)
            {
                titanSummonRock.TryModifyFieldValue("baseDuration", Configs.titanSummonDuration.Value);
            }
        }

        private void AdjustSkillDrivers(AISkillDriver skillDriver)
        {
            if (skillDriver.skillSlot == SkillSlot.Special)
            {
                skillDriver.maxDistance = Configs.titanLaserMaxRange.Value >= 0 ? Configs.titanLaserMaxRange.Value : skillDriver.maxDistance;
                skillDriver.minDistance = Configs.titanLaserMinRange.Value >= 0 ? Configs.titanLaserMinRange.Value : skillDriver.minDistance;
            }
        }
    }
}
