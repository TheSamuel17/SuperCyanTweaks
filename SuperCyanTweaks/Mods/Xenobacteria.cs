using RoR2;

namespace SuperCyanTweaks
{
    public class Xenobacteria
    {
        public Xenobacteria()
        {
            // Change to Void Uncommon
            if (Configs.xenobacteriaRetier.Value == true)
            {
                ItemCatalog.availability.CallWhenAvailable(delegate ()
                {
                    ItemDef xenobacteria = ItemCatalog.GetItemDef(ItemCatalog.FindItemIndex("Xenobacteria"));
                    if (xenobacteria)
                    {
                        xenobacteria.tier = ItemTier.VoidTier2;
                    }
                });
            }
        }
    }
}
