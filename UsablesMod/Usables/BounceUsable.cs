using UnityEngine;
using System.Collections;
using System;

namespace UsablesMod.Usables
{
    class BounceUsable : IUsable, IRevertable
    {
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

        private IEnumerator Bouncing()
        {
            System.Random rnd = new System.Random(DateTime.Now.Ticks.GetHashCode());
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

        public string GetName()
        {
            return "BounceUsable";
        }
        public string GetDisplayName()
        {
            return "Spring Shoes";
        }
        public string GetDescription()
        {
            return "I swear, your jump key is broken.";
        }
        public string GetItemSpriteKey()
        {
            return "ShopIcons.Wings";
        }
    }
}

