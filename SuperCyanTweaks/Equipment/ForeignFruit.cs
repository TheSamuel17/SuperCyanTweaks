using RoR2;
using UnityEngine.AddressableAssets;

namespace SuperCyanTweaks
{
    public class ForeignFruit
    {
        public static EquipmentDef fruit = Addressables.LoadAssetAsync<EquipmentDef>("RoR2/Base/Fruit/Fruit.asset").WaitForCompletion();

        public ForeignFruit()
        {
            // Adjust cooldown
            if (Configs.foreignFruitCooldown.Value >= 0)
            {
                fruit.cooldown = Configs.foreignFruitCooldown.Value;
            }
        }
    }
}
