using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SuperCyanTweaks
{
    public class RallypointDelta
    {
        public static Sprite fanSpeedBuffSprite = Addressables.LoadAssetAsync<Sprite>("RoR2/Base/Common/texMovespeedBuffIcon.tif").WaitForCompletion();
        public static BuffDef fanSpeedBuffDef;

        public RallypointDelta()
        {
            // Speed boost
            if (Configs.fanSpeedBoost.Value == true)
            {
                // Create BuffDef
                fanSpeedBuffDef = ScriptableObject.CreateInstance<BuffDef>();
                fanSpeedBuffDef.buffColor = new Color(32f/255f, 128f/255f, 223f/255f, 1f);
                fanSpeedBuffDef.canStack = false;
                fanSpeedBuffDef.eliteDef = null;
                fanSpeedBuffDef.iconSprite = fanSpeedBuffSprite;
                fanSpeedBuffDef.ignoreGrowthNectar = false;
                fanSpeedBuffDef.isCooldown = false;
                fanSpeedBuffDef.isDebuff = false;
                fanSpeedBuffDef.isDOT = false;
                fanSpeedBuffDef.isHidden = false;
                fanSpeedBuffDef.stackingDisplayMethod = BuffDef.StackingDisplayMethod.Default;
                fanSpeedBuffDef.startSfx = null;
                fanSpeedBuffDef.name = "SuperCyanTweaks_FanSpeedBuff";

                // Register buff
                ContentAddition.AddBuffDef(fanSpeedBuffDef);

                // Buff logic
                RecalculateStatsAPI.GetStatCoefficients += GetStatCoefficients;

                // Set up fan behavior
                Stage.onStageStartGlobal += OnStageStartGlobal;
            }
        }

        private void GetStatCoefficients(CharacterBody body, RecalculateStatsAPI.StatHookEventArgs args)
        {
            if (body && body.HasBuff(fanSpeedBuffDef))
            {
                args.moveSpeedMultAdd += Configs.fanBuffStrength.Value / 100f;
            }
        }

        private void OnStageStartGlobal(Stage stage)
        {
            if (SceneInfo.instance.sceneDef != SceneCatalog.FindSceneDef("frozenwall"))
            {
                return;
            }

            GameObject fanHolder = GameObject.Find("/PERMUTATION: Human Fan/");
            if (fanHolder)
            {
                foreach (Transform child in fanHolder.transform)
                {
                    AdjustFan(child.gameObject);
                }
            }
        }

        private void AdjustFan(GameObject fan)
        {
            Transform model = fan.transform.Find("mdlHumanFan");
            if (model is not null)
            {
                Transform jumpVolume = model.transform.Find("JumpVolume");
                if (jumpVolume is not null)
                {
                    jumpVolume.gameObject.AddComponent<OnUseFan>();
                }
            }
        }
    }

    public class OnUseFan : MonoBehaviour
    {
        public JumpVolume jumpVolume;
        
        public void Awake()
        {
            jumpVolume = gameObject.GetComponent<JumpVolume>();
            if (jumpVolume)
            {
                jumpVolume.onJump.AddListener(OnJump);
            }
        }
        
        public void OnJump(CharacterBody body)
        {
            if (body && body.teamComponent && body.teamComponent.teamIndex == TeamIndex.Player)
            {
                body.AddTimedBuff(RallypointDelta.fanSpeedBuffDef, Configs.fanBuffDuration.Value);
            }
        }
    }
}
