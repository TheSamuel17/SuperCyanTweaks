using MonoMod.Cil;
using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SuperCyanTweaks
{
    public class EccentricVase
    {
        public static BuffDef cloakBuff = Addressables.LoadAssetAsync<BuffDef>("RoR2/Base/Common/bdCloak.asset").WaitForCompletion();
        public static BuffDef intangibleBuff = Addressables.LoadAssetAsync<BuffDef>("RoR2/Base/Common/bdIntangible.asset").WaitForCompletion();
        public static BuffDef fallImmuneBuff = Addressables.LoadAssetAsync<BuffDef>("RoR2/Junk/Common/bdIgnoreFallDamage.asset").WaitForCompletion();
        public static GameObject ziplineVehicle = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Gateway/ZiplineVehicle.prefab").WaitForCompletion();
        public static EquipmentDef gateway = Addressables.LoadAssetAsync<EquipmentDef>("RoR2/Base/Gateway/Gateway.asset").WaitForCompletion();

        public EccentricVase()
        {
            // Adjust cooldown
            if (Configs.eccentricVaseCooldown.Value >= 0)
            {
                gateway.cooldown = Configs.eccentricVaseCooldown.Value;
            }

            // Adjust zipline length
            if (Configs.eccentricVaseMaxDist.Value >= 0)
            {
                bool hookFailed = true;
                IL.RoR2.EquipmentSlot.FireGateway += (il) =>
                {
                    ILCursor c = new(il);

                    if (
                        c.TryGotoNext(MoveType.Before,
                        x => x.MatchLdcR4(1000f)
                    ))
                    {
                        c.Next.Operand = Configs.eccentricVaseMaxDist.Value;
                        hookFailed = false;
                    }

                    if (hookFailed == true)
                    {
                        Log.Error("Eccentric Vase length hook failed!");
                    }
                };

                if (hookFailed == false)
                {
                    UpdateDescription(Configs.eccentricVaseMaxDist.Value);
                }
            }

            // Adjust acceleration
            if (Configs.eccentricVaseAcceleration.Value >= 0)
            {
                ziplineVehicle.GetComponent<ZiplineVehicle>().acceleration = Configs.eccentricVaseAcceleration.Value;
            }

            // Handle buffs while traveling
            if (Configs.eccentricVaseCloak.Value == true || Configs.eccentricVaseIntangible.Value || Configs.eccentricVaseNoCrater.Value)
            {
                On.RoR2.ZiplineVehicle.OnPassengerEnter += OnPassengerEnter;
                On.RoR2.ZiplineVehicle.OnPassengerExit += OnPassengerExit;
            }
        }

        private void OnPassengerEnter(On.RoR2.ZiplineVehicle.orig_OnPassengerEnter orig, ZiplineVehicle self, GameObject passenger)
        {
            CharacterBody body = passenger.GetComponent<CharacterBody>();
            if (body)
            {
                if (Configs.eccentricVaseCloak.Value == true) body.AddBuff(cloakBuff);
                if (Configs.eccentricVaseIntangible.Value == true) body.AddBuff(intangibleBuff);
            }

            orig(self, passenger);
        }

        private void OnPassengerExit(On.RoR2.ZiplineVehicle.orig_OnPassengerExit orig, ZiplineVehicle self, GameObject passenger)
        {
            orig(self, passenger);

            CharacterBody body = passenger.GetComponent<CharacterBody>();
            if (body)
            {
                if (Configs.eccentricVaseCloak.Value == true) body.RemoveBuff(cloakBuff);
                if (Configs.eccentricVaseIntangible.Value == true) body.RemoveBuff(intangibleBuff);
                if (Configs.eccentricVaseNoCrater.Value == true) body.AddTimedBuff(fallImmuneBuff, .5f);
            }
        }

        private void UpdateDescription(float distance)
        {
            string distanceString = distance.ToString().Replace(",", ".");

            LanguageAPI.Add("EQUIPMENT_GATEWAY_DESC",
                $"Create a <style=cIsUtility>quantum tunnel</style> of up to <style=cIsUtility>{distanceString}m</style> in length. " +
                $"Lasts 30 seconds.",
                "en"
            );

            LanguageAPI.Add("EQUIPMENT_GATEWAY_DESC",
                $"生成一条最长为<style=cIsUtility>{distanceString}米</style>的<style=cIsUtility>量子隧道</style>。" +
                $"持续30秒。",
                "zh-CN"
            );
        }
    }
}
