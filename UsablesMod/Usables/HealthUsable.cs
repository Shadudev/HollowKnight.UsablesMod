using UnityEngine;
using System.Collections;
using System;

namespace UsablesMod.Usables
{
    class HealthUsable : MonoBehaviour, IUsable, IRevertable
    {
        private System.Random random;
        private bool running;

        public void Run()
        {
            random = new System.Random(DateTime.Now.Ticks.GetHashCode());
            running = true;

            int amount = random.Next(1, 3);
            if (amount == 1)
            {
                GameManager.instance.StartCoroutine(Regeneration());
            }
            else
            {
                GameManager.instance.StartCoroutine(Poison());
            }
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
            running = false;
        }
        
        private IEnumerator Regeneration()
        {
            while (running)
            {
                HeroController.instance.AddHealth(1);
                yield return new WaitForSeconds(random.Next(2, 12));
            }
        }

        private IEnumerator Poison()
        {
            while (running)
            {
                HeroController.instance.TakeDamage(HeroController.instance.gameObject, GlobalEnums.CollisionSide.top, 1, 0);
                yield return new WaitForSeconds(random.Next(3, 12));
            }
        }

        public string GetName()
        {
            return "HealUsable";
        }

        public string GetDisplayName()
        {
            return "Health Supply";
        }

        public string GetDescription()
        {
            return "Health supply has arrived! Wait, are you the supplier?";
        }

        public string GetItemSpriteKey()
        {
            return "ShopIcons.Focus";
        }
    }
}

