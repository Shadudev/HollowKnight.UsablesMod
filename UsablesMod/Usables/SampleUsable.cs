using static UsablesMod.LogHelper;

namespace UsablesMod.Usables
{
    class SampleUsable : IUsable, IRevertable
    {
        public void Run()
        {
            Log("Running SampleUsable");
        }

        public float GetDuration()
        {
            return 12f;
        }

        public void Revert()
        {
            Log("Reverting SampleUsable");
        }

        public string GetName()
        {
            return "SampleUsable";
        }
        public string GetDisplayName()
        {
            return "Sample Usable";
        }
        public string GetDescription()
        {
            return "Shop description here.";
        }
        public string GetItemSpriteKey()
        {
            return "UI.Shop.Shitpost";
        }
    }
}
