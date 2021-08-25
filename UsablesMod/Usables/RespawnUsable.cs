using System;

namespace UsablesMod.Usables
{
    class RespawnUsable : IUsable
    {
        public void Run()
        {
            GameManager.instance.HazardRespawn();
        }

        public string GetName()
        {
            return "RespawnUsable";
        }
        public string GetDisplayName()
        {
            return "Hazard Respawn";
        }
        public string GetDescription()
        {
            return "How did I get here?";
        }
        public string GetItemSpriteKey()
        {
            return "ShopIcons.CityKey";
        }
    }
}