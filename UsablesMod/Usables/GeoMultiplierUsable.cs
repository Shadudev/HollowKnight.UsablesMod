using System;

namespace UsablesMod.Usables
{
    class GeoMultiplierUsable : IUsable
    {
        private readonly bool isGoodMultiplier;

        Random random;

        public GeoMultiplierUsable(int randomSeed)
        {
            random = new Random(randomSeed);
            isGoodMultiplier = random.Next(2) == 0;
        }

        public void Run()
        {
            if (isGoodMultiplier)
            {
                HeroController.instance.TakeGeo((int) (PlayerData.instance.geo * random.Next(1, 5) / 10));
            } 
            else
            {
                HeroController.instance.AddGeo((int) (PlayerData.instance.geo * random.Next(6, 10) / 10)); 
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
