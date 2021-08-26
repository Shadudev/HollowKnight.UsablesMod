using System;

namespace UsablesMod.Usables
{
    class ZoomUsable : IUsable, IRevertable
    {
        private float multiplier;

        public ZoomUsable(int randomSeed) 
        {
            multiplier = new Random(randomSeed).Next(7, 20) / 10f;
        }

        public void Run()
        {
            GameCameras.instance.tk2dCam.ZoomFactor *= multiplier;
        }

        public float GetDuration()
        {
            return 30;
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
            return "Zoomed In Camera";
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