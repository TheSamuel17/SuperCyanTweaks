using RoR2;
using RoR2.CharacterAI;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SuperCyanTweaks
{
    public class VoidJailer
    {
        public static GameObject jailerMasterPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/VoidJailer/VoidJailerMaster.prefab").WaitForCompletion();

        public VoidJailer()
        {
            // Idling fix
            if (Configs.voidJailerIdleFix.Value == true)
            {
                AISkillDriver[] skillDrivers = jailerMasterPrefab.GetComponents<AISkillDriver>();
                foreach (AISkillDriver skillDriver in skillDrivers)
                {
                    if (skillDriver.customName == "PathFromAfar")
                    {
                        skillDriver.selectionRequiresTargetLoS = false;
                    }
                }
            }
        }
    }
}
