namespace UsablesMod.Usables
{
    class OvercharmedUsable : IUsable
    {
        public void Run()
        {
            PlayerData.instance.overcharmed = true;
        }

        public bool IsRevertable()
        {
            return true;
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
    }
}
