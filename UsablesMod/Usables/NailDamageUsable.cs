using System;

namespace UsablesMod.Usables
{
    class NailDamageUsable : IUsable
    {
        public void Run()
        {
            PlayerData.instance.nailDamage += 4;
            PlayMakerFSM.BroadcastEvent("UPDATE NAIL DAMAGE");
            
        }

        public bool IsRevertable()
        {
            return true;
        }

        public float GetDuration()
        {
            return 20;
        }

        public void Revert()
        {
            PlayerData.instance.nailDamage -= 4;
            PlayMakerFSM.BroadcastEvent("UPDATE NAIL DAMAGE");
        }

        public string GetName()
        {
            return "NailDamageUsable";
        }
    }
}
