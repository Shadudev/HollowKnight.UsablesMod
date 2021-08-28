namespace UsablesMod.Usables
{
    class OvercharmedUsable : IUsable, IRevertable
    {
        private static float unifiedDuration = 0;

        public OvercharmedUsable() {}

        public void Run()
        {
            unifiedDuration += 90f;
            SetOvercharm();
        }

        private static void SetOvercharm()
        {
            PlayerData.instance.overcharmed = true;
        }

        public float GetDuration()
        {
            // Case for 2+ instances of usable running before 1st was reverted
            if (unifiedDuration != 90) return 0;

            return unifiedDuration;
        }

        public void Revert()
        {
            GameManager.instance.RefreshOvercharm();
            unifiedDuration = 0;
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
