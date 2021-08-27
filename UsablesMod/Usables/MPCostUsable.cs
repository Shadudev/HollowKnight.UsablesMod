using HutongGames.PlayMaker;
using System;

namespace UsablesMod.Usables
{
    class MPCostUsable : IUsable, IRevertable
    {
        private readonly int multiplier;
        private FsmInt mpCost;

        public MPCostUsable(int randomSeed)
        {
            multiplier = new Random(randomSeed).Next(0, 2) * 2;
        }

        public void Run()
        {
            mpCost = HeroController.instance.gameObject.LocateMyFSM("Spell Control").FsmVariables.FindFsmInt("MP Cost");
            mpCost.Value *= multiplier;
        }

        public float GetDuration()
        {
            return 120;
        }

        public void Revert()
        {
            mpCost.Value = PlayerData.instance.GetBool("equippedCharm_33") ? 24 : 33;
        }

        public string GetName()
        {
            return "MP_Cost_Usable";
        }
        public string GetDisplayName()
        {
            return "Stell Twisper";
        }
        public string GetDescription()
        {
            return "Comes with a cup, lid, and sippy straw. No additional refills.";
        }
        public string GetItemSpriteKey()
        {
            return "Charms.33";
        }
    }
}