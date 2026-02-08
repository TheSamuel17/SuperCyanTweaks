using RoR2;
using RoR2.CharacterAI;
using R2API;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SuperCyanTweaks
{
    public class SolusControlUnit
    {
        public static GameObject scuMasterPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/RoboBallBoss/RoboBallBossMaster.prefab").WaitForCompletion();
        public static GameObject awuMasterPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/RoboBallBoss/SuperRoboBallBossMaster.prefab").WaitForCompletion();
        public static CharacterSpawnCard cscScu = Addressables.LoadAssetAsync<CharacterSpawnCard>("RoR2/Base/RoboBallBoss/cscRoboBallBoss.asset").WaitForCompletion();

        public SolusControlUnit()
        {
            if (Configs.scuRespectNodegraph.Value == true || Configs.scuPrimaryAttackRange.Value >= 0 || Configs.scuProbeHealthThreshold.Value >= 0)
            {
                AISkillDriver[] skillDrivers = scuMasterPrefab.GetComponents<AISkillDriver>();
                foreach (AISkillDriver skillDriver in skillDrivers)
                {
                    AdjustSkillDrivers(skillDriver);
                }

                skillDrivers = awuMasterPrefab.GetComponents<AISkillDriver>();
                foreach (AISkillDriver skillDriver in skillDrivers)
                {
                    AdjustSkillDrivers(skillDriver);
                }
            }

            if (Configs.scuOnSolusWingLoop.Value == true)
            {
                DirectorAPI.DirectorCardHolder scuSpawnCard = new()
                {
                    Card = new()
                    {
                        minimumStageCompletions = 5,
                        preventOverhead = false,
                        selectionWeight = 20,
                        spawnDistance = DirectorCore.MonsterSpawnDistance.Standard,
                        spawnCard = cscScu,
                    },
                    MonsterCategory = DirectorAPI.MonsterCategory.Champions,
                    MonsterCategorySelectionWeight = 33,
                };
                DirectorAPI.Helpers.AddNewMonsterToStage(scuSpawnCard, false, DirectorAPI.Stage.SolutionalHaunt);
            }
        }

        private void AdjustSkillDrivers(AISkillDriver skillDriver)
        {
            switch (skillDriver.customName)
            {
                case "FireAndChase":
                    if (Configs.scuRespectNodegraph.Value == true)
                    {
                        skillDriver.ignoreNodeGraph = false;
                    }
                    break;

                case "FireAndStop":
                    if (Configs.scuPrimaryAttackRange.Value >= 0)
                    {
                        skillDriver.maxDistance = Configs.scuPrimaryAttackRange.Value;
                    }
                    break;

                case "FireAndFlee":
                    if (Configs.scuPrimaryAttackRange.Value >= 0 && Configs.scuPrimaryAttackRange.Value < skillDriver.maxDistance)
                    {
                        skillDriver.maxDistance = Configs.scuPrimaryAttackRange.Value;
                    }
                    break;

                case "DeployMinion":
                    if (Configs.scuProbeHealthThreshold.Value >= 0)
                    {
                        if (Configs.scuProbeHealthThreshold.Value >= 1)
                        {
                            skillDriver.maxUserHealthFraction = float.PositiveInfinity;
                        } 
                        else
                        {
                            skillDriver.maxUserHealthFraction = Configs.scuProbeHealthThreshold.Value;
                        }
                    }
                    break;

                default:
                    break;
            }
        }
    }
}
