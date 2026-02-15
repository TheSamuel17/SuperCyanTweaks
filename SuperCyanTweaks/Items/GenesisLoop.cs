using Mono.Cecil.Cil;
using MonoMod.Cil;
using RoR2;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SuperCyanTweaks
{
    public class GenesisLoop
    {
        public static EntityStateConfiguration detonateState = Addressables.LoadAssetAsync<EntityStateConfiguration>("RoR2/Base/NovaOnLowHealth/EntityStates.VagrantNovaItem.DetonateState.asset").WaitForCompletion();

        public GenesisLoop()
        {
            // Adjust proc coefficient
            if (Configs.genesisLoopProcCoeff.Value >= 0)
            {
                detonateState.TryModifyFieldValue("blastProcCoefficient", Configs.genesisLoopProcCoeff.Value);
            }

            // Line of sight tweak
            if (Configs.genesisLoopLosTweak.Value == true)
            {
                bool hookFailed = true;
                IL.EntityStates.VagrantNovaItem.DetonateState.OnEnter += (il) =>
                {
                    ILCursor c = new(il);

                    if (
                        c.TryGotoNext(MoveType.After,
                        x => x.MatchStfld<BlastAttack>("losType"))
                    )
                    {
                        c.Emit(OpCodes.Ldarg_0);
                        c.EmitDelegate<Func<BlastAttack, EntityStates.VagrantNovaItem.DetonateState, BlastAttack>>((blast, state) =>
                        {
                            if (state.attachedBody && state.attachedBody.teamComponent && state.attachedBody.teamComponent.teamIndex == TeamIndex.Player)
                            {
                                blast.losType = BlastAttack.LoSType.None;
                            }
                            return blast;
                        });

                        hookFailed = false;
                    }

                    if (hookFailed == true)
                    {
                        Log.Error("Genesis Loop line of sight hook failed!");
                    }
                };
            }
        }
    }
}
