using System;

namespace UsablesMod.Usables
{
    class ZoomUsable : IUsable, IRevertable
    {
        public void Run()
        {
            GameCameras.instance.tk2dCam.ZoomFactor = GameCameras.instance.tk2dCam.ZoomFactor * 1.4f;
        }

        public float GetDuration()
        {
            return 30;
        }

        public void Revert()
        {
            GameCameras.instance.tk2dCam.ZoomFactor = GameCameras.instance.tk2dCam.ZoomFactor * 5f / 7f;
        }

        public string GetName()
        {
            return "ZoomUsable";
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