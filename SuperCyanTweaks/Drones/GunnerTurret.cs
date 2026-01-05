using RoR2;
using RoR2.CharacterAI;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SuperCyanTweaks
{
    public class GunnerTurret
    {
        public static GameObject gunnerTurretMasterPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Drones/Turret1Master.prefab").WaitForCompletion();

        public GunnerTurret()
        {
            // Adjust attack range
            if (Configs.gunnerTurretRange.Value >= 0)
            {
                AISkillDriver[] skillDrivers = gunnerTurretMasterPrefab.GetComponents<AISkillDriver>();
                foreach (AISkillDriver skillDriver in skillDrivers)
                {
                    if (skillDriver.skillSlot == SkillSlot.Primary)
                    {
                        skillDriver.maxDistance = Configs.gunnerTurretRange.Value;
                    }
                }
            }
        }
    }
}
