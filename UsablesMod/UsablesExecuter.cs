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
                float duration = revertable.GetDuration();
                LogHelper.Log($"Reverting {usable.GetName()} in {duration}");
                yield return new WaitForSeconds(duration);
                revertable.Revert();
            }
        }
    }
}
