using System;

namespace UsablesMod.Usables
{
    class MPCostUsable : IUsable
    {
        public void Run()
        {
            PlayMakerFSM SpellControl = HeroController.instance.gameObject.LocateMyFSM("Spell Control");
            SpellControl.FsmVariables.FindFsmInt("MP Cost").Value = 66;
        }

        public bool IsRevertable()
        {
            return true;
        }

        public float GetDuration()
        {
            return 30;
        }

        public void Revert()
        {
            PlayMakerFSM SpellControl = HeroController.instance.gameObject.LocateMyFSM("Spell Control");
            SpellControl.FsmVariables.FindFsmInt("MP Cost").Value = 33;
        }

        public string GetName()
        {
            return "MPCost";
        }
    }
}