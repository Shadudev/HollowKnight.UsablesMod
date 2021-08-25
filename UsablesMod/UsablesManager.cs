using System;
using System.Collections.Generic;
using UsablesMod.Usables;

namespace UsablesMod
{
    internal class UsablesManager
    {
        private static readonly int MIN_USABLES = 7, MAX_USABLES = 16;
        private readonly UsablesExecuter usablesExecuter;

        internal Dictionary<string, IUsable> usables;

        public UsablesManager() 
        {
            usablesExecuter = new UsablesExecuter();
            CreateUsablesReqDefs();
        }

        internal void AddUsableItemsToSet(HashSet<string> items)
        {
            usables = new Dictionary<string, IUsable>();

            int amount = new Random(RandomizerMod.RandomizerMod.Instance.Settings.Seed).Next(MIN_USABLES, MAX_USABLES + 1);
            UsablesFactory factory = new UsablesFactory();

            for (int i = 0; i < amount; i++)
            {
                IUsable usable = factory.GetRandomUsable();
                string usableItemName = $"{usable.GetName()}_({i})";

                SetUsableItemData(usableItemName, usable);

                usables.Add(usableItemName, usable);
                items.Add(usableItemName);
            }
        }

        internal void LoadMissingItems(RandomizerMod.SaveSettings settings)
        {
            usables = new Dictionary<string, IUsable>();
            foreach ((string item, string _) in settings.ItemPlacements)
            {
                if (UsablesFactory.TryCreateUsable(item, out IUsable usable))
                {
                    RegisterUsable(item, usable);
                }
            }
        }
        internal void RegisterUsable(string usableItemName, IUsable usable)
        {
            SetUsableItemData(usableItemName, usable);
            usables.Add(usableItemName, usable);
        }

        internal static void SetUsableItemData(string usableItemName, IUsable usable)
        {
            RandomizerMod.LanguageStringManager.SetString("UI", usableItemName, usable.GetDisplayName());

            RandomizerMod.Randomization.LogicManager.EditItemDef(usableItemName,
                RandomizerMod.Randomization.LogicManager.GetItemDef(
                    usableItemName.Substring(0, usableItemName.LastIndexOf("_"))));
        }

        private static void CreateUsablesReqDefs()
        {
            foreach (string usableName in UsablesFactory.USABLE_NAMES)
            {
                UsablesFactory.TryCreateUsable(usableName, out IUsable usable);
                RandomizerMod.LanguageStringManager.SetString("UI", usableName, usable.GetDisplayName());

                string shopDescKey = usableName + "_SHOP_DESC";
                RandomizerMod.LanguageStringManager.SetString("UI", shopDescKey, usable.GetDescription());

                RandomizerMod.Randomization.LogicManager.EditItemDef(usableName,
                    new RandomizerMod.Randomization.ReqDef()
                    {
                        action = RandomizerMod.GiveItemActions.GiveAction.None,
                        pool = "Usables",
                        type = RandomizerMod.Randomization.ItemType.Trinket,
                        nameKey = usableName,
                        shopDescKey = shopDescKey,
                        shopSpriteKey = usable.GetItemSpriteKey()
                    });
            }
        }

        internal void Run(string itemName)
        {
            IUsable usable = usables[itemName];
            usablesExecuter.RunUsable(usable);
        }

        internal bool IsAUsable(string item)
        {
            foreach (string usableName in UsablesFactory.USABLE_NAMES)
                if (item.StartsWith(usableName))
                    return true;

            return false;
        }
    }
}
