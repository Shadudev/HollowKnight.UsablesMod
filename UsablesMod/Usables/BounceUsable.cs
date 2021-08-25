using static UsablesMod.LogHelper;
using UnityEngine;
using System.Collections;
using System;

namespace UsablesMod.Usables
{
    class BounceUsable : IUsable
    {
        System.Random rnd = new System.Random();
        private bool bouncing = false;
        
        public void Run()
        {
            bouncing = true;
            GameManager.instance.StartCoroutine(Bouncing());
        }

        public bool IsRevertable()
        {
            return true;
        }

        public float GetDuration()
        {
            return 30f;
        }

        public void Revert()
        {
            bouncing = false;

        }

        public string GetName()
        {
            return "BounceUsable";
        }

        private IEnumerator Bouncing()
        {
            while (bouncing)
            {
                if (HeroController.instance.CheckTouchingGround())
                {
                    HeroController.instance.ShroomBounce();
                }
                yield return new WaitForSeconds(rnd.Next(5));
            }
            yield return null;
        }
    }
}

