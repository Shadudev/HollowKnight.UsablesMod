using System;

namespace UsablesMod.Usables
{
    class MPCostUsable : IUsable
    {
        public void Run()
        {
            PlayMakerFSM SpellControl = HeroController.instance.gameObject.LocateMyFSM("Spell Control");

            int multiplier = new Random(RandomizerMod.RandomizerMod.Instance.Settings.Seed).Next(0, 2) * 2;
            SpellControl.FsmVariables.FindFsmInt("MP Cost").Value *= multiplier;
        }

        public float GetDuration()
        {
            return 30;
        }

        public void Revert()
        {
            PlayMakerFSM SpellControl = HeroController.instance.gameObject.LocateMyFSM("Spell Control");
            SpellControl.FsmVariables.FindFsmInt("MP Cost").Value = PlayerData.instance.GetBool("equippedCharm_33") ? 24 : 33;
        }

        public string GetName()
        {
            return "MPCost";
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