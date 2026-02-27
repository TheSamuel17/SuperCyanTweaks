using RoR2;
using UnityEngine.AddressableAssets;

namespace SuperCyanTweaks
{
    public class PretendersPrecipice
    {
        public static InteractableSpawnCard iscShrineChance = Addressables.LoadAssetAsync<InteractableSpawnCard>("RoR2/Base/ShrineChance/iscShrineChance.asset").WaitForCompletion();
        public static InteractableSpawnCard iscShrineBlood = Addressables.LoadAssetAsync<InteractableSpawnCard>("RoR2/Base/ShrineBlood/iscShrineBlood.asset").WaitForCompletion();
        public static InteractableSpawnCard iscShrineBoss = Addressables.LoadAssetAsync<InteractableSpawnCard>("RoR2/Base/ShrineBoss/iscShrineBoss.asset").WaitForCompletion();

        public static InteractableSpawnCard iscShrineChanceSnowy = Addressables.LoadAssetAsync<InteractableSpawnCard>("RoR2/Base/ShrineChance/iscShrineChanceSnowy.asset").WaitForCompletion();
        public static InteractableSpawnCard iscShrineBloodSnowy = Addressables.LoadAssetAsync<InteractableSpawnCard>("RoR2/Base/ShrineBlood/iscShrineBloodSnowy.asset").WaitForCompletion();
        public static InteractableSpawnCard iscShrineBossSnowy = Addressables.LoadAssetAsync<InteractableSpawnCard>("RoR2/Base/ShrineBoss/iscShrineBossSnowy.asset").WaitForCompletion();

        public static DirectorCardCategorySelection dccsNestInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>("RoR2/DLC3/nest/dccsNestInteractables.asset").WaitForCompletion();

        public PretendersPrecipice()
        {
            // Snowy shrines on Pretender's Precipice
            if (Configs.nestSnowyShrines.Value == true)
            {
                foreach (DirectorCardCategorySelection.Category category in dccsNestInteractables.categories)
                {
                    foreach (DirectorCard card in category.cards)
                    {
                        SpawnCard spawnCard = card.GetSpawnCard();
                        if (spawnCard == iscShrineChance)
                        {
                            card.spawnCard = iscShrineChanceSnowy;
                        }
                        else if (spawnCard == iscShrineBlood)
                        {
                            card.spawnCard = iscShrineBloodSnowy;
                        }
                        else if (spawnCard == iscShrineBoss)
                        {
                            card.spawnCard = iscShrineBossSnowy;
                        }
                    }
                }
            }
        }
    }
}
