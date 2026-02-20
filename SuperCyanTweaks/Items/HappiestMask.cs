using RoR2;
using R2API;
using MonoMod.Cil;

namespace SuperCyanTweaks
{
    public class HappiestMask
    {
        public HappiestMask()
        {
            // Configurable proc chance
            if (Configs.happiestMaskProcChance.Value >= 0)
            {
                bool hookFailed = true;
                IL.RoR2.GlobalEventManager.OnCharacterDeath += (il) =>
                {
                    ILCursor c = new(il);

                    if (
                        c.TryGotoNext(MoveType.After,
                        x => x.MatchLdsfld(typeof(RoR2Content.Items), nameof(RoR2Content.Items.GhostOnKill))) &&
                        c.TryGotoNext(MoveType.Before,
                        x => x.MatchLdcR4(7f))
                    )
                    {
                        c.Next.Operand = Configs.happiestMaskProcChance.Value;
                        hookFailed = false;
                    }

                    if (hookFailed == true)
                    {
                        Log.Error("Happiest Mask Proc Chance hook failed!");
                    }
                };

                if (hookFailed == false)
                {
                    UpdateDescription(Configs.happiestMaskProcChance.Value);
                }
            }
        }

        private void UpdateDescription(float procChance)
        {
            string procChanceString = procChance.ToString().Replace(",", ".");

            LanguageAPI.Add("ITEM_GHOSTONKILL_DESC",
                $"Killing enemies has a <style=cIsDamage>{procChanceString}%</style> chance to <style=cIsDamage>spawn a ghost</style> of the killed enemy with <style=cIsDamage>1500%</style> damage. " +
                $"Lasts <style=cIsDamage>30s</style> <style=cStack>(+30s per stack)</style>.",
                "en"
            );

            LanguageAPI.Add("ITEM_GHOSTONKILL_DESC",
                $"En cas d'ennemi tué, vous avez <style=cIsDamage>{procChance} %</style> de chance de <style=cIsDamage>faire apparaître un fantôme</style> de votre victime qui inflige <style=cIsDamage>1500 %</style> de dégâts. " +
                $"Dure <style=cIsDamage>30 s</style> <style=cStack>(+30 s par cumul)</style>.",
                "fr"
            );

            LanguageAPI.Add("ITEM_GHOSTONKILL_DESC",
                $"击杀敌人时有<style=cIsDamage>{procChanceString}%</style>的几率<style=cIsDamage>生成该敌人的幽灵</style>，幽灵拥有<style=cIsDamage>1500%</style>伤害。" +
                $"持续<style=cIsDamage>30秒</style> <style=cStack>(每层堆叠+30秒)</style>。",
                "zh-CN"
            );
        }
    }
}
