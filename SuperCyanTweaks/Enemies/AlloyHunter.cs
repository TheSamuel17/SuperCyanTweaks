using RoR2;
using UnityEngine.AddressableAssets;

namespace SuperCyanTweaks
{
    public class AlloyHunter
    {
        public static RoR2.Skills.SkillDef alloyHunterLaserSkill = Addressables.LoadAssetAsync<RoR2.Skills.SkillDef>("RoR2/DLC3/VultureHunter/ChargeSolusLaser.asset").WaitForCompletion();

        public AlloyHunter()
        {
            // Block cooldown while laser is active
            if (Configs.alloyHunterLaserCooldownTweak.Value == true)
            {
                alloyHunterLaserSkill.isCooldownBlockedUntilManuallyReset = true;
                On.EntityStates.VultureHunter.Weapon.FireSolusLaser.OnExit += FireSolusLaser_OnExit;
            }
        }

        private void FireSolusLaser_OnExit(On.EntityStates.VultureHunter.Weapon.FireSolusLaser.orig_OnExit orig, EntityStates.VultureHunter.Weapon.FireSolusLaser self)
        {
            orig(self);

            CharacterBody body = self.characterBody;
            if (body && body.skillLocator && body.skillLocator.primary && body.skillLocator.primary.skillDef == alloyHunterLaserSkill)
            {
                body.skillLocator.primary.SetBlockedCooldownSkillState(false);
            }
        }
    }
}
