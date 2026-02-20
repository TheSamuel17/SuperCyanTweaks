using Mono.Cecil.Cil;
using MonoMod.Cil;
using RoR2;
using System;

namespace SuperCyanTweaks
{
    public class FrostRelic
    {
        public FrostRelic()
        {
            // Allow it to crit
            if (Configs.frostRelicCrit.Value == true)
            {
                IL.RoR2.IcicleAuraController.FixedUpdate += (il) =>
                {
                    ILCursor c = new(il);
                    bool hookFailed = true;

                    if (
                        c.TryGotoNext(MoveType.Before,
                        x => x.MatchStfld<BlastAttack>("crit")) &&
                        c.TryGotoPrev(MoveType.Before,
                        x => x.MatchLdcI4(out _))
                    )
                    {
                        c.Remove();
                        c.Emit(OpCodes.Ldarg_0);
                        c.EmitDelegate<Func<IcicleAuraController, bool>>((self) =>
                        {
                            return self.cachedOwnerInfo.characterBody.RollCrit();
                        });

                        hookFailed = false;
                    }

                    if (hookFailed == true)
                    {
                        Log.Error("Frost Relic Crit hook failed!");
                    }
                };
            }
        }
    }
}
