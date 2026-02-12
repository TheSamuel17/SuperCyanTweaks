using MonoMod.Cil;

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
                        c.TryGotoNext(MoveType.After,
                        x => x.MatchLdcR4(1.5f)
                    ))
                    {
                        if (
                            c.TryGotoNext(MoveType.After,
                            x => x.MatchLdcR4(1.5f)
                        ))
                        {
                            c.Index += 1;
                            c.Remove();
                            hookFailed = false;
                        }
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
