using Mono.Cecil.Cil;
using MonoMod.Cil;
using RoR2;
using System;

namespace SuperCyanTweaks
{
    class Eclipse
    {
        public Eclipse()
        {
            // Eclipse modifiers for non-monster teams
            if (Configs.eclipseTeamTweak.Value == true)
            {
                // E4
                IL.RoR2.CharacterBody.RecalculateStats += (il) =>
                {
                    ILCursor c = new(il);
                    bool hookFailed = true;

                    if (
                        c.TryGotoNext(MoveType.Before,
                        x => x.MatchLdsfld(typeof(DLC2Content.Buffs), nameof(DLC2Content.Buffs.ElusiveAntlersBuff))) &&
                        c.TryGotoNext(MoveType.Before,
                        x => x.MatchCallvirt("RoR2.Run", "get_selectedDifficulty")) &&
                        c.TryGotoPrev(MoveType.Before,
                        x => x.MatchCallvirt("RoR2.TeamComponent", "get_teamIndex")) &&
                        c.TryGotoNext(MoveType.Before,
                        x => x.MatchLdcI4(2))
                    )
                    {
                        c.Next.OpCode = OpCodes.Ldc_I4_1;
                        c.Index++;
                        c.Next.OpCode = OpCodes.Beq;

                        hookFailed = false;
                    }

                    if (hookFailed == true)
                    {
                        Log.Error("Eclipse 4 hook failed!");
                    }
                };

                // E7
                IL.RoR2.CharacterBody.RecalculateStats += (il) =>
                {
                    ILCursor c = new(il);
                    bool hookFailed = true;

                    if (
                        c.TryGotoNext(MoveType.Before,
                        x => x.MatchLdsfld(typeof(DroneWeaponsBoostBehavior), nameof(DroneWeaponsBoostBehavior.attackSpeedPerStack))) &&
                        c.TryGotoNext(MoveType.Before,
                        x => x.MatchCallvirt("RoR2.Run", "get_selectedDifficulty")) &&
                        c.TryGotoPrev(MoveType.Before,
                        x => x.MatchCallvirt("RoR2.TeamComponent", "get_teamIndex")) &&
                        c.TryGotoNext(MoveType.Before,
                        x => x.MatchLdcI4(2))
                    )
                    {
                        c.Next.OpCode = OpCodes.Ldc_I4_1;
                        c.Index++;
                        c.Next.OpCode = OpCodes.Beq;

                        hookFailed = false;
                    }

                    if (hookFailed == true)
                    {
                        Log.Error("Eclipse 7 hook failed!");
                    }
                };
            }
        }
    }
}
