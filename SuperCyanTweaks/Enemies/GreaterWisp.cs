using RoR2;
using RoR2.CharacterAI;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SuperCyanTweaks
{
    public class GreaterWisp
    {
        public static GameObject greaterWispMasterPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/GreaterWisp/GreaterWispMaster.prefab").WaitForCompletion();

        public GreaterWisp()
        {
            // Attack range
            if (Configs.greaterWispAttackRange.Value >= 0)
            {
                AISkillDriver[] skillDrivers = greaterWispMasterPrefab.GetComponents<AISkillDriver>();
                foreach (AISkillDriver skillDriver in skillDrivers)
                {
                    if (skillDriver.skillSlot == SkillSlot.Primary)
                    {
                        if (skillDriver.movementType == AISkillDriver.MovementType.FleeMoveTarget) // Close version
                        {
                            if (Configs.greaterWispAttackRange.Value < skillDriver.maxDistance)
                            {
                                skillDriver.maxDistance = Configs.greaterWispAttackRange.Value;
                            }
                        }
                        else // Ranged version
                        {
                            skillDriver.maxDistance = Configs.greaterWispAttackRange.Value;
                        }
                    }
                }
            }
        }
    }
}
