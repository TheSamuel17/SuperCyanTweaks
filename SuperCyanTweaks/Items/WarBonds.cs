using RoR2;
using UnityEngine.AddressableAssets;

namespace SuperCyanTweaks
{
    public class WarBonds
    {
        public static ItemDef warBonds = Addressables.LoadAssetAsync<ItemDef>("RoR2/DLC2/Items/BarrageOnBoss/BarrageOnBoss.asset").WaitForCompletion();

        public WarBonds()
        {
            // No longer inheritable by turrets
            if (Configs.warBondsInheritable.Value == false)
            {
                warBonds.tags = warBonds.tags.AddToArray(ItemTag.CannotCopy);
            }
        }
    }
}
