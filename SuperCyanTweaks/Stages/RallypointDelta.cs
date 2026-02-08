using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SuperCyanTweaks
{
    public class RallypointDelta
    {
        public static Sprite fanSpeedBuffSprite = Addressables.LoadAssetAsync<Sprite>("RoR2/Base/Common/texMovespeedBuffIcon.tif").WaitForCompletion();
        public static BuffDef fanSpeedBuffDef;

        public RallypointDelta()
        {
            // Speed boost
            if (Configs.fanSpeedBoost.Value == true)
            {
                // Create BuffDef
                fanSpeedBuffDef = ScriptableObject.CreateInstance<BuffDef>();
                fanSpeedBuffDef.buffColor = new Color(32f/255f, 128f/255f, 223f/255f, 1f);
                fanSpeedBuffDef.canStack = false;
                fanSpeedBuffDef.eliteDef = null;
                fanSpeedBuffDef.iconSprite = fanSpeedBuffSprite;
                fanSpeedBuffDef.ignoreGrowthNectar = false;
                fanSpeedBuffDef.isCooldown = false;
                fanSpeedBuffDef.isDebuff = false;
                fanSpeedBuffDef.isDOT = false;
                fanSpeedBuffDef.isHidden = false;
                fanSpeedBuffDef.stackingDisplayMethod = BuffDef.StackingDisplayMethod.Default;
                fanSpeedBuffDef.startSfx = null;
                fanSpeedBuffDef.name = "SuperCyanTweaks_FanSpeedBuff";

                // Register buff
                ContentAddition.AddBuffDef(fanSpeedBuffDef);

                // Buff logic
                RecalculateStatsAPI.GetStatCoefficients += GetStatCoefficients;

                // Set up fan behavior
                PurchaseInteraction.onPurchaseGlobalServer += PurchaseInteraction_onPurchaseGlobalServer;
            }
        }

        private void GetStatCoefficients(CharacterBody body, RecalculateStatsAPI.StatHookEventArgs args)
        {
            if (body && body.HasBuff(fanSpeedBuffDef))
            {
                args.moveSpeedMultAdd += Configs.fanBuffStrength.Value / 100f;
            }
        }

        private void PurchaseInteraction_onPurchaseGlobalServer(CostTypeDef.PayCostContext payCostContext, CostTypeDef.PayCostResults payCostResults)
        {
            GameObject purchasedObject = payCostContext.purchasedObject;
            PurchaseInteraction purchaseInteraction = purchasedObject.GetComponent<PurchaseInteraction>();
            if (purchaseInteraction && purchaseInteraction.displayNameToken == "FAN_NAME")
            {
                var buffZone = purchasedObject.AddComponent<BuffWard>();
                buffZone.shape = BuffWard.BuffWardShape.VerticalTube;
                buffZone.radius = 3.1f;
                buffZone.TubeHeight = 5.1f;
                buffZone.interval = .1f;
                buffZone.expires = false;

                buffZone.buffDef = fanSpeedBuffDef;
                buffZone.buffDuration = Configs.fanBuffDuration.Value;
                buffZone.useTeamMemberFeetPosition = true;
                buffZone.invertTeamFilter = false;

                buffZone.gameObject.transform.position = purchasedObject.transform.position;
                buffZone.floorWard = true;

                buffZone.Networkradius = buffZone.radius;

                TeamFilter teamFilter = buffZone.gameObject.GetComponent<TeamFilter>();
                teamFilter.teamIndex = TeamIndex.Player;
            }
        }
    }
}
