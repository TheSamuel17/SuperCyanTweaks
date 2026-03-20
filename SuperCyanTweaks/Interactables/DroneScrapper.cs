using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SuperCyanTweaks
{
    public class DroneScrapper
    {
        public static InteractableSpawnCard iscDroneScrapper = Addressables.LoadAssetAsync<InteractableSpawnCard>("RoR2/DLC3/DroneScrapper/iscDroneScrapper.asset").WaitForCompletion();
        public static EntityStateConfiguration waitToBeginScrapping = Addressables.LoadAssetAsync<EntityStateConfiguration>("RoR2/DLC3/DroneScrapper/EntityStates.Scrapper.WaitToBeginScrappingDrone.asset").WaitForCompletion();
        public static EntityStateConfiguration scrappingDrone = Addressables.LoadAssetAsync<EntityStateConfiguration>("RoR2/DLC3/DroneScrapper/EntityStates.Scrapper.ScrappingDrone.asset").WaitForCompletion();
        public static EntityStateConfiguration scrappingDroneToIdle = Addressables.LoadAssetAsync<EntityStateConfiguration>("RoR2/DLC3/DroneScrapper/EntityStates.Scrapper.ScrappingDroneToIdle.asset").WaitForCompletion();
        public static GameObject scrappingVFX = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC3/DroneScrapper/DroneScrapperScrappingVFX.prefab").WaitForCompletion();
        
        public DroneScrapper()
        {
            // Set max count per stage
            iscDroneScrapper.maxSpawnsPerStage = Configs.droneScrapperMaxCount.Value;

            // Make them spawn on Treeborn Colony & Golden Dieback
            if (Configs.droneScrapperOnHabitat.Value == true)
            {
                DirectorAPI.DirectorCardHolder droneScrapperSpawnCard = new()
                {
                    Card = new()
                    {
                        minimumStageCompletions = 0,
                        preventOverhead = false,
                        selectionWeight = 6,
                        spawnCard = iscDroneScrapper,
                    },
                    InteractableCategory = DirectorAPI.InteractableCategory.Duplicator,
                };

                DirectorAPI.Helpers.AddNewInteractableToStage(droneScrapperSpawnCard, DirectorAPI.Stage.TreebornColony);
                DirectorAPI.Helpers.AddNewInteractableToStage(droneScrapperSpawnCard, DirectorAPI.Stage.GoldenDieback);
            }

            // Faster animation
            if (Configs.droneScrapperFaster.Value == true)
            {
                waitToBeginScrapping.TryModifyFieldValue("duration", 1.33f); // 1.5
                scrappingDrone.TryModifyFieldValue("duration", 1.67f); // 3
                scrappingDroneToIdle.TryModifyFieldValue("duration", .4f); // .5

                scrappingVFX.GetComponent<DestroyOnTimer>().duration = 2.17f;
            }
        }
    }
}
