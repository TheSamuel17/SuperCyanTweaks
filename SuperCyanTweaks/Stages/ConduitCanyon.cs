using MonoMod.Cil;
using RoR2;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SuperCyanTweaks
{
    public class ConduitCanyon
    {
        public ConduitCanyon()
        {
            if (Configs.conduitCanyonCollectiveCost.Value >= 0 || Configs.conduitCanyonShrineCreditLeader.Value >= 0 || Configs.conduitCanyonShrineCreditSupport.Value >= 0 || EclipseRevampedCompat.enabled)
            {
                SceneDirector.onPrePopulateMonstersSceneServer += OnPrePopulateMonsters;
            }

            // No more debt!
            if (Configs.conduitCanyonCollectiveDebtFix.Value == true)
            {
                On.RoR2.PromoteSpawnedEnemyToElite.OnEnemySpawn += DebtFix;
            }

            // Collective Shrine of Combat: supporting monsters credit multiplier
            if (Configs.conduitCanyonShrineCreditSupportMult.Value >= 0)
            {
                bool hookFailed = true;
                IL.RoR2.ShrineCombatTroopBehavior.Init += (il) =>
                {
                    ILCursor c = new(il);

                    if (
                        c.TryGotoNext(MoveType.Before,
                        x => x.MatchCallOrCallvirt("RoR2.ShrineCombatTroopBehavior", "get_totalSupportMonsterCredit")) &&
                        c.TryGotoNext(MoveType.Before,
                        x => x.MatchLdcR4(out _))
                    )
                    {
                        c.Next.Operand = 1 / Configs.conduitCanyonShrineCreditSupportMult.Value;
                        hookFailed = false;
                    }

                    if (hookFailed == true)
                    {
                        Log.Error("Collective Combat Shrine support hook failed!");
                    }
                };
            }

            // Adjust gold reward
            if (Configs.conduitCanyonCollectiveRewardFix.Value == true)
            {
                On.RoR2.PromoteSpawnedEnemyToElite.PromoteThisBody += RewardFix;
            }
        }

        private void OnPrePopulateMonsters(SceneDirector sceneDirector)
        {
            if (!SceneInfo.instance) return;
            if (SceneInfo.instance.sceneDef != SceneCatalog.FindSceneDef("conduitcanyon")) return;

            // Elite cost
            if (Configs.conduitCanyonCollectiveCost.Value >= 0 || EclipseRevampedCompat.enabled)
            {
                // Elite promoting algorithm
                GameObject directorHolder = GameObject.Find("/Director/");
                if (directorHolder)
                {
                    PromoteSpawnedEnemyToElite promoter = directorHolder.GetComponent<PromoteSpawnedEnemyToElite>();
                    if (promoter)
                    {
                        float newCost = Configs.conduitCanyonCollectiveCost.Value >= 0 ? Configs.conduitCanyonCollectiveCost.Value : promoter.customEliteCostMultiplier;

                        if (EclipseRevampedCompat.enabled && Run.instance && Run.instance.selectedDifficulty >= DifficultyIndex.Eclipse3)
                        {
                            newCost *= .8f;
                        }

                        promoter.customEliteCostMultiplier = newCost;
                    }
                }
            }

            // Collective Combat Shrines
            List<PurchaseInteraction> instanceList = InstanceTracker.GetInstancesList<PurchaseInteraction>();
            foreach (PurchaseInteraction instance in instanceList)
            {
                if (instance.displayNameToken == "COLLECTIVE_SHRINE_NAME")
                {
                    ShrineCombatTroopBehavior shrineBehavior = instance.gameObject.GetComponent<ShrineCombatTroopBehavior>();
                    if (shrineBehavior)
                    {
                        // Elite cost
                        if (Configs.conduitCanyonCollectiveCost.Value >= 0 || EclipseRevampedCompat.enabled)
                        {
                            float newCost = Configs.conduitCanyonCollectiveCost.Value >= 0 ? Configs.conduitCanyonCollectiveCost.Value : shrineBehavior.leaderEliteCostCoeff;

                            if (EclipseRevampedCompat.enabled && Run.instance && Run.instance.selectedDifficulty >= DifficultyIndex.Eclipse3)
                            {
                                newCost *= .8f;
                            }

                            shrineBehavior.leaderEliteCostCoeff = newCost;
                        }

                        // Monster credits
                        if (Configs.conduitCanyonShrineCreditLeader.Value >= 0)
                        {
                            shrineBehavior.baseLeaderMonsterCredit = Configs.conduitCanyonShrineCreditLeader.Value;
                        }

                        if (Configs.conduitCanyonShrineCreditSupport.Value >= 0)
                        {
                            shrineBehavior.baseSupportMonsterCredit = Configs.conduitCanyonShrineCreditSupport.Value;
                        }
                    }
                }
            }
        }

        private void DebtFix(On.RoR2.PromoteSpawnedEnemyToElite.orig_OnEnemySpawn orig, PromoteSpawnedEnemyToElite self, GameObject spawn, CombatDirector combatDir)
        {
            if (self.TryResolveCharacterBodyFromSpawn(spawn, out var spawnBody))
            {
                if (combatDir && combatDir.lastAttemptedMonsterCard.spawnCard)
                {
                    float newCost = Mathf.Max((float)combatDir.lastAttemptedMonsterCard.spawnCard.directorCreditCost * self.customEliteCostMultiplier - spawnBody.cost, 0f);
                    if (newCost > combatDir.monsterCredit)
                    {
                        return;
                    }
                }
            }
            
            orig(self, spawn, combatDir);
        }

        private void RewardFix(On.RoR2.PromoteSpawnedEnemyToElite.orig_PromoteThisBody orig, PromoteSpawnedEnemyToElite self, CharacterBody spawnedBody)
        {
            orig(self, spawnedBody);

            GameObject bodyObject = spawnedBody.gameObject;
            DeathRewards deathRewards = bodyObject.GetComponent<DeathRewards>();

            if (deathRewards)
            {
                deathRewards.expReward = (uint)Mathf.Max(1f, deathRewards.expReward * self.customEliteCostMultiplier);
                deathRewards.goldReward = (uint)Mathf.Max(1f, deathRewards.goldReward * self.customEliteCostMultiplier);
            }
        }
    }
}
