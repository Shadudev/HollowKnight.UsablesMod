using System;

namespace UsablesMod.Usables
{
    class LifebloodUsable : IUsable
    {
        public void Run()
        {
            int amount = new Random(DateTime.Now.Ticks.GetHashCode()).Next(3, 7);
            for (int i = 0; i < amount; i++)
            {
                EventRegister.SendEvent("ADD BLUE HEALTH");
            }
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
            return "LifebloodUsable";
        }
    }
}
