using RoR2;
using UnityEngine;

namespace SuperCyanTweaks
{
    public class FalseSon
    {
        public FalseSon()
        {
            // Phase 2 Golem Count
            if (Configs.falseSonGolemCountTweak.Value == true)
            {
                On.RoR2.MeridianEventTriggerInteraction.Start += MeridianEventTriggerInteraction_Start;
            }
        }

        private void MeridianEventTriggerInteraction_Start(On.RoR2.MeridianEventTriggerInteraction.orig_Start orig, MeridianEventTriggerInteraction self) // Brought to you by FalseSonBossTweaks
        {
            orig(self);

            if (self.phase2CombatDirector)
            {
                CombatDirector director = self.phase2CombatDirector.GetComponent<CombatDirector>();
                if (director)
                {
                    director.maxSquadCount = (uint)Mathf.Min(Run.instance.stageClearCount + 1, 40);
                    Log.Message(director.maxSquadCount);
                }
            }
        }
    }
}
