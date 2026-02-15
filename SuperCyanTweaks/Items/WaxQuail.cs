using MonoMod.Cil;
using RoR2;

namespace SuperCyanTweaks
{
    public class WaxQuail
    {
        public WaxQuail()
        {
            // Make it work on multi-jumps
            if (Configs.waxQuailMultiJump.Value == true)
            {
                IL.EntityStates.GenericCharacterMain.ProcessJump_bool += (il) =>
                {
                    ILCursor c = new(il);
                    bool hookFailed = true;

                    if (
                        c.TryGotoNext(MoveType.Before,
                        x => x.MatchCallvirt("RoR2.CharacterBody", "get_isSprinting")) &&
                        c.TryGotoPrev(MoveType.Before,
                        x => x.MatchBr(out _))
                    )
                    {
                        c.Remove();
                        hookFailed = false;
                    }

                    if (hookFailed == true)
                    {
                        Log.Error("Wax Quail Multi Jump hook failed!");
                    }
                };
            }
        }
    }
}
