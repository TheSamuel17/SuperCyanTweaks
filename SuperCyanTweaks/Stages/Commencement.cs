using RoR2;
using System.Collections.Generic;

namespace SuperCyanTweaks
{
    public class Commencement
    {
        public Commencement()
        {
            if (Configs.moonCauldronTweak.Value == true)
            {
                Stage.onStageStartGlobal += OnStageStartGlobal;
            }
        }

        private void OnStageStartGlobal(Stage stage)
        {
            if (SceneInfo.instance.sceneDef != SceneCatalog.FindSceneDef("moon2") && SceneInfo.instance.sceneDef != SceneCatalog.FindSceneDef("moon"))
            {
                return;
            }

            List<PurchaseInteraction> instanceList = InstanceTracker.GetInstancesList<PurchaseInteraction>();
            foreach (PurchaseInteraction instance in instanceList)
            {
                if (instance.displayNameToken == "BAZAAR_CAULDRON_NAME")
                {
                    var stb = instance.gameObject.GetComponent<ShopTerminalBehavior>();
                    if (stb && stb.pickup != null)
                    {
                        stb.bannedItemTag |= ItemTag.OnStageBeginEffect;

                        ItemDef currentItemDef = ItemCatalog.GetItemDef(stb.pickup.pickupIndex.pickupDef.itemIndex);
                        if (currentItemDef && currentItemDef.ContainsTag(ItemTag.OnStageBeginEffect))
                        {
                            stb.GenerateNewPickupServer();
                        }
                    }
                }
            }
        }
    }
}
