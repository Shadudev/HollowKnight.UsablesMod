using static UsablesMod.LogHelper;
using UnityEngine;
using System;

namespace UsablesMod.Usables
{
    class HealUsable : IUsable
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
    }
}
