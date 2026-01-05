using RoR2.CharacterAI;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SuperCyanTweaks
{
    public class EquipmentDrone
    {
        public static GameObject equipDroneMasterPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Drones/EquipmentDroneMaster.prefab").WaitForCompletion();

        public EquipmentDrone()
        {
            // Make them always fire their held Equipment
            if (Configs.equipDroneAlwaysFire.Value == true)
            {
                AISkillDriver[] skillDrivers = equipDroneMasterPrefab.GetComponents<AISkillDriver>();
                foreach (AISkillDriver skillDriver in skillDrivers)
                {
                    skillDriver.shouldFireEquipment = true;

                    // For the rare instances where an Equipment cooldown ends up shorter than a behavior's duration
                    if (skillDriver.driverUpdateTimerOverride > .5f)
                    {
                        skillDriver.driverUpdateTimerOverride = .5f;
                    }
                }
            }
        }
    }
}
