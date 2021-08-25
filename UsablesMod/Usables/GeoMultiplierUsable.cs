using System;

namespace UsablesMod.Usables
{
    class GeoMultiplierUsable : IUsable
    {
        public void Run()
        {
            int amount = new Random(RandomizerMod.RandomizerMod.Instance.Settings.Seed).Next(1, 3);
            if (amount == 1) 
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
            return "GeoMultiplierUsable";
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
