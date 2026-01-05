using RoR2;
using RoR2.CharacterAI;
using R2API;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SuperCyanTweaks
{
    public class MiniMushrum
    {
        public static GameObject mushrumMasterPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/MiniMushroom/MiniMushroomMaster.prefab").WaitForCompletion();
        public static GameObject mushrumBodyPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/MiniMushroom/MiniMushroomBody.prefab").WaitForCompletion();
        public static CharacterSpawnCard cscMiniMushroom = Addressables.LoadAssetAsync<CharacterSpawnCard>("RoR2/Base/MiniMushroom/cscMiniMushroom.asset").WaitForCompletion();

        public MiniMushrum()
        {
            // Enable sprinting behavior
            if (Configs.mushrumSprintDist.Value >= 0)
            {
                AISkillDriver pathSprint = mushrumMasterPrefab.AddComponent<AISkillDriver>();
                pathSprint.activationRequiresAimConfirmation = false;
                pathSprint.activationRequiresAimTargetLoS = false;
                pathSprint.activationRequiresTargetLoS = false;
                pathSprint.aimType = AISkillDriver.AimType.AtMoveTarget;
                pathSprint.buttonPressType = AISkillDriver.ButtonPressType.Hold;
                pathSprint.customName = "PathSprint";
                pathSprint.driverUpdateTimerOverride = -1f;
                pathSprint.ignoreNodeGraph = false;
                pathSprint.maxDistance = float.PositiveInfinity;
                pathSprint.maxTargetHealthFraction = float.PositiveInfinity;
                pathSprint.maxTimesSelected = -1;
                pathSprint.maxUserHealthFraction = float.PositiveInfinity;
                pathSprint.minDistance = Configs.mushrumSprintDist.Value;
                pathSprint.minTargetHealthFraction = float.NegativeInfinity;
                pathSprint.minUserHealthFraction = float.NegativeInfinity;
                pathSprint.moveInputScale = 1f;
                pathSprint.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
                pathSprint.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
                pathSprint.noRepeat = false;
                pathSprint.requireEquipmentReady = false;
                pathSprint.requireSkillReady = false;
                pathSprint.resetCurrentEnemyOnNextDriverSelection = false;
                pathSprint.selectionRequiresAimTarget = false;
                pathSprint.selectionRequiresOnGround = false;
                pathSprint.selectionRequiresTargetLoS = false;
                pathSprint.selectionRequiresTargetNonFlier = false;
                pathSprint.shouldFireEquipment = false;
                pathSprint.shouldSprint = true;
                pathSprint.skillSlot = SkillSlot.None;

                mushrumMasterPrefab.ReorderSkillDrivers(pathSprint, 3);
            }

            // Adjust sprint speed multiplier
            if (Configs.mushrumSprintCoeff.Value >= 0)
            {
                CharacterBody body = mushrumBodyPrefab.GetComponent<CharacterBody>();
                if (body)
                {
                    body.sprintingSpeedMultiplier = Configs.mushrumSprintCoeff.Value;
                }
            }

            // Make them spawn on Golden Dieback
            if (Configs.mushrumOnHabitatFall.Value == true)
            {
                DirectorAPI.DirectorCardHolder mushroomSpawnCard = new()
                {
                    Card = new()
                    {
                        minimumStageCompletions = 0,
                        preventOverhead = false,
                        selectionWeight = 1,
                        spawnDistance = DirectorCore.MonsterSpawnDistance.Standard,
                        spawnCard = cscMiniMushroom,
                    },
                    MonsterCategory = DirectorAPI.MonsterCategory.BasicMonsters,
                };

                DirectorAPI.Helpers.AddNewMonsterToStage(mushroomSpawnCard, false, DirectorAPI.Stage.GoldenDieback);
            }
        }
    }
}
