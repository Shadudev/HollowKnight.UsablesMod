namespace UsablesMod.Usables
{
    class SampleUsable : IUsable, IRevertable
    {
        public void Run()
        {
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
