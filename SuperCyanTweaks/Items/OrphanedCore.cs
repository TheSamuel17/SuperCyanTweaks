using RoR2;
using R2API;
using UnityEngine.AddressableAssets;
using UnityEngine;
using RoR2.CharacterAI;

namespace SuperCyanTweaks
{
    public class OrphanedCore
    {
        public static EntityStateConfiguration bestBuddyRam = Addressables.LoadAssetAsync<EntityStateConfiguration>("RoR2/DLC3/FriendUnit/EntityStates.FriendUnit.KineticAura.asset").WaitForCompletion();
        public static GameObject friendUnitMasterPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC3/FriendUnit/FriendUnitMaster.prefab").WaitForCompletion();
        public static GameObject friendUnitBodyPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC3/FriendUnit/FriendUnitBody.prefab").WaitForCompletion();


        public OrphanedCore()
        {
            // Adjust ram damage
            if (Configs.bestBuddyRamDamage.Value >= 0)
            {
                bestBuddyRam.TryModifyFieldValue("chargeDamageCoefficient", Configs.bestBuddyRamDamage.Value);
                bestBuddyRam.TryModifyFieldValue("knockbackDamageCoefficient", Configs.bestBuddyRamDamage.Value * 2.5f);
                UpdateDescription(Configs.bestBuddyRamDamage.Value * 100f);
            }

            // Improve hit detection
            if (Configs.bestBuddyHitDetection.Value == true)
            {
                if (bestBuddyRam.TryGetFieldValueString("refreshTime", out float refreshTime) && refreshTime > 1f)
                    bestBuddyRam.TryModifyFieldValue("refreshTime", 1f); // Vanilla is 1.5

                CharacterBody body = friendUnitBodyPrefab.GetComponent<CharacterBody>();
                if (!body) return;

                ModelLocator modelLocator = body.GetComponent<ModelLocator>();
                if (!modelLocator) return;

                Transform modelTransform = modelLocator.modelTransform;
                if (!modelTransform) return;

                HitBoxGroup hitBoxGroup = modelTransform.GetComponent<HitBoxGroup>();
                if (!hitBoxGroup) return;

                HitBox[] hitboxes = hitBoxGroup.hitBoxes;
                foreach (HitBox hitbox in hitboxes)
                    hitbox.transform.localScale *= 2.5f;
            }

            // Adjust behavior
            if (Configs.bestBuddyBehaviorTweak.Value == true)
            {
                // Ram properties
                if (bestBuddyRam.TryGetFieldValueString("initialHopVelocity", out float hopVelocity) && hopVelocity < 1.2f)
                {
                    bestBuddyRam.TryModifyFieldValue("initialHopVelocity", 1.2f); // Vanilla is 1
                }

                if (bestBuddyRam.TryGetFieldValueString("lockonDistance", out float lockonDistance) && lockonDistance < 40f)
                {
                    bestBuddyRam.TryModifyFieldValue("lockonDistance", 50f); // Vanilla is 30
                }

                if (bestBuddyRam.TryGetFieldValueString("lockonAngle", out float lockonAngle) && lockonAngle < 360f)
                {
                    bestBuddyRam.TryModifyFieldValue("lockonAngle", 360f); // Vanilla is 180
                }

                if (bestBuddyRam.TryGetFieldValueString("recoilAmplitude", out float recoilAmplitude) && recoilAmplitude < 2f)
                {
                    bestBuddyRam.TryModifyFieldValue("recoilAmplitude", 2f); // Vanilla is 1
                }

                // AI parameters
                BaseAI ai = friendUnitMasterPrefab.GetComponent<BaseAI>();
                if (ai)
                {
                    ai.fullVision = true;
                    ai.xrayVision = true;
                    ai.copyLeaderTarget = true;
                }

                // Skill drivers
                AISkillDriver[] skillDrivers = friendUnitMasterPrefab.GetComponents<AISkillDriver>();
                foreach (AISkillDriver skillDriver in skillDrivers)
                {
                    switch (skillDriver.customName)
                    {
                        case "KineticAura":
                            if (skillDriver.maxDistance < 50f)
                            {
                                skillDriver.maxDistance = 50f; // Increase targeting range; vanilla is 30.
                            }

                            skillDriver.requireSkillReady = true; // This skill driver can be chosen while on cooldown, for some reason. This is what makes them very clingy towards enemies.
                            skillDriver.shouldSprint = true;

                            skillDriver.resetCurrentEnemyOnNextDriverSelection = true; // Retarget after behavior is complete.

                            skillDriver.activationRequiresTargetLoS = true; // LoS check is helpful methinks.
                            skillDriver.selectionRequiresTargetLoS = true;

                            return;

                        case "StrafeAroundEnemy":
                            if (skillDriver.minDistance > 0f)
                            {
                                skillDriver.minDistance = 0f; // Only strafes in a narrow 10-15m range, also contributing to their clingy behavior towards enemies.
                            }

                            if (skillDriver.maxDistance < 20f)
                            {
                                skillDriver.maxDistance = 20f;
                            }

                            return;

                        case "NavigateToEnemy":
                            
                            if (skillDriver.maxDistance < 60f)
                            {
                                skillDriver.maxDistance = 60f; // Increase pathing range; vanilla is 50.
                            }
                            
                            skillDriver.selectionRequiresAimTarget = false; // Why are these set to true?
                            skillDriver.selectionRequiresTargetLoS = false;

                            return;
                    }
                }
            }
        }

        private void UpdateDescription(float ramDamage)
        {
            string ramDamageString = ramDamage.ToString().Replace(",", ".");

            LanguageAPI.Add("ITEM_PHYSICSPROJECTILE_DESC",
                $"Gain a <style=cIsHealing>friendly Solus unit</style> that launches itself at enemies for <style=cIsDamage>{ramDamageString}%</style> <style=cStack>(+{ramDamageString}% per stack)</style> <style=cIsDamage>damage</style>. " +
                $"The unit inherits your <style=cIsUtility>movement speed items</style> and deals <style=cIsDamage>more damage the faster it moves</style>. " +
                $"The unit can be pet to <style=cIsHealing>cleanse negative effects</style>. " +
                $"Recharges after 15 seconds.",
                "en"
            );
        }
    }
}
