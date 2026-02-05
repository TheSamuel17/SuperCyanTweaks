using R2API;
using RoR2;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SuperCyanTweaks
{
    public class Halcyonite
    {
        public static GameObject halcyoniteBodyPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC2/Halcyonite/HalcyoniteBody.prefab").WaitForCompletion();
        public static CharacterSpawnCard cscHalcyonite = Addressables.LoadAssetAsync<CharacterSpawnCard>("RoR2/DLC2/Halcyonite/cscHalcyonite.asset").WaitForCompletion();
        public static DirectorCardCategorySelection dccsFalseSonPhase2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>("RoR2/DLC2/meridian/dccsFalseSonBossPhase2.asset").WaitForCompletion();

        public static DirectorCardCategorySelection[] dccsWithHalcyoniteChampion =
        {
            Addressables.LoadAssetAsync<DirectorCardCategorySelection>("RoR2/DLC2/dccsBlackBeachMonstersDLC2.asset").WaitForCompletion(),
            Addressables.LoadAssetAsync<DirectorCardCategorySelection>("RoR2/DLC2/dccsGolemplainsMonstersDLC2Only.asset").WaitForCompletion(),
            Addressables.LoadAssetAsync<DirectorCardCategorySelection>("RoR2/DLC2/dccsSnowyForestMonstersDLC2.asset").WaitForCompletion(),
            Addressables.LoadAssetAsync<DirectorCardCategorySelection>("RoR2/DLC2/lakes/dccsLakesMonstersDLC2.asset").WaitForCompletion(),
            Addressables.LoadAssetAsync<DirectorCardCategorySelection>("RoR2/DLC2/lakesnight/dccsLakesnightMonsters.asset").WaitForCompletion(),
            Addressables.LoadAssetAsync<DirectorCardCategorySelection>("RoR2/DLC2/dccsGooLakeMonstersDLC2Only.asset").WaitForCompletion(),
            Addressables.LoadAssetAsync<DirectorCardCategorySelection>("RoR2/DLC2/dccsFoggySwampMonstersDLC2Only.asset").WaitForCompletion(),
            Addressables.LoadAssetAsync<DirectorCardCategorySelection>("RoR2/DLC2/lemuriantemple/dccsLemurianTempleMonsters.asset").WaitForCompletion(),
            Addressables.LoadAssetAsync<DirectorCardCategorySelection>("RoR2/DLC2/habitat/dccsHabitatMonsters.asset").WaitForCompletion(),
            Addressables.LoadAssetAsync<DirectorCardCategorySelection>("RoR2/DLC2/habitatfall/dccsHabitatfallMonsters.asset").WaitForCompletion(),
            Addressables.LoadAssetAsync<DirectorCardCategorySelection>("RoR2/DLC2/dccsDampCaveMonstersDLC2Only.asset").WaitForCompletion(),
            Addressables.LoadAssetAsync<DirectorCardCategorySelection>("RoR2/DLC2/dccsRootJungleMonstersDLC2.asset").WaitForCompletion(),
        };

        public static DirectorAPI.Stage[] stagesWithHalcyoniteChampion =
        {
            DirectorAPI.Stage.DistantRoost,
            DirectorAPI.Stage.TitanicPlains,
            DirectorAPI.Stage.SiphonedForest,
            DirectorAPI.Stage.VerdantFalls,
            DirectorAPI.Stage.ViscousFalls,
            DirectorAPI.Stage.AbandonedAqueduct,
            DirectorAPI.Stage.WetlandAspect,
            DirectorAPI.Stage.ReformedAltar,
            DirectorAPI.Stage.TreebornColony,
            DirectorAPI.Stage.GoldenDieback,
            DirectorAPI.Stage.AbyssalDepths,
            DirectorAPI.Stage.SunderedGrove,
        };

        public Halcyonite()
        {
            // Spawn on Prime Meridian pre-loop
            if (Configs.halcyoniteOnMeridianPreLoop.Value == true)
            {
                DirectorAPI.DirectorCardHolder halcyoniteSpawnCard = new()
                {
                    Card = new()
                    {
                        minimumStageCompletions = 0,
                        preventOverhead = false,
                        selectionWeight = 1,
                        spawnDistance = DirectorCore.MonsterSpawnDistance.Standard,
                        spawnCard = cscHalcyonite,
                    },
                    MonsterCategory = DirectorAPI.MonsterCategory.Minibosses,
                };

                DirectorAPI.Helpers.RemoveExistingMonsterFromStage("Halcyonite", DirectorAPI.Stage.PrimeMeridian);
                DirectorAPI.Helpers.AddNewMonsterToStage(halcyoniteSpawnCard, false, DirectorAPI.Stage.PrimeMeridian);
            }

            // Spawn during False Son P2 on loop
            if (Configs.halcyoniteOnFalseSonLoop.Value == true)
            {
                DirectorAPI.DirectorCardHolder halcyoniteSpawnCard = new()
                {
                    Card = new()
                    {
                        minimumStageCompletions = 5,
                        preventOverhead = false,
                        selectionWeight = 25, // lol
                        spawnDistance = DirectorCore.MonsterSpawnDistance.Standard,
                        spawnCard = cscHalcyonite,
                    },
                    MonsterCategory = DirectorAPI.MonsterCategory.Minibosses, // It's gonna be a different category no matter what
                };

                dccsFalseSonPhase2.AddCard(halcyoniteSpawnCard);
            }

            // Champion -> Miniboss
            if (Configs.halcyoniteProperCategory.Value == true)
            {
                for (int i = 0; i < dccsWithHalcyoniteChampion.Length; i++)
                {
                    DirectorCardCategorySelection dccs = dccsWithHalcyoniteChampion[i];

                    if (CheckForHalcyonite(dccs))
                    {
                        DirectorAPI.Stage stage = stagesWithHalcyoniteChampion[i];
                        int minCompletions = 4;
                        int weight = 1;

                        if (stage == DirectorAPI.Stage.ViscousFalls || stage == DirectorAPI.Stage.ReformedAltar || stage == DirectorAPI.Stage.TreebornColony || stage == DirectorAPI.Stage.GoldenDieback)
                        {
                            minCompletions = 0;
                        }

                        if (stage == DirectorAPI.Stage.ReformedAltar)
                        {
                            weight = 2;
                        }

                        DirectorAPI.DirectorCardHolder halcyoniteSpawnCard = new()
                        {
                            Card = new()
                            {
                                minimumStageCompletions = minCompletions,
                                preventOverhead = false,
                                selectionWeight = weight,
                                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard,
                                spawnCard = cscHalcyonite,
                            },
                            MonsterCategory = DirectorAPI.MonsterCategory.Minibosses,
                        };

                        DirectorAPI.Helpers.RemoveExistingMonsterFromStage("Halcyonite", stage);
                        DirectorAPI.Helpers.AddNewMonsterToStage(halcyoniteSpawnCard, false, stage);
                    }
                }
            }
        }

        private bool CheckForHalcyonite(DirectorCardCategorySelection dccs)
        {
            bool hasHalcyonite = false;

            foreach (DirectorCardCategorySelection.Category category in dccs.categories)
            {
                foreach (DirectorCard card in category.cards)
                {
                    if (card.GetSpawnCard() == cscHalcyonite)
                    {
                        hasHalcyonite = true;
                        break;
                    }
                }
            }

            return hasHalcyonite;
        }
    }
}
