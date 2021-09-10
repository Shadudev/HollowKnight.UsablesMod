using Modding;

namespace UsablesMod
{
    public class UsablesMod : Mod, ITogglableMod
    {
        public static UsablesMod Instance { get; private set; }

        internal UsablesManager UsablesManager;

        public SaveSettings Settings { get; set; } = new SaveSettings();
        public override ModSettings SaveSettings
        {
            get => Settings = Settings ?? new SaveSettings();
            set => Settings = value is SaveSettings saveSettings ? saveSettings : Settings;
        }

        public override void Initialize()
        {
            if (Instance != null)
            {
                LogWarn("Initialized twice... Stop that.");
                return;
            }

            Instance = this;
            UsablesManager = new UsablesManager();

            RandomizerMod.Randomization.ItemManager.AddItemsToRandomizedItemsSet += UsablesManager.AddUsableItemsToSet;
            RandomizerMod.GiveItemActions.ExternItemHandlers.Add(TriggerUsable);
            RandomizerMod.SaveSettings.PreAfterDeserialize += UsablesManager.LoadMissingItems;
        }

        private bool TriggerUsable(RandomizerMod.GiveItemActions.GiveAction action,
			string item, string location, int geo)
        {
            if (UsablesManager.IsAUsable(item))
                UsablesManager.AddToSlots(item);

            return false;
        }

        public override string GetVersion()
        {
            string ver = "0.0.3";
            return ver;
        }

        public void Unload()
        {
			Instance = null;
            RandomizerMod.Randomization.ItemManager.AddItemsToRandomizedItemsSet -= UsablesManager.AddUsableItemsToSet;
            RandomizerMod.GiveItemActions.ExternItemHandlers.Remove(TriggerUsable);
            RandomizerMod.SaveSettings.PreAfterDeserialize -= UsablesManager.LoadMissingItems;
            UsablesManager = null;
        }
    }
}
