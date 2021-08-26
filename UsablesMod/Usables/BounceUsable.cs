using UnityEngine;
using System.Collections;

namespace UsablesMod.Usables
{
    class BounceUsable : IUsable, IRevertable
    {
        private readonly System.Random random;
        private bool bouncing = false;

        public BounceUsable(int randomSeed)
        {
            random = new System.Random(randomSeed);
        }

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
            while (bouncing)
            {
                if (HeroController.instance.CheckTouchingGround())
                {
                    HeroController.instance.ShroomBounce();
                }
                yield return new WaitForSeconds(random.Next(5));
            }
            yield return null;
        }

        public string GetName()
        {
            return "Bounce_Usable";
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

