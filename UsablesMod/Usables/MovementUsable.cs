namespace UsablesMod.Usables
{
    class MovementUsable : IUsable, IRevertable
    {
        private readonly System.Random random;

        public MovementUsable(int randomSeed)
        {
            random = new System.Random(randomSeed);
        }

        public void Run()
        {
            HeroController.instance.DASH_COOLDOWN = HeroController.instance.SHADOW_DASH_TIME = 0.01f;
            HeroController.instance.JUMP_SPEED = HeroController.instance.SWIM_MAX_SPEED = HeroController.instance.RUN_SPEED = 15f;
            HeroController.instance.TIME_TO_ENTER_SCENE_HOR = HeroController.instance.SPEED_TO_ENTER_SCENE_HOR = 0.1f;
            HeroController.instance.DOUBLE_JUMP_STEPS = 50;
            HeroController.instance.WJ_KICKOFF_SPEED = 10f;
            HeroController.instance.WJLOCK_STEPS_LONG = 15;
        }

        public float GetDuration()
        {
            return 30f;
        }

        public void Revert() 
        { 

        }

        public string GetName()
        {
            return "Movement_Usable";
        }
        public string GetDisplayName()
        {
            return "Movement Usable";
        }
        public string GetDescription()
        {
            return "You ever want to go fast? It's time to go weeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee";
        }
        public string GetItemSpriteKey()
        {
            return "UI.Shop.Shitpost";
        }
    }
}

