namespace UsablesMod.Usables
{
    class OvercharmedUsable : IUsable, IRevertable
    {
        public OvercharmedUsable() {}

        public void Run()
        {
            PlayerData.instance.overcharmed = true;
        }

        public float GetDuration()
        {
            return 90f;
        }

        public void Revert()
        {
            GameManager.instance.RefreshOvercharm();
        }

        public string GetName()
        {
            return "Overcharmed_Usable";
        }
        public string GetDisplayName()
        {
            return "Overcharmed Time";
        }
        public string GetDescription()
        {
            return "Cursing your notches has proven to improve Flukemarm RNG.";
        }
        public string GetItemSpriteKey()
        {
            return "ShopIcons.CharmNotch";
        }
    }
}
