using RoR2;
using System;
using UnityEngine.AddressableAssets;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using RoR2.CharacterAI;

namespace SuperCyanTweaks
{
    public class HalcyonSeed
    {
        public static GameObject titanGoldBodyPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Titan/TitanGoldBody.prefab").WaitForCompletion();
        public static GameObject titanGoldAllyMasterPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Titan/TitanGoldAllyMaster.prefab").WaitForCompletion();

        public HalcyonSeed()
        {
            if (Configs.allyAurelioniteTaunt.Value == true)
            {
                if (!titanGoldBodyPrefab.GetComponent<TitanGoldAllyTauntComponent>())
                {
                    titanGoldBodyPrefab.AddComponent<TitanGoldAllyTauntComponent>();
                }
            }

            if (Configs.allyAurelioniteMinLaserRange.Value >= 0)
            {
                AISkillDriver[] skillDrivers = titanGoldAllyMasterPrefab.GetComponents<AISkillDriver>();
                foreach (AISkillDriver skillDriver in skillDrivers)
                {
                    if (skillDriver.skillSlot == SkillSlot.Special)
                    {
                        skillDriver.minDistance = Configs.allyAurelioniteMinLaserRange.Value;
                    }
                }
            }
        }
    }

    public class TitanGoldAllyTauntComponent : MonoBehaviour
    {
        public CharacterBody body;

        private float distractDelay = 1f;
        public float age = 0;

        private void Awake()
        {
            body = GetComponent<CharacterBody>();
        }

        private void OnEnable()
        {
            if (body.master && body.master.masterIndex != MasterCatalog.FindMasterIndex(HalcyonSeed.titanGoldAllyMasterPrefab)) // Only ally Aurelionite
            {
                Destroy(this);
            }

            GlobalEventManager.onServerDamageDealt += OnServerDamageDealt;
        }

        private void FixedUpdate()
        {
            if (age < distractDelay)
            {
                age += Time.fixedDeltaTime;
                if (age >= distractDelay)
                {
                    TauntRadius();
                }
            }
        }

        private void TauntRadius()
        {
            BullseyeSearch search = new BullseyeSearch();

            search.teamMaskFilter = TeamMask.allButNeutral;
            search.teamMaskFilter.RemoveTeam(body.teamComponent.teamIndex);

            search.filterByLoS = false;
            search.searchOrigin = body.corePosition;
            search.sortMode = BullseyeSearch.SortMode.None;
            search.maxDistanceFilter = Configs.allyAurelioniteTauntRange.Value;
            search.maxAngleFilter = 360f;
            search.searchDirection = Vector3.up;
            search.RefreshCandidates();

            List<HurtBox> list = search.GetResults().ToList();

            foreach (HurtBox target in list)
            {
                if (target.healthComponent && target.healthComponent.body)
                {
                    CharacterBody targetBody = target.healthComponent.body;
                    if (targetBody.master)
                    {
                        BaseAI ai = targetBody.master.GetComponent<BaseAI>();
                        if (ai)
                        {
                            Distract(ai);
                        }
                    }
                }
            }
        }

        private void OnServerDamageDealt(DamageReport report)
        {
            if (report.damageDealt <= 0) return; // Ignore if damage dealt is 0
            if (report.attackerBody != body) return;

            if (!report.victimBody) return;
            if (!report.victimMaster) return;

            BaseAI ai = report.victimMaster.GetComponent<BaseAI>();
            if (ai)
            {
                Distract(ai);
            }
        }

        private void Distract(BaseAI ai)
        {
            if (body && body.gameObject)
            {
                ai.currentEnemy.gameObject = body.gameObject;
                ai.enemyAttention = Math.Max(Configs.allyAurelioniteTauntDuration.Value, ai.enemyAttentionDuration);
            }
        }
    }
}