using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SuperCyanTweaks
{
    public class ScorchWorm
    {
        public static EntityStateConfiguration scorchlingBreach = Addressables.LoadAssetAsync<EntityStateConfiguration>("RoR2/DLC2/Scorchling/EntityStates.Scorchling.Breach.asset").WaitForCompletion();

        public ScorchWorm()
        {
            // Adjust breach damage
            if (Configs.scorchWormBreachDmg.Value >= 0)
            {
                scorchlingBreach.TryModifyFieldValue("blastDamageCoefficient", Configs.scorchWormBreachDmg.Value);
            }
        }
    }
}
