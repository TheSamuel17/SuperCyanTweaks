using RoR2;
using RoR2.CharacterAI;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SuperCyanTweaks
{
    public class Lemurian
    {
        public static GameObject lemurianMasterPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Lemurian/LemurianMaster.prefab").WaitForCompletion();

        public Lemurian()
        {
            // Adjust range on fireball attack
            if (Configs.lemurianFireballRange.Value >= 0)
            {
                if (Configs.lemurianFireballRange.Value > 30)
                {
                    AISkillDriver chasePrimary = lemurianMasterPrefab.AddComponent<AISkillDriver>();
                    chasePrimary.activationRequiresAimConfirmation = false;
                    chasePrimary.activationRequiresAimTargetLoS = false;
                    chasePrimary.activationRequiresTargetLoS = true;
                    chasePrimary.aimType = AISkillDriver.AimType.AtMoveTarget;
                    chasePrimary.buttonPressType = AISkillDriver.ButtonPressType.Hold;
                    chasePrimary.customName = "ChasePrimary";
                    chasePrimary.driverUpdateTimerOverride = -1f;
                    chasePrimary.ignoreNodeGraph = false;
                    chasePrimary.maxDistance = Configs.lemurianFireballRange.Value;
                    chasePrimary.maxTargetHealthFraction = float.PositiveInfinity;
                    chasePrimary.maxTimesSelected = -1;
                    chasePrimary.maxUserHealthFraction = float.PositiveInfinity;
                    chasePrimary.minDistance = 15f;
                    chasePrimary.minTargetHealthFraction = float.NegativeInfinity;
                    chasePrimary.minUserHealthFraction = float.NegativeInfinity;
                    chasePrimary.moveInputScale = 1f;
                    chasePrimary.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
                    chasePrimary.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
                    chasePrimary.noRepeat = false;
                    chasePrimary.requireEquipmentReady = false;
                    chasePrimary.requireSkillReady = false;
                    chasePrimary.resetCurrentEnemyOnNextDriverSelection = false;
                    chasePrimary.selectionRequiresAimTarget = false;
                    chasePrimary.selectionRequiresOnGround = false;
                    chasePrimary.selectionRequiresTargetLoS = true;
                    chasePrimary.selectionRequiresTargetNonFlier = false;
                    chasePrimary.shouldFireEquipment = false;
                    chasePrimary.shouldSprint = false;
                    chasePrimary.skillSlot = SkillSlot.Primary;

                    lemurianMasterPrefab.ReorderSkillDrivers(chasePrimary, 4);
                }
                else
                {
                    AISkillDriver[] skillDrivers = lemurianMasterPrefab.GetComponents<AISkillDriver>();
                    foreach (AISkillDriver skillDriver in skillDrivers)
                    {
                        if (skillDriver.skillSlot == SkillSlot.Primary)
                        {
                            skillDriver.maxDistance = Configs.lemurianFireballRange.Value;
                        }
                    }
                }
            }
        }
    }
}
