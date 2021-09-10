using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UsablesMod
{
    class ActiveUsablesBar
    {
        private readonly Dictionary<GameObject, Func<float>> iconsDurations;
        private readonly List<GameObject> iconsBar;

        internal ActiveUsablesBar()
        {
            iconsDurations = new Dictionary<GameObject, Func<float>>();
            iconsBar = new List<GameObject>();
        }

        internal void Add(GameObject icon, Func<float> getDurationFunc)
        {
            iconsDurations[icon] = getDurationFunc;
            iconsBar.Add(icon);
            GameManager.instance.StartCoroutine(ShowDuration(icon));
        }

        internal IEnumerator ShowDuration(GameObject icon)
        {
            Vector2 newPos = new Vector2(0.11f + 0.03f * iconsBar.IndexOf(icon), 0.76f);
            icon.GetComponent<RectTransform>().anchorMin = newPos;
            icon.GetComponent<RectTransform>().anchorMax = newPos;
            
            float timer = 0;
            Image iconImg = icon.GetComponentInChildren<Image>();
            iconImg.type = Image.Type.Filled;
            iconImg.fillMethod = Image.FillMethod.Radial360;
            iconImg.fillAmount = 0f;
            
            while (iconsDurations.ContainsKey(icon) && timer < iconsDurations[icon]())
            {
                float duration = iconsDurations[icon]();
                iconImg.fillAmount = Map(duration - timer, duration, 1f, 1f, 0.1f);
                timer += 0.1f;
                yield return new WaitForSeconds(0.1f);
            }
        }

        private float Map(float s, float a1, float a2, float b1, float b2)
        {
            return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
        }

        internal void Remove(GameObject icon)
        {
            iconsDurations.Remove(icon);
            iconsBar.Remove(icon);

            try
            {
                UpdatePositions();
            }
            catch { } // Even if exception is thrown, next UpdatePosition calls will fix positions
        }

        private void UpdatePositions()
        {
            for (int i = 0; i < iconsBar.Count; i++)
            {
                Vector2 newPos = new Vector2(0.11f + 0.03f * i, 0.76f);
                iconsBar[i].GetComponent<RectTransform>().anchorMin = newPos;
                iconsBar[i].GetComponent<RectTransform>().anchorMax = newPos;
            }
        }
    }
}
