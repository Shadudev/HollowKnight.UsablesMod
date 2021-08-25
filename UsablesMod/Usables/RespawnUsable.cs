using System;

namespace UsablesMod.Usables
{
    class RespawnUsable : IUsable
    {
        public void Run()
        {
            GameManager.instance.HazardRespawn();
        }

        public bool IsRevertable()
        {
            return false;
        }

        public float GetDuration()
        {
            return -1;
        }

        public void Revert()
        {
        }

        public string GetName()
        {
            return "RespawnUsable";
        }
    }
}