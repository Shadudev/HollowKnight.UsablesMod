using System;

namespace UsablesMod.Usables
{
    class LifebloodUsable : IUsable
    {
        public void Run()
        {
            int amount = new Random(RandomizerMod.RandomizerMod.Instance.Settings.Seed).Next(3, 7);
            for (int i = 0; i < amount; i++)
            {
                EventRegister.SendEvent("ADD BLUE HEALTH");
            }
        }

        public string GetName()
        {
            return "LifebloodUsable";
        }
        public string GetDisplayName()
        {
            return "Some Lifeblood Masks";
        }
        public string GetDescription()
        {
            return "Limited edition Lifeblood masks - get them while they last!";
        }
        public string GetItemSpriteKey()
        {
            return "ShopIcons.Lifeblood";
        }
    }
}
