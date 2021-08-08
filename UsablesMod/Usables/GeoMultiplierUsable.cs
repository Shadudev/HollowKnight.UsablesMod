using System;

namespace UsablesMod.Usables
{
    class GeoMultiplierUsable : IUsable
    {
        public void Run()
        {
            int amount = new Random(DateTime.Now.Ticks.GetHashCode()).Next(1, 3);
            if (amount == 1) 
            {
                HeroController.instance.TakeGeo((int) (PlayerData.instance.geo * 0.6f));
            } 
            else
            {
                HeroController.instance.AddGeo((int) (PlayerData.instance.geo * 0.6f)); ;
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
            return "GeoMultiplierUsable";
        }
    }
}
