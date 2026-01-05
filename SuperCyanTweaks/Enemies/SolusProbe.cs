using RoR2;
using RoR2.CharacterAI;
using R2API;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SuperCyanTweaks
{
    public class SolusProbe
    {
        public static CharacterSpawnCard cscProbe = Addressables.LoadAssetAsync<CharacterSpawnCard>("RoR2/Base/RoboBallBoss/cscRoboBallMini.asset").WaitForCompletion();
        public static GameObject probeMasterPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/RoboBallBoss/RoboBallMiniMaster.prefab").WaitForCompletion();
        public static FamilyDirectorCardCategorySelection solusFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>("RoR2/DLC3/dccsSuperRoboBallpitFamily.asset").WaitForCompletion();

        public SolusProbe()
        {
            // Adjust director credit cost
            if (Configs.probeCost.Value >= 0)
            {
                cscProbe.directorCreditCost = Configs.probeCost.Value;
            }

            // Make them spawn in Siren's Call
            if (Configs.probeOnShipGraveyard.Value == true)
            {
                DirectorAPI.DirectorCardHolder probeSpawnCard = new()
                {
                    Card = new()
                    {
                        minimumStageCompletions = 0,
                        preventOverhead = false,
                        selectionWeight = 5, // Half weight
                        spawnDistance = DirectorCore.MonsterSpawnDistance.Standard,
                        spawnCard = cscProbe,
                    },
                    MonsterCategory = DirectorAPI.MonsterCategory.BasicMonsters,
                };

                DirectorAPI.Helpers.AddNewMonsterToStage(probeSpawnCard, false, DirectorAPI.Stage.SirensCall);
            }

            // Make them spawn in Repurposed Crater
            if (Configs.probeOnRepurposedCrater.Value == true)
            {
                DirectorAPI.DirectorCardHolder probeSpawnCard = new()
                {
                    Card = new()
                    {
                        minimumStageCompletions = 0,
                        preventOverhead = false,
                        selectionWeight = 5, // Half weight
                        spawnDistance = DirectorCore.MonsterSpawnDistance.Standard,
                        spawnCard = cscProbe,
                    },
                    MonsterCategory = DirectorAPI.MonsterCategory.BasicMonsters,
                };

                DirectorAPI.Helpers.AddNewMonsterToStage(probeSpawnCard, false, DirectorAPI.Stage.RepurposedCrater);
            }

            // Make them spawn during the Solus Family Event
            if (Configs.probeOnSolusEvent.Value == true)
            {
                DirectorAPI.DirectorCardHolder probeSpawnCard = new()
                {
                    Card = new()
                    {
                        minimumStageCompletions = 0,
                        preventOverhead = false,
                        selectionWeight = 1,
                        spawnDistance = DirectorCore.MonsterSpawnDistance.Standard,
                        spawnCard = cscProbe,
                    },
                    MonsterCategory = DirectorAPI.MonsterCategory.BasicMonsters,
                };

                DirectorAPI.AddCard(solusFamily, probeSpawnCard);
            }

            // Solus Probe will move around a little before attacking
            if (Configs.probeDelayAttackOnSpawn.Value == true)
            {
                AISkillDriver strafeOnSpawn = probeMasterPrefab.AddComponent<AISkillDriver>();
                strafeOnSpawn.activationRequiresAimConfirmation = false;
                strafeOnSpawn.activationRequiresAimTargetLoS = false;
                strafeOnSpawn.activationRequiresTargetLoS = false;
                strafeOnSpawn.aimType = AISkillDriver.AimType.AtMoveTarget;
                strafeOnSpawn.buttonPressType = AISkillDriver.ButtonPressType.Abstain;
                strafeOnSpawn.customName = "SpawnAttackDelay";
                strafeOnSpawn.driverUpdateTimerOverride = 1.5f;
                strafeOnSpawn.ignoreNodeGraph = false;
                strafeOnSpawn.maxDistance = float.PositiveInfinity;
                strafeOnSpawn.maxTargetHealthFraction = float.PositiveInfinity;
                strafeOnSpawn.maxTimesSelected = 1;
                strafeOnSpawn.maxUserHealthFraction = float.PositiveInfinity;
                strafeOnSpawn.minDistance = 0;
                strafeOnSpawn.minTargetHealthFraction = float.NegativeInfinity;
                strafeOnSpawn.minUserHealthFraction = float.NegativeInfinity;
                strafeOnSpawn.moveInputScale = 1f;
                strafeOnSpawn.movementType = AISkillDriver.MovementType.StrafeMovetarget;
                strafeOnSpawn.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
                strafeOnSpawn.noRepeat = true;
                strafeOnSpawn.requireEquipmentReady = false;
                strafeOnSpawn.requireSkillReady = false;
                strafeOnSpawn.resetCurrentEnemyOnNextDriverSelection = false;
                strafeOnSpawn.selectionRequiresAimTarget = false;
                strafeOnSpawn.selectionRequiresOnGround = false;
                strafeOnSpawn.selectionRequiresTargetLoS = false;
                strafeOnSpawn.selectionRequiresTargetNonFlier = false;
                strafeOnSpawn.shouldFireEquipment = false;
                strafeOnSpawn.shouldSprint = false;
                strafeOnSpawn.skillSlot = SkillSlot.None;

                probeMasterPrefab.ReorderSkillDrivers(strafeOnSpawn, 0);
            }
        }
    }
}
