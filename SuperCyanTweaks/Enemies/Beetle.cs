using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SuperCyanTweaks
{
    public class Beetle
    {
        public static EntityStateConfiguration beetleSpawn = Addressables.LoadAssetAsync<EntityStateConfiguration>("RoR2/Base/Beetle/EntityStates.BeetleMonster.SpawnState.asset").WaitForCompletion();
        public static EntityStateConfiguration beetleHeadbutt = Addressables.LoadAssetAsync<EntityStateConfiguration>("RoR2/Base/Beetle/EntityStates.BeetleMonster.HeadbuttState.asset").WaitForCompletion();

        public Beetle()
        {
            // Adjust spawn duration
            if (Configs.beetleSpawnDuration.Value >= 0)
            {
                beetleSpawn.TryModifyFieldValue("duration", Configs.beetleSpawnDuration.Value);
            }

            // Adjust headbutt damage
            if (Configs.beetleHeadbuttDmg.Value >= 0)
            {
                beetleHeadbutt.TryModifyFieldValue("damageCoefficient", Configs.beetleHeadbuttDmg.Value);
            }
        }
    }
}
