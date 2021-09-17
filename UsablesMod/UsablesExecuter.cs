using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UsablesMod.Usables;

namespace UsablesMod
{
    class UsablesExecuter : MonoBehaviour
    {
        private readonly ActiveUsablesBar usablesBar;
        private readonly Dictionary<IRevertable, Coroutine> activeRevertables;

        internal UsablesExecuter()
        {
            usablesBar = new ActiveUsablesBar();
            activeRevertables = new Dictionary<IRevertable, Coroutine>();
        }

        public void RunUsable((IUsable Usable, GameObject icon) pair)
        {
            lock (activeRevertables)
            {
                Coroutine coroutine = GameManager.instance.StartCoroutine(RunUsableRoutine(pair));
                if (pair.Usable is IRevertable)
                    activeRevertables.Add(pair.Usable as IRevertable, coroutine);
            }

            ShowUsablePopup(pair.Usable);
        }

        private void ShowUsablePopup(IUsable usable)
        {
            string itemDefKey = "usablePopUp";
            string displayName = "Used " + usable.GetDisplayName();
            string spriteKey = usable.GetItemSpriteKey();

            RandomizerMod.Randomization.LogicManager.EditItemDef(itemDefKey, 
                new RandomizerMod.Randomization.ReqDef() { nameKey = itemDefKey, shopSpriteKey = spriteKey });

            RandomizerMod.LanguageStringManager.SetString("UI", itemDefKey, displayName);

            RandomizerMod.GiveItemActions.ShowEffectiveItemPopup(itemDefKey);
        }

        private IEnumerator RunUsableRoutine((IUsable Usable, GameObject icon) pair)
        {
            LogHelper.Log($"Running {pair.Usable.GetName()} routine");
            pair.Usable.Run();
            if (pair.Usable is IRevertable)
            {
                IRevertable revertable = pair.Usable as IRevertable;
                yield return Revert(revertable, pair.Usable.GetName(), pair.icon);
            }
            else
            {
                Destroy(pair.icon);
            }
        }

        private IEnumerator Revert(IRevertable revertable, string name, GameObject icon)
        {
            float duration = revertable.GetDuration();
            if (duration > 0)
            {
                usablesBar.Add(icon, revertable.GetDuration);

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

                usablesBar.Remove(icon);
            }

            lock (activeRevertables)
            {
                activeRevertables.Remove(revertable);
                LogHelper.Log($"Reverting {name}");
                revertable.Revert();
                Destroy(icon);
            }
        }

        internal void RevertAll()
        {
            lock (activeRevertables)
            {
                foreach (KeyValuePair<IRevertable, Coroutine> kvp in activeRevertables)
                {
                    try
                    {
                        LogHelper.Log($"Reverting {(kvp.Key as IUsable).GetName()}");
                        GameManager.instance.StopCoroutine(kvp.Value);
                        kvp.Key.Revert();
                    }
                    catch (Exception e)
                    {
                        LogHelper.LogWarn($"Failed reverting {(kvp.Key as IUsable).GetName()}: {e.Message}");
                        LogHelper.LogWarn($"{e.StackTrace}");
                    }
                }

                activeRevertables.Clear();
            }
        }

        private int CalculateVanillaNailDamageDifference()
        {
            lock (activeRevertables)
            {
                int difference = 0;
                foreach (var kvp in activeRevertables)
                {
                    if (kvp.Key is NailModifierUsable)
                    {
                        NailModifierUsable nailModifier = kvp.Key as NailModifierUsable;
                        difference += nailModifier.GetDamageBuff();
                    }
                }

                return difference;
            }
        }

        internal void OnSave(SaveGameData data)
        {
            data.playerData.nailDamage -= CalculateVanillaNailDamageDifference();
        }

        internal void AfterSave(int id)
        {
            PlayerData.instance.nailDamage += CalculateVanillaNailDamageDifference();
        }
    }
}
