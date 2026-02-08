using Mono.Cecil.Cil;
using MonoMod.Cil;
using RoR2;
using RoR2.CharacterAI;
using RoR2.Projectile;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SuperCyanTweaks
{
    public class Grandparent
    {
        public static CharacterSpawnCard cscGrandparent = Addressables.LoadAssetAsync<CharacterSpawnCard>("RoR2/Base/Grandparent/cscGrandparent.asset").WaitForCompletion();
        public static GameObject grandparentMasterPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Grandparent/GrandparentMaster.prefab").WaitForCompletion();
        public static GameObject gravOrb = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Grandparent/GrandparentGravSphere.prefab").WaitForCompletion();
        public static EntityStateConfiguration groundSwipe = Addressables.LoadAssetAsync<EntityStateConfiguration>("RoR2/Base/Grandparent/EntityStates.GrandParentBoss.GroundSwipe.asset").WaitForCompletion();

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

            // Dynamic Rock Throw speed
            if (Configs.grandparentRockDynamicSpeed.Value == true)
            {
                On.EntityStates.GrandParentBoss.GroundSwipe.FixedUpdate += RockDynamicSpeed;
            }

            // Rock Throw Targeting
            if (Configs.grandparentRockTargeting.Value == true)
            {
                bool hookFailed = true;
                IL.EntityStates.GrandParentBoss.GroundSwipe.FixedUpdate += (il) =>
                {
                    ILCursor c = new(il);

                    if (
                        c.TryGotoNext(MoveType.After,
                        x => x.MatchStfld<BullseyeSearch>("searchOrigin")) &&
                        c.TryGotoPrev(MoveType.Before,
                        x => x.MatchCallOrCallvirt("EntityStates.EntityState", "get_characterBody"))
                    )
                    {
                        c.RemoveRange(2);
                        c.EmitDelegate<Func<EntityStates.BaseState, Vector3>>((state) =>
                        {
                            return state.GetAimRay().origin;
                        });

                        if (
                            c.TryGotoNext(MoveType.After,
                            x => x.MatchStfld<BullseyeSearch>("searchDirection")) &&
                            c.TryGotoPrev(MoveType.Before,
                            x => x.MatchCallOrCallvirt("EntityStates.EntityState", "get_characterBody"))
                        )
                        {
                            c.RemoveRange(2);
                            c.EmitDelegate<Func<EntityStates.BaseState, Vector3>>((state) =>
                            {
                                return state.GetAimRay().direction;
                            });

                            if (
                                c.TryGotoNext(MoveType.Before,
                                x => x.MatchStfld<BullseyeSearch>("teamMaskFilter")) &&
                                c.TryGotoPrev(MoveType.Before,
                                x => x.MatchCallOrCallvirt<EntityStates.BaseState>(nameof(EntityStates.BaseState.GetTeam)))
                            )
                            {
                                c.RemoveRange(2);
                                c.EmitDelegate<Func<EntityStates.BaseState, TeamMask>>((state) =>
                                {
                                    TeamMask teamMask = TeamMask.allButNeutral;
                                    if (state.teamComponent)
                                    {
                                        teamMask.RemoveTeam(state.teamComponent.teamIndex);
                                    }
                                    return teamMask;
                                });

                                if (
                                    c.TryGotoNext(MoveType.Before,
                                    x => x.MatchStfld<BullseyeSearch>("sortMode"))
                                )
                                {
                                    c.Index--;
                                    c.Next.OpCode = OpCodes.Ldc_I4_2;

                                    groundSwipe.TryModifyFieldValue("maxBullseyeAngle", 90f);

                                    hookFailed = false;
                                }
                            }
                        }  
                    }

                    if (hookFailed == true)
                    {
                        Log.Error("Granparent rock targeting hook failed!");
                    }
                };
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

        private void RockDynamicSpeed(On.EntityStates.GrandParentBoss.GroundSwipe.orig_FixedUpdate orig, EntityStates.GrandParentBoss.GroundSwipe self)
        {
            float distance = 0f;
            float projectileSpeedMultiplier = 1f;

            if (self.isAuthority && !self.hasFired && self.fixedAge >= self.fireProjectileDelay - .05f)
            {
                // Get distance
                if (self.characterBody && self.characterBody.gameObject && self.characterBody.master)
                {
                    BaseAI ai = self.characterBody.master.GetComponent<BaseAI>();
                    if (ai && ai.currentEnemy != null && ai.currentEnemy.characterBody && ai.currentEnemy.characterBody.gameObject)
                    {
                        distance = (self.characterBody.gameObject.transform.position - ai.currentEnemy.characterBody.gameObject.transform.position).magnitude;
                    }
                }

                // Base projectile speed is 60
                projectileSpeedMultiplier = Mathf.Clamp(distance / 150, 1f, 3.75f);
            }

            self.projectileHorizontalSpeed *= projectileSpeedMultiplier;

            orig(self);

            self.projectileHorizontalSpeed /= projectileSpeedMultiplier;
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
