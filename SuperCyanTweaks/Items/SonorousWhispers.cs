using RoR2;
using MonoMod.Cil;
using Mono.Cecil.Cil;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using R2API;

namespace SuperCyanTweaks
{
    public class SonorousWhispers
    {
        public static ItemDef whispers = Addressables.LoadAssetAsync<ItemDef>("RoR2/DLC2/Items/ItemDropChanceOnKill/ItemDropChanceOnKill.asset").WaitForCompletion();

        public static GameObject solusWingBodyPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC3/SolusWing/SolusWingBody.prefab").WaitForCompletion();
        public static GameObject solusWingWeakPointBodyPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC3/SolusWing/ExhaustPortWeakpointBody.prefab").WaitForCompletion();
        public static GameObject solusHeartDownedBodyPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC3/SolusHeart/SolusHeartBody_Offering.prefab").WaitForCompletion();

        public static WeightedSelection<UniquePickup> selector = new WeightedSelection<UniquePickup>();
        public static float tier1Weight = 0.6f;
        public static float tier2Weight = 0.38f;
        public static float tier3Weight = 0.02f;

        public SonorousWhispers()
        {
            // Enable the rework
            if (Configs.sonorousWhispersRework.Value == true)
            {
                // Disable the vanilla effect
                bool hookFailed = true;
                IL.RoR2.GlobalEventManager.OnCharacterDeath += (il) =>
                {
                    ILCursor c = new(il);

                    if (
                        c.TryGotoNext(MoveType.Before,
                        x => x.MatchLdsfld(typeof(DLC2Content.Items), nameof(DLC2Content.Items.ItemDropChanceOnKill))
                    ))
                    {
                        c.Remove();
                        c.Emit<Main>(OpCodes.Ldsfld, nameof(Main.emptyItemDef));
                        hookFailed = false;
                    }

                    if (hookFailed == true)
                    {
                        Log.Error("Sonorous Whispers Rework hook failed!");
                    }
                };

                // New stuff
                if (hookFailed == false)
                {
                    whispers.tags = whispers.tags.AddToArray(ItemTag.CannotCopy); // No longer inheritable
                    GlobalEventManager.onCharacterDeathGlobal += GlobalEventManager_onCharacterDeathGlobal;
                    
                    UpdateDescription();
                }
            }
        }

        // Handle the new effect
        private void GlobalEventManager_onCharacterDeathGlobal(DamageReport damageReport)
        {
            if (!NetworkServer.active || damageReport == null)
            {
                return;
            }

            CharacterBody victimBody = damageReport.victimBody;
            CharacterMaster attackerMaster = damageReport.attackerMaster;
            TeamIndex attackerTeamIndex = damageReport.attackerTeamIndex;

            if (victimBody && attackerMaster)
            {
                // Positional info, making sure to match 1:1 with the original effect
                GameObject gameObject = null;
                if ((bool)damageReport.victim)
                {
                    gameObject = damageReport.victim.gameObject;
                }

                Vector3 vector = Vector3.zero;
                Quaternion quaternion = Quaternion.identity;
                Transform transform = gameObject.transform;
                if ((bool)transform)
                {
                    vector = transform.position;
                    quaternion = transform.rotation;
                }

                InputBankTest inputBankTest = null;
                if ((bool)victimBody)
                {
                    inputBankTest = victimBody.inputBank;
                }
                Ray ray = (inputBankTest ? inputBankTest.GetAimRay() : new Ray(vector, quaternion * Vector3.forward));

                // Obtain the team-wide number of stacks
                int num = 0;
                ReadOnlyCollection<CharacterMaster> readOnlyInstancesList = CharacterMaster.readOnlyInstancesList;
                for (int i = 0; i < readOnlyInstancesList.Count; i++)
                {
                    CharacterMaster characterMaster = readOnlyInstancesList[i];
                    if (characterMaster.teamIndex == attackerTeamIndex)
                    {
                        num += characterMaster.inventory.GetItemCountEffective(DLC2Content.Items.ItemDropChanceOnKill);
                    }
                }

                // Main logic
                if (num > 0 && (victimBody.isBoss || victimBody.bodyIndex == BodyCatalog.FindBodyIndex(solusHeartDownedBodyPrefab)))
                {
                    Vector3 finalVelocity;

                    if (victimBody.bodyIndex == BodyCatalog.FindBodyIndex(solusWingBodyPrefab))
                    {
                        finalVelocity = Vector3.up * 40f + ray.direction * 20f; // Launch the item extra far so it doesn't get buried under Solus Wing's corpse.
                    }
                    else if (victimBody.bodyIndex == BodyCatalog.FindBodyIndex(solusWingWeakPointBodyPrefab))
                    {
                        finalVelocity = Vector3.up * 30f + ray.direction * 15f;
                    }
                    else
                    {
                        finalVelocity = Vector3.up * 20f + ray.direction * 2f;
                    }

                    PickupDropletController.CreatePickupDroplet(GenerateDrop(num), vector + Vector3.up * 1.5f, finalVelocity, false, false);
                }
            }

            
        }

        private UniquePickup GenerateDrop(int num)
        {
            selector.Clear();
            Add(Run.instance.availableTier1DropList, tier1Weight);
            Add(Run.instance.availableTier2DropList, tier2Weight * num);
            Add(Run.instance.availableTier3DropList, tier3Weight * Mathf.Pow(num, 2f));

            return PickupDropTable.GeneratePickupFromWeightedSelection(Run.instance.runRNG, selector);
        }

        private void Add(List<PickupIndex> sourceDropList, float listWeight)
        {
            if (listWeight <= 0f || sourceDropList.Count == 0)
            {
                return;
            }
            float weight = listWeight / sourceDropList.Count;
            foreach (PickupIndex sourceDrop in sourceDropList)
            {
                selector.AddChoice(new UniquePickup(sourceDrop), weight);
            }
        }

        private void UpdateDescription()
        {
            string tier1String = (tier1Weight * 100).ToString().Replace(",", ".");
            string tier2String = (tier2Weight * 100).ToString().Replace(",", ".");
            string tier3String = (tier3Weight * 100).ToString().Replace(",", ".");

            // ========== ENGLISH ========== //

            LanguageAPI.Add("ITEM_ITEMDROPCHANCEONKILL_PICKUP",
                $"Bosses drop an item on kill.",
                "en"
            );
            
            LanguageAPI.Add("ITEM_ITEMDROPCHANCEONKILL_DESC",
                $"Bosses drop an item ({tier1String}%/<style=cIsHealing>{tier2String}%</style>/<style=cIsHealth>{tier3String}%</style>) on kill. " +
                $"<style=cStack>(Increases rarity chances of the items per stack).</style>",
                "en"
            );

            // ========== FRENCH ========== //

            LanguageAPI.Add("ITEM_ITEMDROPCHANCEONKILL_PICKUP",
                $"Obtenez un objet lorsqu'un boss est éliminé.",
                "fr"
            );

            LanguageAPI.Add("ITEM_ITEMDROPCHANCEONKILL_DESC",
                $"Les boss vaincus font apparaître un objet ({tier1Weight * 100} %/<style=cIsHealing>{tier2Weight * 100} %</style>/<style=cIsHealth>{tier3Weight * 100} %</style>). " +
                $"<style=cStack>(La rareté de l'objet augmente à chaque cumul).</style>",
                "fr"
            );
        }
    }
}
