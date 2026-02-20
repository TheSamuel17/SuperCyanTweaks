using RoR2;
using R2API;
using MonoMod.Cil;
using System;
using UnityEngine.AddressableAssets;

namespace SuperCyanTweaks
{
    public class OldWarStealthkit
    {
        public static ItemDef stealthkit = Addressables.LoadAssetAsync<ItemDef>("RoR2/Base/Phasing/Phasing.asset").WaitForCompletion();

        public OldWarStealthkit() 
        {
            bool updated = false;

            // Configurable health threshold
            if (Configs.stealthKitThreshold.Value >= 0)
            {
                bool hookFailed = true;
                IL.RoR2.Items.PhasingBodyBehavior.FixedUpdate += (il) => // Brought to you by WolfoItemBuffs
                {
                    ILCursor c = new(il);

                    if (
                        c.TryGotoNext(MoveType.Before,
                        x => x.MatchCallvirt("RoR2.HealthComponent", "get_isHealthLow"))
                    )
                    {
                        c.Remove();
                        c.EmitDelegate<Func<HealthComponent, bool>>((health) =>
                        {
                            return health.IsHealthBelowThreshold(Configs.stealthKitThreshold.Value);
                        });

                        hookFailed = false;
                        updated = true;
                    }

                    if (hookFailed == true)
                    {
                        Log.Error("Old War Stealthkit threshold hook failed!");
                    }
                };
            }

            if (updated == true)
            {
                if (Configs.stealthKitThreshold.Value != .25f)
                {
                    stealthkit.tags = stealthkit.tags.RemoveFromArray(ItemTag.LowHealth);
                }

                UpdateDescription(Configs.stealthKitThreshold.Value * 100f);
            }
        }

        private void UpdateDescription(float threshold)
        {
            string thresholdString = threshold.ToString().Replace(",", ".");

            LanguageAPI.Add("ITEM_PHASING_DESC",
                $"Falling below <style=cIsHealth>{thresholdString}% health</style> causes you to gain <style=cIsUtility>40% movement speed</style> and <style=cIsUtility>invisibility</style> for <style=cIsUtility>5s</style>. " +
                $"Recharges every <style=cIsUtility>30 seconds</style> <style=cStack>(-50% per stack)</style>.",
                "en"
            );

            LanguageAPI.Add("ITEM_PHASING_DESC",
                $"Tomber en dessous de <style=cIsHealth>{threshold} % de vos PV</style> vous fait gagner <style=cIsUtility>40 % de vitesse de déplacement</style> et vous rend <style=cIsUtility>invisible</style> pendant <style=cIsUtility>5 s</style>. " +
                $"Se recharge toutes les <style=cIsUtility>30 secondes</style> <style=cStack>(-50 % par cumul)</style>.",
                "fr"
            );

            LanguageAPI.Add("ITEM_PHASING_DESC",
                $"生命值低于<style=cIsHealth>{thresholdString}%</style>时，获得<style=cIsUtility>40%移动速度</style>和<style=cIsUtility>隐身</style>，持续<style=cIsUtility>5秒</style>。" +
                $"冷却时间<style=cIsUtility>30秒</style> <style=cStack>(每层堆叠-50%)</style>。",
                "zh-CN"
            );
        }
    }
}
