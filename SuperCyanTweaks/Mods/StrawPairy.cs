using RoR2;
using UnityEngine;
using RoR2.CharacterAI;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Linq;

namespace SuperCyanTweaks
{
    public class StrawPairy
    {
        public static float sqrAttackDistance = 441f;
        
        public StrawPairy()
        {
            // AI tweaks
            if (Configs.strawPairyAITweak.Value == true)
            {
                RoR2Application.onLoad += () =>
                {
                    RegisterModifiedGup();
                };
            }

            // FoodRelated tag
            if (Configs.strawPairyIsFood.Value == true)
            {
                ItemCatalog.availability.CallWhenAvailable(delegate()
                {
                    ItemDef strawPairy = ItemCatalog.GetItemDef(ItemCatalog.FindItemIndex("SEEKINTHEVOID_STRAWPAIRY_NAME"));
                    if (strawPairy)
                    {
                        strawPairy.tags = strawPairy.tags.AddToArray(ItemTag.FoodRelated);
                    }
                });
            }
        }

        private void RegisterModifiedGup()
        {
            GameObject modifiedGupMasterPrefab = MasterCatalog.FindMasterPrefab("modifiedGupMaster");
            GameObject modifiedGupBodyPrefab = BodyCatalog.FindBodyPrefab("modifiedGupBody");
            
            if (modifiedGupMasterPrefab)
            {
                AdjustSkillDrivers(modifiedGupMasterPrefab);
            }
            else if (modifiedGupBodyPrefab)
            {
                // Absent from MasterCatalog. Gotta make do.
                CharacterMaster.onCharacterMasterDiscovered += (master) =>
                {
                    if (master.bodyPrefab == modifiedGupBodyPrefab)
                    {
                        AdjustSkillDrivers(master.gameObject);
                    }
                };
            }
        }

        private void AdjustSkillDrivers(GameObject masterObject)
        {
            if (masterObject)
            {
                // Component that will make it make it *prioritize*, but not exclusively target grounded enemies
                masterObject.AddComponent<ModifiedGupController>();

                AISkillDriver[] skillDrivers = masterObject.GetComponents<AISkillDriver>();
                foreach (AISkillDriver skillDriver in skillDrivers)
                {
                    // Gup is explicitly told not to follow and attack flying targets, but that doesn't stop it from *targeting* them. As a fallback, it just idles near you.
                    skillDriver.selectionRequiresTargetNonFlier = false;

                    switch (skillDriver.customName)
                    {
                        case "FollowNodeGraphToTarget":
                            skillDriver.moveTargetType = AISkillDriver.TargetType.Custom; // Allow it to use the component's targeting
                            break;

                        case "Spike":
                            skillDriver.moveTargetType = AISkillDriver.TargetType.Custom; // Allow it to use the component's targeting
                            skillDriver.maxDistance *= .7f; // Reduce the amount of "just out of reach" moments
                            sqrAttackDistance = Mathf.Pow(skillDriver.maxDistance, 2f);
                            break;
                    }
                }
            }
        } 
    }

    public class ModifiedGupController : MonoBehaviour
    {
        public static float updateInterval = 1f; // Every 1s is sufficient
        public static float timer = 0f;

        private BaseAI ai;
        private BullseyeSearch enemySearch;

        private void Awake()
        {
            if (NetworkServer.active)
            {
                enemySearch = new BullseyeSearch();
            }
        }

        private void Start()
        {
            ai = gameObject.GetComponent<BaseAI>();
        }

        private void FixedUpdate()
        {
            if (ai && ai.customTarget != null)
            {
                if (ai.customTarget.gameObject != null)
                {
                    HurtBox bestHurtBox = ai.GetBestHurtBox(ai.customTarget.gameObject);
                    if (bestHurtBox)
                    {
                        ai.customTarget.lastKnownBullseyePosition = ai.GetBestHurtBox(ai.customTarget.gameObject).transform.position;
                        ai.customTarget.lastKnownBullseyePositionTime = Run.FixedTimeStamp.now;
                    }
                    else
                    {
                        ai.customTarget.lastKnownBullseyePosition = null;
                        ai.customTarget.lastKnownBullseyePositionTime = Run.FixedTimeStamp.negativeInfinity;
                    }
                }
                else
                {
                    ai.customTarget.lastKnownBullseyePosition = null;
                    ai.customTarget.lastKnownBullseyePositionTime = Run.FixedTimeStamp.negativeInfinity;
                }
            }
            
            timer += Time.fixedDeltaTime;
            if (timer < updateInterval) return;
            timer -= updateInterval;

            for (int i = 0; i < 2; i++)
            {
                if (!ai) return;
                if (!ai.body) return;
                if (!ai.bodyInputBank) return;
                if (ai.customTarget == null) return;
                if (i == 1 && ai.customTarget.gameObject != null) return;

                Ray aimRay = ai.bodyInputBank.GetAimRay();

                enemySearch.viewer = ai.body;
                enemySearch.teamMaskFilter = TeamMask.allButNeutral;
                enemySearch.teamMaskFilter.RemoveTeam(ai.master.teamIndex);
                enemySearch.sortMode = BullseyeSearch.SortMode.Distance;
                enemySearch.minDistanceFilter = 0f;
                enemySearch.maxDistanceFilter = float.PositiveInfinity;
                enemySearch.searchOrigin = ai.bodyInputBank.aimOrigin;
                enemySearch.searchDirection = ai.bodyInputBank.aimDirection;
                enemySearch.maxAngleFilter = 360f;
                enemySearch.filterByLoS = false;
                enemySearch.RefreshCandidates();

                IEnumerable<HurtBox> results;
                if (i == 0)
                {
                    results = enemySearch.GetResults().Where(TargetPassesFilters);
                }
                else
                {
                    results = enemySearch.GetResults();
                }

                HurtBox hurtBox = results.FirstOrDefault();
                if (hurtBox && hurtBox.healthComponent)
                {
                    ai.customTarget._gameObject = hurtBox.healthComponent.gameObject;
                    ai.customTarget.bestHurtBox = hurtBox;
                    ai.customTarget.unset = false;
                }
                else
                {
                    ai.customTarget._gameObject = null;
                    ai.customTarget.bestHurtBox = null;
                    ai.customTarget.unset = true;
                }
            }
        }

        private bool TargetPassesFilters(HurtBox arg)
        {
            // Filter out flying targets farther than Gup's attack range
            if (arg.healthComponent && arg.healthComponent.body)
            {
                return (!arg.healthComponent.body.isFlying || (arg.transform.position - enemySearch.searchOrigin).sqrMagnitude <= StrawPairy.sqrAttackDistance);
            }

            return false;
        }
    }
}
