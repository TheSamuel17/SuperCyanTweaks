using RoR2;
using RoR2.CharacterAI;
using RoR2.Projectile;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SuperCyanTweaks
{
    public class Grandparent
    {
        public static CharacterSpawnCard cscGrandparent = Addressables.LoadAssetAsync<CharacterSpawnCard>("RoR2/Base/Grandparent/cscGrandparent.asset").WaitForCompletion();
        public static GameObject grandparentMasterPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Grandparent/GrandparentMaster.prefab").WaitForCompletion();
        public static GameObject gravOrb = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Grandparent/GrandparentGravSphere.prefab").WaitForCompletion();

        public Grandparent()
        {
            // Adjust director credit cost
            if (Configs.grandparentCost.Value >= 0)
            {
                cscGrandparent.directorCreditCost = Configs.grandparentCost.Value;
            }

            // Rock Throw activation range
            if (Configs.grandparentRockActivationRange.Value >= 0)
            {
                AISkillDriver[] skillDrivers = grandparentMasterPrefab.GetComponents<AISkillDriver>();
                foreach (AISkillDriver skillDriver in skillDrivers)
                {
                    if (skillDriver.customName == "GroundSwipe")
                    {
                        skillDriver.maxDistance = Configs.grandparentRockActivationRange.Value;
                    }
                }
            }

            // Dynamic Gravity Orb speed
            if (Configs.grandparentGravOrbDynamicSpeed.Value == true)
            {
                On.EntityStates.GrandParentBoss.FireSecondaryProjectile.Fire += FireGravOrb;
            }

            // Gravity Orb pull force
            if (Configs.grandparentGravOrbForce.Value != -1000f)
            {
                RadialForce rf = gravOrb.GetComponent<RadialForce>();
                if (rf)
                {
                    rf.forceMagnitude = Configs.grandparentGravOrbForce.Value;
                }
            }

            // Reduce downtime
            if (Configs.grandparentReduceDowntime.Value == true)
            {
                AISkillDriver[] skillDrivers = grandparentMasterPrefab.GetComponents<AISkillDriver>();
                foreach (AISkillDriver skillDriver in skillDrivers)
                {
                    if (skillDriver.skillSlot == SkillSlot.None && skillDriver.driverUpdateTimerOverride > 2f)
                    {
                        skillDriver.driverUpdateTimerOverride = 2f;
                    }
                }
            }
        }

        private void FireGravOrb(On.EntityStates.GrandParentBoss.FireSecondaryProjectile.orig_Fire orig, EntityStates.GrandParentBoss.FireSecondaryProjectile self)
        {
            // Get distance
            float distance = 0f;
            if (self.characterBody && self.characterBody.gameObject && self.characterBody.master)
            {
                BaseAI ai = self.characterBody.master.GetComponent<BaseAI>();
                if (ai && ai.currentEnemy != null && ai.currentEnemy.characterBody && ai.currentEnemy.characterBody.gameObject)
                {
                    distance = (self.characterBody.gameObject.transform.position - ai.currentEnemy.characterBody.gameObject.transform.position).magnitude;
                }
            }

            // Base projectile speed is 15
            float projectileSpeedMultiplier = Mathf.Clamp(distance / 90, 1f, 15f);

            // Multiply projectile speed, then set back to normal afterwards
            ProjectileSimple ps = self.projectilePrefab.GetComponent<ProjectileSimple>();
            if (ps) {
                ps.desiredForwardSpeed *= projectileSpeedMultiplier;
            }  

            orig(self);

            if (ps) {
                ps.desiredForwardSpeed /= projectileSpeedMultiplier;
            }
        }
    }
}
