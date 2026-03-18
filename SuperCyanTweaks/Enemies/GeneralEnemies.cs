using EntityStates;
using EntityStates.Gup;
using EntityStates.Vermin.Weapon;
using EntityStates.Halcyonite;
using RoR2;
using RoR2.Skills;
using UnityEngine.AddressableAssets;
using R2API;
using UnityEngine;
using HG;
using MonoMod.Cil;
using Mono.Cecil.Cil;
using System;

namespace SuperCyanTweaks
{
    public class GeneralEnemies : MonoBehaviour
    {
        public static SkillDef gupSpikesSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/DLC1/Gup/GupSpikes.asset").WaitForCompletion();
        public static SteppedSkillDef verminLashSkill = Addressables.LoadAssetAsync<SteppedSkillDef>("RoR2/DLC1/Vermin/TongueLash.asset").WaitForCompletion();
        public static SkillDef halcyoniteSlashSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/DLC2/Halcyonite/HalcyoniteMonsterGoldenSlash.asset").WaitForCompletion();
        public static SkillDef halcyoniteSwipeSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/DLC2/Halcyonite/HalcyoniteMonsterGoldenSwipe.asset").WaitForCompletion();

        public static EntityStateConfiguration gupSpikesEsc = Addressables.LoadAssetAsync<EntityStateConfiguration>("RoR2/DLC1/Gup/EntityStates.Gup.GupSpikesState.asset").WaitForCompletion();
        public static EntityStateConfiguration verminLashEsc = Addressables.LoadAssetAsync<EntityStateConfiguration>("RoR2/DLC1/Vermin/EntityStates.Vermin.Weapon.TongueLash.asset").WaitForCompletion();
        public static EntityStateConfiguration halcyoniteSlashEsc = Addressables.LoadAssetAsync<EntityStateConfiguration>("RoR2/DLC2/Halcyonite/EntityStates.HalcyoniteMonster.GoldenSlash.asset").WaitForCompletion();
        public static EntityStateConfiguration halcyoniteSwipeEsc = Addressables.LoadAssetAsync<EntityStateConfiguration>("RoR2/DLC2/Halcyonite/EntityStates.HalcyoniteMonster.GoldenSwipe.asset").WaitForCompletion();

        public GeneralEnemies()
        {
            // Enemy melee tweak
            if (Configs.enemyMeleeTweak.Value == true)
            {
                AdjustAttack(gupSpikesEsc, gupSpikesSkill, typeof(GupSpikesStateModified));
                AdjustAttack(verminLashEsc, verminLashSkill, typeof(TongueLashModified));
                AdjustAttack(halcyoniteSlashEsc, halcyoniteSlashSkill, typeof(GoldenSlashModified));
                AdjustAttack(halcyoniteSwipeEsc, halcyoniteSwipeSkill, typeof(GoldenSwipeModified));
            }
        }

        private void AdjustAttack(EntityStateConfiguration esc, SkillDef skillDef, System.Type type)
        {
            EntityStateConfiguration newEsc = Instantiate(esc);
            newEsc.targetType = (SerializableSystemType)type;
            newEsc.SetNameFromTargetType();
            ContentAddition.AddEntityStateConfiguration(newEsc);

            SerializableEntityStateType newState = ContentAddition.AddEntityState(type, out bool added);
            if (added) skillDef.activationState = newState;
        }
    }
    public class GupSpikesStateModified : GupSpikesState
    {
        public override bool allowExitFire => false;
    }

    public class TongueLashModified : TongueLash
    {
        public override bool allowExitFire => false;
    }

    public class GoldenSlashModified : GoldenSlash
    {
        public override bool allowExitFire => false;
    }

    public class GoldenSwipeModified : GoldenSwipe
    {
        public override bool allowExitFire => false;
    }
}