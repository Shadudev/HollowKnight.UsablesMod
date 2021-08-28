using System;

namespace UsablesMod.Usables
{
    class ZoomUsable : IUsable, IRevertable
    {
        private readonly Random random;
        private readonly float multiplier;
        private readonly float duration;

        public ZoomUsable(int randomSeed) 
        {
            random = new Random(randomSeed);
            if (random.Next(2) == 1)
                multiplier = random.Next(6, 8) / 10f;
            else
                multiplier = random.Next(13, 20) / 10f;
            duration = random.Next(120, 240);
        }

        public void Run()
        {
            GameCameras.instance.tk2dCam.ZoomFactor *= multiplier;
        }

        public float GetDuration()
        {
            return duration;
        }

        public void Revert()
        {
            GameCameras.instance.tk2dCam.ZoomFactor /= multiplier;
        }

        public string GetName()
        {
            return "Zoom_Usable";
        }
        public string GetDisplayName()
        {
            return "Zoomed Camera";
        }
        public string GetDescription()
        {
            return "Enhance. Enhance. Still can't see anything, ENHANCE!";
        }
        public string GetItemSpriteKey()
        {
            return "ShopIcons.Focus";
        }
    }
}