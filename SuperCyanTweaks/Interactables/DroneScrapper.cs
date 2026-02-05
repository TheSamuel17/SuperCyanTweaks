using R2API;
using RoR2;
using UnityEngine.AddressableAssets;

namespace SuperCyanTweaks
{
    public class DroneScrapper
    {
        public static InteractableSpawnCard iscDroneScrapper = Addressables.LoadAssetAsync<InteractableSpawnCard>("RoR2/DLC3/DroneScrapper/iscDroneScrapper.asset").WaitForCompletion();

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
        }
    }
}
