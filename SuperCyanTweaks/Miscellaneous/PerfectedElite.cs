using Mono.Cecil.Cil;
using MonoMod.Cil;
using RoR2;
using System;
using UnityEngine.AddressableAssets;

namespace SuperCyanTweaks
{
    public class PerfectedElite
    {
        public static EliteDef edLunar = Addressables.LoadAssetAsync<EliteDef>("RoR2/Base/EliteLunar/edLunar.asset").WaitForCompletion();

        public PerfectedElite()
        {
            // Cripple duration scales with proc coefficient
            if (Configs.perfectedCrippleProcCoeff.Value == true)
            {
                IL.RoR2.HealthComponent.TakeDamageProcess += (il) =>
                {
                    ILCursor c = new(il);
                    bool hookFailed = true;

                    if (
                        c.TryGotoNext(MoveType.Before,
                        x => x.MatchLdsfld(typeof(RoR2Content.Buffs), nameof(RoR2Content.Buffs.Cripple))) &&
                        c.TryGotoNext(MoveType.After,
                        x => x.MatchLdcR4(out _))
                    )
                    {
                        c.Emit(OpCodes.Ldarg_1);
                        c.EmitDelegate<Func<float, DamageInfo, float>>((duration, damageInfo) =>
                        {
                            return duration * damageInfo.procCoefficient;
                        });

                        hookFailed = false;
                    }

                    if (hookFailed == true)
                    {
                        Log.Error("Cripple duration hook failed!");
                    }
                };
            }

            // Adjust health multiplier
            if (Configs.perfectedHealthMultiplier.Value >= 0)
            {
                edLunar.healthBoostCoefficient = Configs.perfectedHealthMultiplier.Value;
            }
        }
    }
}
