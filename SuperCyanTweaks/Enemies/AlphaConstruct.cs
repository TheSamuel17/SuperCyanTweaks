using EntityStates;
using RoR2;
using RoR2.Skills;
using RoR2.Navigation;
using RoR2.CharacterAI;
using R2API;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System;
using System.Collections.Generic;

namespace SuperCyanTweaks
{
    public class AlphaConstruct
    {
        // Asset references
        public static GameObject minorConstructBodyPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/MajorAndMinorConstruct/MinorConstructBody.prefab").WaitForCompletion();
        public static GameObject minorConstructMasterPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/MajorAndMinorConstruct/MinorConstructMaster.prefab").WaitForCompletion();

        public static GameObject minorConstructAllyBodyPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/MajorAndMinorConstruct/MinorConstructOnKillBody.prefab").WaitForCompletion();
        public static GameObject minorConstructAllyMasterPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/MajorAndMinorConstruct/MinorConstructOnKillMaster.prefab").WaitForCompletion();

        public AlphaConstruct()
        {
            // Implement teleporting skill
            if (Configs.alphaConstructTeleportEnabled.Value == true)
            {
                // Necessary stat adjustments
                BaseAI ai = minorConstructMasterPrefab.GetComponent<BaseAI>();
                if (ai)
                {
                    ai.enemySearch.maxDistanceFilter = float.PositiveInfinity;
                    ai.xrayVision = true;
                }

                // Necessary component
                if (!minorConstructBodyPrefab.GetComponent<TargetingAndPredictionController>())
                {
                    minorConstructBodyPrefab.AddComponent<TargetingAndPredictionController>();
                }

                // Register EntityState
                ContentAddition.AddEntityState<MinorConstructTeleport>(out _);

                // Create SkillDef
                SkillDef teleportSkillDef = ScriptableObject.CreateInstance<SkillDef>();

                teleportSkillDef.activationState = new SerializableEntityStateType(typeof(MinorConstructTeleport));
                teleportSkillDef.activationStateMachineName = "Body";
                teleportSkillDef.baseMaxStock = 1;
                teleportSkillDef.baseRechargeInterval = Configs.alphaConstructTeleportCooldown.Value;
                teleportSkillDef.beginSkillCooldownOnSkillEnd = false;
                teleportSkillDef.canceledFromSprinting = false;
                teleportSkillDef.cancelSprintingOnActivation = true;
                teleportSkillDef.dontAllowPastMaxStocks = true;
                teleportSkillDef.forceSprintDuringState = false;
                teleportSkillDef.fullRestockOnAssign = false;
                teleportSkillDef.hideCooldown = false;
                teleportSkillDef.hideStockCount = false;
                teleportSkillDef.interruptPriority = InterruptPriority.Skill;
                teleportSkillDef.isCombatSkill = false;
                teleportSkillDef.mustKeyPress = false;
                teleportSkillDef.rechargeStock = 1;
                teleportSkillDef.requiredStock = 1;
                teleportSkillDef.resetCooldownTimerOnUse = false;
                teleportSkillDef.skillName = "MINORCONSTRUCT_UTILITY_TELEPORT_NAME";
                teleportSkillDef.stockToConsume = 1;

                ContentAddition.AddSkillDef(teleportSkillDef);

                // Create new SkillFamily
                var utilFamily = ScriptableObject.CreateInstance<SkillFamily>();
                (utilFamily as ScriptableObject).name = "MinorConstructUtilityFamily";
                Array.Resize(ref utilFamily.variants, 1);
                utilFamily.variants[0].skillDef = teleportSkillDef;

                var skill = minorConstructBodyPrefab.AddComponent<GenericSkill>();
                skill.skillName = "MinorConstructTeleport";
                skill._skillFamily = utilFamily;

                var loc = minorConstructBodyPrefab.GetComponent<SkillLocator>();
                loc.utility = skill;

                ContentAddition.AddSkillFamily(utilFamily);

                // Create skill driver
                AISkillDriver teleport = minorConstructMasterPrefab.AddComponent<AISkillDriver>();
                teleport.activationRequiresAimConfirmation = false;
                teleport.activationRequiresAimTargetLoS = false;
                teleport.activationRequiresTargetLoS = false;
                teleport.aimType = AISkillDriver.AimType.AtMoveTarget;
                teleport.aimVectorDampTimeOverride = .05f;
                teleport.aimVectorMaxSpeedOverride = float.PositiveInfinity;
                teleport.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;
                teleport.customName = "TeleportToTarget";
                teleport.driverUpdateTimerOverride = 1f;
                teleport.ignoreNodeGraph = false;
                teleport.maxDistance = float.PositiveInfinity;
                teleport.maxTargetHealthFraction = float.PositiveInfinity;
                teleport.maxTimesSelected = -1;
                teleport.maxUserHealthFraction = float.PositiveInfinity;
                teleport.minDistance = Configs.alphaConstructTeleportMinDist.Value;
                teleport.minTargetHealthFraction = float.NegativeInfinity;
                teleport.minUserHealthFraction = float.NegativeInfinity;
                teleport.moveInputScale = 1f;
                teleport.movementType = AISkillDriver.MovementType.Stop;
                teleport.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
                teleport.noRepeat = false;
                teleport.requireEquipmentReady = false;
                teleport.requireSkillReady = true;
                teleport.resetCurrentEnemyOnNextDriverSelection = false;
                teleport.selectionRequiresAimTarget = false;
                teleport.selectionRequiresOnGround = false;
                teleport.selectionRequiresTargetLoS = false;
                teleport.selectionRequiresTargetNonFlier = false;
                teleport.shouldFireEquipment = false;
                teleport.shouldSprint = false;
                teleport.skillSlot = SkillSlot.Utility;

                minorConstructMasterPrefab.ReorderSkillDrivers(teleport, 1);

                // Component to remove stocks on start
                if (!minorConstructBodyPrefab.GetComponent<RemoveTeleportStocksOnStart>())
                {
                    minorConstructBodyPrefab.AddComponent<RemoveTeleportStocksOnStart>();
                }
            }
        }
    }

    public class RemoveTeleportStocksOnStart : MonoBehaviour
    {
        CharacterBody characterBody;
        SkillLocator skillLocator;
        
        private void Awake()
        {
            characterBody = GetComponent<CharacterBody>();
            if (characterBody && characterBody.skillLocator)
            {
                skillLocator = characterBody.skillLocator;
            }
        }

        private void Start()
        {
            if (skillLocator && skillLocator.utility)
            {
                skillLocator.utility.RemoveAllStocks();
            }
        }
    }

    public class MinorConstructTeleport : BaseState
    {
        public float duration = .2f;
        public float nodeSearchRadiusMin = 25f;
        public float nodeSearchRadiusMax = 40f;
        public float collisionCheckDistance = 5f;

        public int findSurfaceRaycastCount = 10;
        public float findSurfaceMaxRaycastLength = 50f;

        public GameObject teleportEffectPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/MajorAndMinorConstruct/MajorConstructSpawnMinorConstructEffect.prefab").WaitForCompletion();
        public GameObject arrivalEffectPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/MajorAndMinorConstruct/MajorConstructMuzzleflashSpawnMinorConstruct.prefab").WaitForCompletion();
        public string arrivalSoundString = "Play_majorConstruct_R_pulse";

        public HurtBox target;

        public override void OnEnter()
        {
            base.OnEnter();
            target = GetComponent<TargetingAndPredictionController>().GetTargetHurtBox();
            if (!target)
            {
                outer.SetNextStateToMain();
            }
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if ((bool)target && base.fixedAge > duration)
            {
                Teleport();
                outer.SetNextStateToMain();
            }
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        private void Teleport()
        {
            if (FindOffsetPosition(target.transform, out var offsetPosition))
            {
                Vector3 initialPos = base.transform.position;
                
                bool foundSurface = FindNearbySurface(offsetPosition);
                if (foundSurface == true)
                {
                    EffectManager.SpawnEffect(teleportEffectPrefab, new EffectData
                    {
                        origin = initialPos
                    }, transmit: false);

                    EffectManager.SpawnEffect(arrivalEffectPrefab, new EffectData
                    {
                        origin = base.transform.position
                    }, transmit: false);

                    Util.PlaySound(arrivalSoundString, gameObject);
                }
            }
        }

        private bool FindNearbySurface(Vector3 targetPos)
        {
            RaycastHit hitInfo = default(RaycastHit);
            Vector3 corePosition = targetPos;
            bool found = false;

            if (base.isAuthority && !(base.characterBody.master.minionOwnership?.ownerMaster))
            {
                for (int i = 0; i < findSurfaceRaycastCount; i++)
                {
                    if (Physics.Raycast(corePosition, UnityEngine.Random.onUnitSphere, out hitInfo, findSurfaceMaxRaycastLength, LayerIndex.world.mask))
                    {
                        base.transform.position = hitInfo.point;
                        base.transform.up = hitInfo.normal;
                        found = true;
                    }
                }
            }

            return found;
        }

        private bool FindOffsetPosition(Transform targetTransform, out Vector3 offsetPosition)
        {
            return FindBestTeleportPosition(targetTransform.position, out offsetPosition);
        }

        private bool FindBestTeleportPosition(Vector3 nearPosition, out Vector3 teleportPosition)
        {
            if (!DirectorCore.instance || !SceneInfo.instance)
            {
                teleportPosition = default(Vector3);
                return false;
            }
            NodeGraph nodeGraph = SceneInfo.instance.GetNodeGraph(MapNodeGroup.GraphType.Air);
            List<NodeGraph.NodeIndex> list = nodeGraph.FindNodesInRange(nearPosition, nodeSearchRadiusMin, nodeSearchRadiusMax, HullMask.Human);
            if (list.Count >= 1)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    NodeGraph.NodeIndex nodeIndex = list[i];
                    if (!DirectorCore.instance.CheckNodeOccupied(nodeGraph, nodeIndex) && nodeGraph.GetNodePosition(nodeIndex, out var position) && (!(collisionCheckDistance >= 0f) || CheckTeleportPositionValid(position)))
                    {
                        teleportPosition = position;
                        return true;
                    }
                }
            }
            teleportPosition = default(Vector3);
            return false;
        }

        private bool CheckTeleportPositionValid(Vector3 telePosition)
        {
            if (Physics.OverlapSphere(telePosition, collisionCheckDistance, LayerIndex.CommonMasks.allCharacterCollisions, QueryTriggerInteraction.Ignore).Length != 0)
            {
                return false;
            }
            return true;
        }
    }
}
