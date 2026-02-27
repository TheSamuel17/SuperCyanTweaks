using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;

namespace SuperCyanTweaks
{
    public class ShrineOfOrder : MonoBehaviour
    {
        public static GameObject shrineRestackPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/ShrineRestack/ShrineRestack.prefab").WaitForCompletion();
        public static GameObject shrineRestackSandyPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/ShrineRestack/ShrineRestackSandy Variant.prefab").WaitForCompletion();

        public ShrineOfOrder()
        {
            // Guaranteed spawn on Stage 4
            if (Configs.shrineOfOrderOnStage4.Value == true)
            {
                Stage.onStageStartGlobal += OnStageStartGlobal;
            }
        }

        private void OnStageStartGlobal(Stage stage)
        {
            GameObject prefab = null;
            Vector3 position = Vector3.zero;
            Vector3 rotation = Vector3.zero;

            switch (stage.sceneDef.cachedName)
            {
                case "dampcavesimple":
                    prefab = shrineRestackPrefab;
                    position = new Vector3(79f, -87.5f, -184.2f);
                    rotation = new Vector3(5f, 275f, 0f);
                    break;

                case "shipgraveyard":
                    prefab = shrineRestackPrefab;
                    position = new Vector3(-92f, 11.81f, -29f);
                    rotation = new Vector3(355f, 38f, 0f);
                    break;

                case "rootjungle":
                    prefab = shrineRestackPrefab;
                    position = new Vector3(-110f, 77f, -275f);
                    rotation = new Vector3(10f, 355f, 0f);
                    break;

                case "repurposedcrater":
                    prefab = shrineRestackSandyPrefab;
                    position = new Vector3(-104.5f, -1.8f, -171f);
                    rotation = new Vector3(5f, 210f, 0f);
                    break;

                case "snowtime_sandtrap":
                    prefab = shrineRestackSandyPrefab;
                    position = new Vector3(-22f, -73f, 177f);
                    rotation = new Vector3(0f, 90f, 0f);
                    break;

                case "snowtime_gmbigcity":
                    prefab = shrineRestackPrefab;
                    position = new Vector3(21.2f, -7.15f, 447.6f);
                    rotation = new Vector3(0f, 45f, 0f);
                    break;

                default:
                    break;
            }

            if (prefab && position != Vector3.zero && rotation != Vector3.zero)
            {
                GameObject shrine = Instantiate(prefab, position, Quaternion.Euler(rotation));
                shrine.transform.eulerAngles = rotation;
                NetworkServer.Spawn(shrine);
            }
        }
    }
}
