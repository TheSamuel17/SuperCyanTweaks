using RoR2;
using RoR2.CharacterAI;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SuperCyanTweaks
{
    public class Gup
    {
        public static GameObject gupMasterPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/Gup/GupMaster.prefab").WaitForCompletion();

        public Gup()
        {
            // Gup will move around a little before attacking
            if (Configs.gupDelayAttackOnSpawn.Value == true)
            {
                AISkillDriver pathOnSpawn = gupMasterPrefab.AddComponent<AISkillDriver>();
                pathOnSpawn.activationRequiresAimConfirmation = false;
                pathOnSpawn.activationRequiresAimTargetLoS = false;
                pathOnSpawn.activationRequiresTargetLoS = false;
                pathOnSpawn.aimType = AISkillDriver.AimType.AtMoveTarget;
                pathOnSpawn.buttonPressType = AISkillDriver.ButtonPressType.Abstain;
                pathOnSpawn.customName = "SpawnAttackDelay";
                pathOnSpawn.driverUpdateTimerOverride = 1f;
                pathOnSpawn.ignoreNodeGraph = false;
                pathOnSpawn.maxDistance = float.PositiveInfinity;
                pathOnSpawn.maxTargetHealthFraction = float.PositiveInfinity;
                pathOnSpawn.maxTimesSelected = 1;
                pathOnSpawn.maxUserHealthFraction = float.PositiveInfinity;
                pathOnSpawn.minDistance = 0;
                pathOnSpawn.minTargetHealthFraction = float.NegativeInfinity;
                pathOnSpawn.minUserHealthFraction = float.NegativeInfinity;
                pathOnSpawn.moveInputScale = 1f;
                pathOnSpawn.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
                pathOnSpawn.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
                pathOnSpawn.noRepeat = true;
                pathOnSpawn.requireEquipmentReady = false;
                pathOnSpawn.requireSkillReady = false;
                pathOnSpawn.resetCurrentEnemyOnNextDriverSelection = false;
                pathOnSpawn.selectionRequiresAimTarget = false;
                pathOnSpawn.selectionRequiresOnGround = false;
                pathOnSpawn.selectionRequiresTargetLoS = false;
                pathOnSpawn.selectionRequiresTargetNonFlier = false;
                pathOnSpawn.shouldFireEquipment = false;
                pathOnSpawn.shouldSprint = false;
                pathOnSpawn.skillSlot = SkillSlot.None;

                gupMasterPrefab.ReorderSkillDrivers(pathOnSpawn, 0);
            }
        }
    }
}
