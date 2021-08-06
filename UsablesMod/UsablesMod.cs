using Modding;
using System;
using UsablesMod.Usables;

namespace UsablesMod
{
    public class UsablesMod : Mod, ITogglableMod
    {
        public UsablesMod Instance { get; private set; }

		public override void Initialize()
		{
			if (Instance != null)
			{
				LogWarn("Initialized twice... Stop that.");
				return;
			}

			Instance = this;

			RandomizerMod.GiveItemActions.ExternItemHandlers.Add(TriggerUsable);
			RandomizerMod.Randomization.PostRandomizer.PostRandomizationActions += ReplaceKPGeoRock;
		}

        private static bool TriggerUsable(RandomizerMod.GiveItemActions.GiveAction action,
			string item, string location, int geo)
        {
			SampleUsable.Run();

			return false;
        }

        private void ReplaceKPGeoRock()
        {
            string kingsPassLeftGeoRockLocation = "Geo_Rock-King's_Pass_Left";
            RandomizerMod.RandomizerMod.Instance.Settings.AddItemPlacement("Rancid_Egg-Weaver's_Den", kingsPassLeftGeoRockLocation);
        }

        public override string GetVersion()
        {
            string ver = "0.0.1";
            return ver;
        }

        public void Unload()
        {
			Instance = null;
			RandomizerMod.GiveItemActions.ExternItemHandlers.Remove(TriggerUsable);
		}
    }
}
