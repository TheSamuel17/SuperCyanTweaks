using RoR2;
using RoR2.CharacterAI;
using UnityEngine;
using UnityEngine.AddressableAssets;
using MonoMod.Cil;
using System;
using Mono.Cecil.Cil;

namespace SuperCyanTweaks
{
    public class SolusTransporter
    {
        public static CharacterSpawnCard cscIronHauler = Addressables.LoadAssetAsync<CharacterSpawnCard>("RoR2/DLC3/IronHauler/cscIronHauler.asset").WaitForCompletion();
        public static GameObject ironHaulerBodyPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC3/IronHauler/IronHaulerBody.prefab").WaitForCompletion();

        public SolusTransporter()
        {
            // Adjust director credit cost
            if (Configs.transporterCost.Value >= 0)
            {
                cscIronHauler.directorCreditCost = Configs.transporterCost.Value;
            }

            // Targeting fix
            if (Configs.transporterTargetingFix.Value == true)
            {
                bool hookFailed = true;
                IL.RoR2.Scripts.GameBehaviors.IronHaulerController.FixedUpdate += (il) =>
                {
                    ILCursor c = new(il);

                    if (
                        c.TryGotoNext(MoveType.Before,
                        x => x.MatchCallOrCallvirt<TeamMask>(nameof(TeamMask.GetEnemyTeams))) &&
                        c.TryGotoPrev(MoveType.Before,
                        x => x.MatchLdfld<RoR2.Scripts.GameBehaviors.IronHaulerController>(nameof(RoR2.Scripts.GameBehaviors.IronHaulerController.ai)))
                    )
                    {
                        c.RemoveRange(4);
                        c.EmitDelegate<Func<RoR2.Scripts.GameBehaviors.IronHaulerController, TeamMask>>((controller) =>
                        {
                            TeamMask teamMask = TeamMask.allButNeutral;
                            if (controller.ai && controller.ai.master)
                            {
                                teamMask.RemoveTeam(controller.ai.master.teamIndex);
                            }
                            return teamMask;
                        });

                        hookFailed = false;
                    }

                    if (hookFailed == true)
                    {
                        Log.Error("Solus Transporter targeting hook failed!");
                    }
                };

                ironHaulerBodyPrefab.AddComponent<IronHaulerRetargetComponent>();
            }
        }
    }

    public class IronHaulerRetargetComponent : MonoBehaviour
    {
        private CharacterBody body;
        private BaseAI ai;

        private float retargetInterval = 30f;
        public float age = 0;

        private void Start()
        {
            body = GetComponent<CharacterBody>();
            if (body && body.masterObject)
            {
                ai = body.master.GetComponent<BaseAI>();
            }
        }

        private void FixedUpdate()
        {
            age += Time.fixedDeltaTime;
            if (age >= retargetInterval)
            {
                age -= retargetInterval;
                
                if (ai.customTarget.gameObject != null)
                {
                    ai.customTarget.gameObject = null;
                }
            }
        }
    }
}
