using MonoMod.Cil;
using RoR2;
using System;
using UnityEngine.AddressableAssets;

namespace SuperCyanTweaks
{
    public class ResonanceDisc
    {
        public static EntityStateConfiguration chargeBeamState = Addressables.LoadAssetAsync<EntityStateConfiguration>("RoR2/Base/LaserTurbine/EntityStates.LaserTurbine.ChargeMainBeamState.asset").WaitForCompletion();

        public ResonanceDisc()
        {
            // Targeting fix
            if (Configs.resonanceDiscTargeting.Value == true)
            {
                IL.EntityStates.LaserTurbine.AimState.OnEnter += (il) =>
                {
                    ILCursor c = new(il);
                    bool hookFailed = true;

                    if (
                        c.TryGotoNext(MoveType.Before,
                        x => x.MatchCallOrCallvirt<TeamMask>(nameof(TeamMask.GetEnemyTeams))) &&
                        c.TryGotoPrev(MoveType.Before,
                        x => x.MatchCallOrCallvirt("EntityStates.LaserTurbine.LaserTurbineBaseState", "get_ownerBody"))
                    )
                    {
                        c.RemoveRange(4);
                        c.EmitDelegate<Func<EntityStates.LaserTurbine.LaserTurbineBaseState, TeamMask>>((baseState) =>
                        {
                            TeamMask teamMask = TeamMask.allButNeutral;
                            if (baseState.ownerBody && baseState.ownerBody.teamComponent)
                            {
                                teamMask.RemoveTeam(baseState.ownerBody.teamComponent.teamIndex);
                            }
                            return teamMask;
                        });

                        hookFailed = false;
                    }

                    if (hookFailed == true)
                    {
                        Log.Error("Resonance Disc targeting hook failed!");
                    }
                };
            }

            if (Configs.resonanceDiscAccuracy.Value == true)
            {
                chargeBeamState.TryModifyFieldValue("baseDuration", 0f);

                On.EntityStates.LaserTurbine.LaserTurbineBaseState.OnEnter += (orig, self) =>
                {
                    orig(self);

                    SimpleRotateToDirection rotate = self.GetComponent<SimpleRotateToDirection>();
                    if (rotate)
                    {
                        rotate.maxRotationSpeed = float.PositiveInfinity;
                        rotate.smoothTime = .001f;
                    }
                };
            }
        }
    }
}
