using System;

namespace UsablesMod.Usables
{
    class ZoomUsable : IUsable
    {
        public void Run()
        {
            GameCameras.instance.tk2dCam.ZoomFactor = GameCameras.instance.tk2dCam.ZoomFactor * 1.4f;
        }

        public bool IsRevertable()
        {
            return true;
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
    }
}