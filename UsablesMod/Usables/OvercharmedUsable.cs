namespace UsablesMod.Usables
{
    class OvercharmedUsable : IUsable, IRevertable
    {
        public void Run()
        {
            PlayerData.instance.overcharmed = true;
        }

        public float GetDuration()
        {
            return 10f;
        }

        public void Revert()
        {
            GameManager.instance.RefreshOvercharm();
        }

        public string GetName()
        {
            return "OvercharmedUsable";
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
