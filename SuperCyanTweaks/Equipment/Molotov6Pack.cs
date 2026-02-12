using MonoMod.Cil;
using R2API;
using RoR2;
using RoR2.Projectile;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SuperCyanTweaks
{
    public class Molotov6Pack
    {
        public static GameObject molotovDotZone = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/Molotov/MolotovProjectileDotZone.prefab").WaitForCompletion();
        public static GameObject molotovClusterProjectile = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/Molotov/MolotovClusterProjectile.prefab").WaitForCompletion();
        public static GameObject molotovSingleProjectile = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/Molotov/MolotovSingleProjectile.prefab").WaitForCompletion();

        public Molotov6Pack()
        {
            float tickRate = 1f;
            float explosionDamage = 2.5f;
            
            // Adjust fire pool size
            if (Configs.molotovResidueSize.Value >= 0)
            {
                molotovDotZone.GetComponent<ProjectileDotZone>().gameObject.transform.localScale *= Configs.molotovResidueSize.Value;
            }

            // Adjust fire pool duration
            if (Configs.molotovResidueDuration.Value >= 0)
            {
                molotovDotZone.GetComponent<ProjectileDotZone>().lifetime = Configs.molotovResidueDuration.Value;
            }

            // Adjust fire pool tickrate
            if (Configs.molotovResidueTickrate.Value > 0)
            {
                molotovDotZone.GetComponent<ProjectileDotZone>().fireFrequency = Configs.molotovResidueTickrate.Value;
                tickRate = Configs.molotovResidueTickrate.Value;
            }
            else if (Configs.molotovResidueTickrate.Value == 0)
            {
                molotovDotZone.GetComponent<ProjectileDotZone>().enabled = false;
                tickRate = 0;
            }

            // Adjust explosion damage
            if (Configs.molotovExplosionDamage.Value >= 0)
            {
                molotovClusterProjectile.GetComponent<ProjectileImpactExplosion>().blastDamageCoefficient = Configs.molotovExplosionDamage.Value;
                molotovSingleProjectile.GetComponent<ProjectileImpactExplosion>().blastDamageCoefficient = Configs.molotovExplosionDamage.Value;
                explosionDamage = Configs.molotovExplosionDamage.Value;
            }

            if (tickRate != 2f || explosionDamage != 5f)
            {
                UpdateDescription(tickRate*100f, explosionDamage*100f);
            }
        }

        private void UpdateDescription(float tickRate, float explosionDamage)
        {
            string tickString = tickRate.ToString().Replace(",", ".");
            string dmgString = explosionDamage.ToString().Replace(",", ".");

            LanguageAPI.Add("EQUIPMENT_MOLOTOV_DESC",
                $"Throw <style=cIsDamage>6</style> molotov cocktails that <style=cIsDamage>ignites</style> enemies for <style=cIsDamage>{dmgString}% base damage</style>. " +
                $"Each molotov leaves a burning area for <style=cIsDamage>{tickString}% damage per second</style>.",
                "en"
            );

            LanguageAPI.Add("EQUIPMENT_MOLOTOV_DESC",
                $"投掷<style=cIsDamage>6</style>枚燃烧瓶，对敌人造成<style=cIsDamage>{dmgString}%</style>基础伤害并使其<style=cIsDamage>点燃</style>。" +
                $"每枚燃烧瓶会留下一片燃烧区域，每秒造成<style=cIsDamage>{tickString}%</style>伤害。",
                "zh-CN"
            );
        }
    }
}
