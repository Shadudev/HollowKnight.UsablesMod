using System.Collections;
using UnityEngine;
using UsablesMod.Usables;

namespace UsablesMod
{
    class UsablesExecuter : MonoBehaviour
    {
        public void RunUsable(IUsable usable)
        {
            GameManager.instance.StartCoroutine(RunUsableRoutine(usable));
        }

        private IEnumerator RunUsableRoutine(IUsable usable)
        {
            LogHelper.Log($"Running {usable.GetName()} routine");
            usable.Run();
            if (usable is IRevertable)
            {
                IRevertable revertable = usable as IRevertable;
                yield return Revert(revertable, usable.GetName());
            }
        }

        private IEnumerator Revert(IRevertable revertable, string name)
        {
            float duration = revertable.GetDuration();
            LogHelper.Log($"Reverting {name}");
            yield return new WaitForSeconds(duration);

            float newDuration = revertable.GetDuration();
            float newDurationDiff = newDuration - duration;
            while (newDurationDiff > 0)
            {
                LogHelper.Log($"Adding {newDurationDiff} before reverting {name}");
                duration = newDuration;

                yield return new WaitForSeconds(newDurationDiff);

                newDuration = revertable.GetDuration();
                newDurationDiff = newDuration - duration;
            }

            LogHelper.Log($"Reverting {name}");
            revertable.Revert();
        }
    }
}
