using RoR2;
using UnityEngine.AddressableAssets;

namespace SuperCyanTweaks
{
    public class BighornBison
    {
        public static EntityStateConfiguration bisonCharge = Addressables.LoadAssetAsync<EntityStateConfiguration>("RoR2/Base/Bison/EntityStates.Bison.Charge.asset").WaitForCompletion();

        public BighornBison()
        {
            // Adjust charge speed coefficient
            if (Configs.bighornBisonChargeCoeff.Value >= 0)
            {
                bisonCharge.TryModifyFieldValue("chargeMovementSpeedCoefficient", Configs.bighornBisonChargeCoeff.Value);
            }
        }
    }
}
