using Modding;
using System.Collections;
using UnityEngine;
using UsablesMod.Usables;

namespace UsablesMod
{
    // Based on RandomizerMod.RecentItems
    class UsablesSlots
    {
        private int invPanels;

        private readonly string[] usablesNames;
        private readonly GameObject[] usablesIcons;
        private GameObject canvas;

        public UsablesSlots()
        {
            invPanels = 0;
            usablesNames = new string[2];
            usablesIcons = new GameObject[2];
        }

        private void Create()
        {
            if (canvas != null) return;
            // Create base canvas
            
            canvas = CanvasUtil.CreateCanvas(RenderMode.ScreenSpaceOverlay, new Vector2(Screen.width, Screen.height));
            Object.DontDestroyOnLoad(canvas);

            CanvasUtil.CreateTextPanel(canvas, "Usables", 18, TextAnchor.MiddleCenter,
                new CanvasUtil.RectData(new Vector2(200, 100), Vector2.zero,
                new Vector2(0.94f, 0.15f), new Vector2(0.98f, 0.15f)));

            if (invPanels <= 0) Show();
        }

        public void Destroy()
        {
            if (canvas != null) Object.Destroy(canvas);
            canvas = null;
        }

        public void Add(string itemName, string spriteKey)
        {
            if (canvas == null)
            {
                Create();
            }

            int slotIndex = usablesIcons[0] == null ? 0 : 1;
            float height = 0.13f - 0.06f * slotIndex;

            GameObject basePanel = CanvasUtil.CreateBasePanel(canvas,
                new CanvasUtil.RectData(new Vector2(50, 50), Vector2.zero,
                new Vector2(0.97f, height), new Vector2(0.97f, height)));

            CanvasUtil.CreateImagePanel(basePanel, RandomizerMod.RandomizerMod.GetSprite(spriteKey),
                new CanvasUtil.RectData(new Vector2(50, 50), Vector2.zero, new Vector2(0f, 0f),
                    new Vector2(0f, 0f)));

            usablesNames[slotIndex] = itemName;
            usablesIcons[slotIndex] = basePanel;
        }

        public string Pop(bool wasUpperSlotUsed)
        {
            int slotIndex = wasUpperSlotUsed ? 0 : 1;
            Object.Destroy(usablesIcons[slotIndex]);
            usablesIcons[slotIndex] = null;

            string usableName = usablesNames[slotIndex];
            usablesNames[slotIndex] = null;
            return usableName;
        }

        public string GetString(int index)
        {
            return usablesNames[index];
        }

        internal IEnumerator updatingDuration(float duration)
        {
            Color usablesColour = usablesIcons[0].gameObject.GetComponent<SpriteRenderer>().color;
            while (usablesColour.a != 0f)
            {
                usablesColour.a -= 0.1f;
                yield return new WaitForSeconds(duration/10);
            }
            Object.Destroy(usablesIcons[0]);
            usablesIcons[0] = null;

            string usableName = usablesNames[0];
            usablesNames[0] = null;
        }

        public bool IsFullyOccupied()
        {
            return usablesIcons[0] != null && usablesIcons[1] != null;
        }

        public void Show()
        {
            if (canvas == null) return;
            canvas.SetActive(true);
        }

        public void Hide()
        {
            if (canvas == null) return;
            canvas.SetActive(false);
        }

        internal void Hook()
        {
            UnHook();

            ModHooks.Instance.AfterSavegameLoadHook += OnLoad; 
            On.QuitToMenu.Start += OnQuitToMenu;
            On.InvAnimateUpAndDown.AnimateUp += OnInventoryOpen;
            On.InvAnimateUpAndDown.AnimateDown += OnInventoryClose;
            On.UIManager.GoToPauseMenu += OnPause;
            On.UIManager.UIClosePauseMenu += OnUnpause;
        }

        internal void UnHook()
        {
            ModHooks.Instance.AfterSavegameLoadHook -= OnLoad;
            On.QuitToMenu.Start -= OnQuitToMenu;
            On.InvAnimateUpAndDown.AnimateUp -= OnInventoryOpen;
            On.InvAnimateUpAndDown.AnimateDown -= OnInventoryClose;
            On.UIManager.GoToPauseMenu -= OnPause;
            On.UIManager.UIClosePauseMenu -= OnUnpause;
        }

        private void OnLoad(SaveGameData data)
        {
            Create();
        }

        private IEnumerator OnQuitToMenu(On.QuitToMenu.orig_Start orig, QuitToMenu self)
        {
            Destroy();
            return orig(self);
        }

        private void OnInventoryOpen(On.InvAnimateUpAndDown.orig_AnimateUp orig, InvAnimateUpAndDown self)
        {
            orig(self);
            invPanels++;
            Hide();
        }

        private void OnInventoryClose(On.InvAnimateUpAndDown.orig_AnimateDown orig, InvAnimateUpAndDown self)
        {
            orig(self);
            invPanels--;
            if (invPanels <= 0) Show();
        }

        private IEnumerator OnPause(On.UIManager.orig_GoToPauseMenu orig, UIManager self)
        {
            invPanels = 0;
            Hide();
            return orig(self);
        }

        private void OnUnpause(On.UIManager.orig_UIClosePauseMenu orig, UIManager self)
        {
            orig(self);
            Show();
        }
    }
}
