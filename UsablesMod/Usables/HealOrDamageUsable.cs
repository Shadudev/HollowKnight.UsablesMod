using static UsablesMod.LogHelper;
using UnityEngine;
using System.Collections;
//using ModCommon;
using Vasi;
namespace UsablesMod.Usables
{
    class HealOrDamageUsable : MonoBehaviour, IUsable
    {
        bool healing;
        public void Run()
        {
            healing = true;
            HeroController.instance.TakeDamage(HeroController.instance.gameObject, GlobalEnums.CollisionSide.top, 1, 0);
            GameManager.instance.StartCoroutine(regeneration());
            PlayMakerFSM[] fsmList = UnityEngine.Object.FindObjectsOfType<PlayMakerFSM>();
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
            healing = false;
        }

        public string GetName()
        {
            return "HealUsable";
        }

        private IEnumerator regeneration()
        {
            while (healing)
            {
                HeroController.instance.AddHealth(1);
                yield return new WaitForSeconds(2f);
            }
        }
    }
}

