using RoR2;
using RoR2.CharacterAI;
using R2API;
using UnityEngine;
using UnityEngine.AddressableAssets;
using MonoMod.Cil;
using Mono.Cecil.Cil;

namespace SuperCyanTweaks
{
    public class ClayApothecary
    {
        public static CharacterSpawnCard cscClayGrenadier = Addressables.LoadAssetAsync<CharacterSpawnCard>("RoR2/DLC1/ClayGrenadier/cscClayGrenadier.asset").WaitForCompletion();
        public static GameObject clayGrenadierMasterPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/ClayGrenadier/ClayGrenadierMaster.prefab").WaitForCompletion();
        public static EntityStateConfiguration clayGrenadierFaceSlam = Addressables.LoadAssetAsync<EntityStateConfiguration>("RoR2/DLC1/ClayGrenadier/EntityStates.ClayGrenadier.FaceSlam.asset").WaitForCompletion();

        public ClayApothecary()
        {
            // Adjust director credit cost
            if (Configs.clayApothecaryCost.Value >= 0)
            {
                cscClayGrenadier.directorCreditCost = Configs.clayApothecaryCost.Value;
            }

            // Adjustments on tar ball & ranged mortar attacks
            if (Configs.clayApothecaryTarBallRange.Value >= 0 || Configs.clayApothecaryMortarHealthThreshold.Value >= 0)
            {
                AISkillDriver[] skillDrivers = clayGrenadierMasterPrefab.GetComponents<AISkillDriver>();
                foreach (AISkillDriver skillDriver in skillDrivers)
                {
                    if (skillDriver.customName == "ThrowBarrel" && Configs.clayApothecaryTarBallRange.Value >= 0)
                    {
                        skillDriver.maxDistance = Configs.clayApothecaryTarBallRange.Value;
                    }
                    else if (skillDriver.customName == "LongDistanceMortar" && Configs.clayApothecaryMortarHealthThreshold.Value >= 0)
                    {
                        if (Configs.clayApothecaryMortarHealthThreshold.Value >= 1)
                        {
                            skillDriver.maxUserHealthFraction = float.PositiveInfinity;
                        }
                        else
                        {
                            skillDriver.maxUserHealthFraction = Configs.clayApothecaryMortarHealthThreshold.Value;
                        }
                    }
                }
            }

            // Slam/Mortar self-damage
            if (Configs.clayApothecarySlamSelfDmg.Value >= 0)
            {                
                clayGrenadierFaceSlam.TryModifyFieldValue("healthCostFraction", Configs.clayApothecarySlamSelfDmg.Value / 100f);
            }

            // Slam/Mortar targeting
            if (Configs.clayApothecaryMortarTargeting.Value == true)
            {
                bool hookFailed = true;
                IL.EntityStates.ClayGrenadier.FaceSlam.FixedUpdate += (il) =>
                {
                    ILCursor c = new(il);

                    if (
                        c.TryGotoNext(MoveType.Before,
                        x => x.MatchStfld<BullseyeSearch>("sortMode"))
                    )
                    {
                        c.Index--;
                        c.Next.OpCode = OpCodes.Ldc_I4_2;

                        hookFailed = false;
                    }

                    if (hookFailed == true)
                    {
                        Log.Error("Clay Apothecary mortar targeting hook failed!");
                    }
                };
            }
        }
    }
}
