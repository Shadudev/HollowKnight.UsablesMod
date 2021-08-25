using static UsablesMod.LogHelper;
using UnityEngine;
using System.Collections;

namespace UsablesMod.Usables
{
    class BounceUsable : IUsable
    {
        private bool bouncing = false;

        public void Run()
        {
            bouncing = true;
            GameManager.instance.StartCoroutine(Bouncing());
            On.HeroController.Awake += mymethod;

        }

        private void mymethod(On.HeroController.orig_Awake orig, HeroController self)
        {
            self.DEFAULT_GRAVITY = 30f;
            orig(self);
        }

        public bool IsRevertable()
        {
            return true;
        }

        public float GetDuration()
        {
            return 12f;
        }

        public void Revert()
        {
            //GameManager.instance.LoadMrMushromScene();
            bouncing = false;
        }

        public string GetName()
        {
            return "SampleUsable";
        }

        private IEnumerator Bouncing()
        {
            while (bouncing)
            {
                if (HeroController.instance.CheckTouchingGround())
                {
                    HeroController.instance.ShroomBounce();
                }
                yield return new WaitForSeconds(0.05f);
            }
            yield return null;
        }
    }
}

