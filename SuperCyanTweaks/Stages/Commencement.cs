using RoR2;
using R2API;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SuperCyanTweaks
{
    public class Commencement
    {
        public static GameObject designPulsePrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/moon2/MoonBatteryDesignPulse.prefab").WaitForCompletion();

        public Commencement()
        {
            // No OnStageBeginEffect items in the cauldrons
            if (Configs.moonCauldronTweak.Value == true)
            {
                Stage.onStageStartGlobal += OnStageStartGlobal;
            }

            // Adjust initial monster credits
            if (Configs.moonInitialMonsterCredits.Value >= 0)
            {
                DirectorAPI.Helpers.AddSceneMonsterCredits(Configs.moonInitialMonsterCredits.Value - 900, DirectorAPI.Stage.Commencement);
            }

            // Adjust Pillar charging parameters
            if (Configs.moonPillarRadius.Value >= 0 || Configs.moonPillarMassDuration.Value >= 0 || Configs.moonPillarMonsterCredits.Value >= 0)
            {
                On.RoR2.HoldoutZoneController.Awake += HoldoutZoneController_Awake;

                // Scale Pillar of Design pulse radius accordingly if bigger than vanilla
                if (Configs.moonPillarRadius.Value >= 0)
                {
                    designPulsePrefab.GetComponent<PulseController>().finalRadius = Mathf.Max(Configs.moonPillarRadius.Value, 20f);
                }
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

        private void HoldoutZoneController_Awake(On.RoR2.HoldoutZoneController.orig_Awake orig, HoldoutZoneController self)
        {
            switch (self.inBoundsObjectiveToken)
            {
                case "OBJECTIVE_MOON_BATTERY_MASS":
                    AdjustChargeRadius(self);
                    AdjustMonsterCredits(self);

                    if (Configs.moonPillarMassDuration.Value >= 0)
                    {
                        self.baseChargeDuration = Configs.moonPillarMassDuration.Value;
                    }

                    break;

                case "OBJECTIVE_MOON_BATTERY_DESIGN":
                    AdjustChargeRadius(self);
                    AdjustMonsterCredits(self);
                    break;

                case "OBJECTIVE_MOON_BATTERY_BLOOD":
                    AdjustChargeRadius(self);
                    AdjustMonsterCredits(self);
                    break;

                case "OBJECTIVE_MOON_BATTERY_SOUL":
                    AdjustChargeRadius(self);
                    AdjustMonsterCredits(self);
                    break;

                default:
                    break;
            }
            
            orig(self);
        }

        private void AdjustChargeRadius(HoldoutZoneController holdout)
        {
            if (Configs.moonPillarRadius.Value >= 0)
            {
                holdout.chargeRadiusDelta *= Configs.moonPillarRadius.Value / holdout.baseRadius; // Pillar of Soul shrinks proportionally
                holdout.baseRadius = Configs.moonPillarRadius.Value;
            }
        }

        private void AdjustMonsterCredits(HoldoutZoneController holdout)
        {
            if (Configs.moonPillarMonsterCredits.Value >= 0)
            {
                CombatDirector director = holdout.gameObject.GetComponent<CombatDirector>();
                if (director)
                {
                    director.SetMonsterCredit(Configs.moonPillarMonsterCredits.Value);
                }
            }
        }
    }
}
