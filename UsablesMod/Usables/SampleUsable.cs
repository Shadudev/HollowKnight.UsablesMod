﻿using static UsablesMod.LogHelper;
using UnityEngine;
using System.Collections;

namespace UsablesMod.Usables
{
    class SampleUsable : IUsable
    {
        private bool bouncing = false;
        public void Run()
        {
            bouncing = true;
            HeroController.instance.DEFAULT_GRAVITY = 20f;
            GameManager.instance.StartCoroutine(Bouncing());
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
            bouncing = false;
        }

        public string GetName()
        {
            return "SampleUsable";
        }

        private IEnumerator Bouncing()
        {
            while(bouncing)
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
