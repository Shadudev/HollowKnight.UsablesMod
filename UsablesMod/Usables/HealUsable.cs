using static UsablesMod.LogHelper;
using UnityEngine;
using System;

namespace UsablesMod.Usables
{
    class HealUsable : IUsable, IRevertable
    {
        public void Run()
        {
            
        }

        public bool IsRevertable()
        {
            return true;
        }

        public float GetDuration()
        {
            return 12f;
        }

        public void Revert()
        {
            
        }

        public string GetName()
        {
            return "HealUsable";
        }
        public string GetDisplayName()
        {
            return "Health Supply";
        }
        public string GetDescription()
        {
            return "Supply has arrived! Wait, are you the supplier?";
        }
        public string GetItemSpriteKey()
        {
            return "ShopIcons.Focus";
        }
    }
}
