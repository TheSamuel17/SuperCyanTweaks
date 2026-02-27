using RoR2;
using RoR2.CharacterAI;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SuperCyanTweaks
{
    public class SolusProspector
    {
        public static EntityStateConfiguration drillDash = Addressables.LoadAssetAsync<EntityStateConfiguration>("RoR2/DLC3/WorkerUnit/EntityStates.WorkerUnit.FireDrillDash.asset").WaitForCompletion();
        public static GameObject prospectorMasterPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC3/WorkerUnit/WorkerUnitMaster.prefab").WaitForCompletion();

        public SolusProspector()
        {
            // Single hit attack
            if (Configs.prospectorSingleHit.Value == true)
            {
                drillDash.TryModifyFieldValue("baseAttackInterval", float.PositiveInfinity);
            }

            // Attack range
            if (Configs.prospectorAttackRange.Value >= 0)
            {
                AISkillDriver[] skillDrivers = prospectorMasterPrefab.GetComponents<AISkillDriver>();
                foreach (AISkillDriver skillDriver in skillDrivers)
                {
                    if (skillDriver.skillSlot == SkillSlot.Primary)
                    {
                        skillDriver.maxDistance = Configs.prospectorAttackRange.Value;
                    }
                }
            }
        }
    }
}
