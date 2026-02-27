using RoR2;
using UnityEngine.Networking;

namespace SuperCyanTweaks
{
    public class AccessNode
    {
        public AccessNode()
        {
            // Multiply item rewards
            if (Configs.accessNodeMultiplyRewards.Value == true)
            {
                On.RoR2.TeleporterInteraction.ActivateAccessCodes += TeleporterInteraction_ActivateAccessCodes; // Remove the vanilla Mountain Shrine effect it has
                TeleporterInteraction.onTeleporterBeginChargingGlobal += OnTeleporterBeginChargingGlobal; // Double final rewards
            }
        }

        private void TeleporterInteraction_ActivateAccessCodes(On.RoR2.TeleporterInteraction.orig_ActivateAccessCodes orig, TeleporterInteraction self)
        {
            BossGroup bossGroup = self.bossGroup;

            int? bonusRewardCount = null;
            if (bossGroup)
            {
                bonusRewardCount = self.bossGroup.bonusRewardCount;
            }
            
            orig(self);

            if (bossGroup && bonusRewardCount != null)
            {
                self.bossGroup.bonusRewardCount = (int)bonusRewardCount;
            } 
        }

        private void OnTeleporterBeginChargingGlobal(TeleporterInteraction teleporterInteraction)
        {
            if (!NetworkServer.active)
            {
                return;
            }

            BossGroup bossGroup = teleporterInteraction.bossGroup;
            if (teleporterInteraction.isAccessingCodes == true && bossGroup)
            {
                int bonusRewardCount = (bossGroup.bonusRewardCount * 2) + 1;
                bossGroup.bonusRewardCount = bonusRewardCount;
            }
        }
    }
}
