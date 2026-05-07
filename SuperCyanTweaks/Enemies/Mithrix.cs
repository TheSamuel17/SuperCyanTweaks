using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SuperCyanTweaks
{
    public class Mithrix
    {
        public static EntityStateConfiguration brotherSpellChannel = Addressables.LoadAssetAsync<EntityStateConfiguration>("RoR2/Base/Brother/EntityStates.BrotherMonster.SpellChannelState.asset").WaitForCompletion();

        public Mithrix()
        {
            // Faster item steal
            if (Configs.mithrixStealInterval.Value >= 0)
            {
                brotherSpellChannel.TryModifyFieldValue("stealInterval", Configs.mithrixStealInterval.Value);
            }
        }
    }
}
