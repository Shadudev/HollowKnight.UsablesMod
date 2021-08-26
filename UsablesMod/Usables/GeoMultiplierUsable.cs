using System;

namespace UsablesMod.Usables
{
    class GeoMultiplierUsable : IUsable
    {
        private readonly bool isGoodMultiplier;

        public GeoMultiplierUsable(int randomSeed)
        {
            isGoodMultiplier = new Random(randomSeed).Next(2) == 0;
        }

        public void Run()
        {
            if (isGoodMultiplier)
            {
                HeroController.instance.TakeGeo((int) (PlayerData.instance.geo * 0.6f));
            } 
            else
            {
                HeroController.instance.AddGeo((int) (PlayerData.instance.geo * 0.6f)); ;
            }
        }

        public string GetName()
        {
            return "Geo_Multiplier_Usable";
        }
        public string GetDisplayName()
        {
            return "Geo Multiplier";
        }
        public string GetDescription()
        {
            return "It may not be safe, but it could be worth it...";
        }
        public string GetItemSpriteKey()
        {
            return "ShopIcons.Geo";
        }

    }
}
