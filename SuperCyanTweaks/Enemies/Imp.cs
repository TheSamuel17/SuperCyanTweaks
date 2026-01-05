using RoR2;
using R2API;
using UnityEngine.AddressableAssets;

namespace SuperCyanTweaks
{
    public class Imp
    {
        public static CharacterSpawnCard cscImp = Addressables.LoadAssetAsync<CharacterSpawnCard>("RoR2/Base/Imp/cscImp.asset").WaitForCompletion();

        public Imp()
        {
            // Make them spawn in Helminth Hatchery
            if (Configs.impOnHelminthRoost.Value == true)
            {
                DirectorAPI.DirectorCardHolder impSpawnCard = new()
                {
                    Card = new()
                    {
                        minimumStageCompletions = 0,
                        preventOverhead = false,
                        selectionWeight = 2,
                        spawnDistance = DirectorCore.MonsterSpawnDistance.Standard,
                        spawnCard = cscImp,
                    },
                    MonsterCategory = DirectorAPI.MonsterCategory.BasicMonsters,
                };

                DirectorAPI.Helpers.AddNewMonsterToStage(impSpawnCard, false, DirectorAPI.Stage.HelminthHatchery);
            }
        }
    }
}
