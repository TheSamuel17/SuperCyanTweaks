using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SuperCyanTweaks
{
    public class AlloyWorshipUnit
    {
        public static EntityStateConfiguration superDelayKnockup = Addressables.LoadAssetAsync<EntityStateConfiguration>("RoR2/Base/RoboBallBoss/EntityStates.RoboBallBoss.Weapon.FireSuperDelayKnockup.asset").WaitForCompletion();
        public static RoR2.Skills.SkillDef superDelayKnockupSkill = Addressables.LoadAssetAsync<RoR2.Skills.SkillDef>("RoR2/Base/RoboBallBoss/SuperFireDelayKnockup.asset").WaitForCompletion();
        public static GameObject awuBodyPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/RoboBallBoss/SuperRoboBallBossBody.prefab").WaitForCompletion();

        public AlloyWorshipUnit()
        {
            // Configurable shield duration
            if (Configs.awuShieldDuration.Value >= 0)
            {
                superDelayKnockup.TryModifyFieldValue("shieldDuration", Configs.awuShieldDuration.Value);
            }

            // Configurable cooldown delay
            if (Configs.awuDelaySuperAttackCooldown.Value > 0)
            {
                superDelayKnockupSkill.isCooldownBlockedUntilManuallyReset = true;
                awuBodyPrefab.AddComponent<SuperDelayKnockupCooldownController>();
            }
        }
    }

    public class SuperDelayKnockupCooldownController : MonoBehaviour
    {
        public float age = 0f;
        public float timer;
        public bool blockCooldown = false;

        CharacterBody characterBody;
        SkillLocator skillLocator;

        private void Awake()
        {
            timer = Configs.awuDelaySuperAttackCooldown.Value;
            characterBody = GetComponent<CharacterBody>();
            if (characterBody && characterBody.skillLocator)
            {
                skillLocator = characterBody.skillLocator;
            }
        }

        private void OnEnable()
        {
            if (characterBody) characterBody.onSkillActivatedServer += OnSkillActivatedServer;
        }

        private void FixedUpdate()
        {
            if (blockCooldown == true)
            {
                age += Time.fixedDeltaTime;
                if (age >= timer)
                {
                    age -= timer;

                    if (skillLocator && skillLocator.special && skillLocator.special.skillDef == AlloyWorshipUnit.superDelayKnockupSkill)
                    {
                        blockCooldown = false;
                        skillLocator.special.SetBlockedCooldownSkillState(false);
                    }
                }
            }
        }

        private void OnDisable()
        {
            if (characterBody) characterBody.onSkillActivatedServer -= OnSkillActivatedServer;
        }

        private void OnSkillActivatedServer(GenericSkill genericSkill)
        {
            if (genericSkill.skillDef == AlloyWorshipUnit.superDelayKnockupSkill)
            {
                blockCooldown = true;
                age = 0f;
            }
        }
    }
}
