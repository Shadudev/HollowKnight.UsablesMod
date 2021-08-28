using UnityEngine;
using System.Collections;

namespace UsablesMod.Usables
{
    class BounceUsable : IUsable, IRevertable
    {
        private static float unifiedDuration = 0;

        private readonly System.Random random;
        private bool bouncing = false;

        public BounceUsable(int randomSeed)
        {
            random = new System.Random(randomSeed);
        }

        public void Run()
        {
            unifiedDuration += 45f;
            bouncing = true;
            GameManager.instance.StartCoroutine(Bouncing());
        }

        public bool IsRevertable()
        {
            return true;
        }

        public float GetDuration()
        {
            // Case for 2+ instances of usable running before 1st was reverted
            if (unifiedDuration != 45f) return 0;

            return unifiedDuration;
        }

        public void Revert()
        {
            bouncing = false;
            unifiedDuration = 0;
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

