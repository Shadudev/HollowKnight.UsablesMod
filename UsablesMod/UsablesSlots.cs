using Modding;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UsablesMod.Usables;

namespace UsablesMod
{
    // Based on RandomizerMod.RecentItems
    class UsablesSlots
    {
        private int invPanels;

        private readonly IUsable[] usables;
        private readonly GameObject[] usablesIcons;
        private GameObject canvas;

        private readonly UsablesExecuter usablesExecuter;

        private Coroutine usablesTriggerRoutine;

        public UsablesSlots()
        {
            invPanels = 0;
            usables = new IUsable[2];
            usablesIcons = new GameObject[2];

            usablesExecuter = new UsablesExecuter();
        }

        private void Create()
        {
            if (canvas != null) return;
            // Create base canvas
            
            canvas = CanvasUtil.CreateCanvas(RenderMode.ScreenSpaceOverlay, new Vector2(Screen.width, Screen.height));
            Object.DontDestroyOnLoad(canvas);

            CanvasUtil.CreateTextPanel(canvas, "Usables", 18, TextAnchor.MiddleCenter,
                new CanvasUtil.RectData(new Vector2(200, 100), Vector2.zero,
                new Vector2(0.941f, 0.18f), new Vector2(0.981f, 0.18f)));

            CanvasUtil.CreateImagePanel(canvas, RandomizerMod.RandomizerMod.GetSprite("ShopIcons.Map"),
                new CanvasUtil.RectData(new Vector2(33, 33), Vector2.zero,
                new Vector2(0.963f, 0.09f), new Vector2(0.963f, 0.09f)));

            CanvasUtil.CreateImagePanel(canvas, RandomizerMod.RandomizerMod.GetSprite("ShopIcons.Upslash"),
                new CanvasUtil.RectData(new Vector2(33, 33), Vector2.zero,
                new Vector2(0.963f, 0.16f), new Vector2(0.963f, 0.16f)));

            GameObject downSlashPanel = CanvasUtil.CreateBasePanel(canvas,
                new CanvasUtil.RectData(new Vector2(33, 33), Vector2.zero,
                new Vector2(0.963f, 0.018f), new Vector2(0.963f, 0.018f)));
            Image downSlashImage = downSlashPanel.AddComponent<Image>();
            downSlashImage.sprite = RandomizerMod.RandomizerMod.GetSprite("ShopIcons.Upslash");
            downSlashImage.preserveAspect = true;
            downSlashImage.transform.rotation = Quaternion.Euler(0, 0, 180f);

            if (invPanels <= 0) Show();
        }

        public void Destroy()
        {
            if (canvas != null) Object.Destroy(canvas);
            canvas = null;
        }

        public void Add(string itemName, IUsable usable)
        {
            if (canvas == null)
            {
                Create();
            }

            int slotIndex;

            if (IsFullyOccupied())
            {
                slotIndex = new System.Random(RandomizerMod.RandomizerMod.Instance.Settings.Seed +
                    100 + NameFormatter.GetIdFromString(itemName)).Next(2);
                Run(Pop(slotIndex == 0));
            }
            else
            {
                slotIndex = usables[0] == null ? 0 : 1;
            }

            float height = 0.145f - 0.065f * slotIndex;
            GameObject basePanel = CanvasUtil.CreateBasePanel(canvas,
                new CanvasUtil.RectData(new Vector2(50, 50), Vector2.zero,
                new Vector2(0.976f, height), new Vector2(0.976f, height)));

            CanvasUtil.CreateImagePanel(basePanel, RandomizerMod.RandomizerMod.GetSprite(usable.GetItemSpriteKey()),
                new CanvasUtil.RectData(new Vector2(50, 50), Vector2.zero, new Vector2(0f, 0f),
                    new Vector2(0f, 0f)));

            usables[slotIndex] = usable;
            usablesIcons[slotIndex] = basePanel;
        }

        public (IUsable Usable, GameObject Icon) Pop(bool wasUpperSlotUsed)
        {
            int slotIndex = wasUpperSlotUsed ? 0 : 1;


            IUsable usable = usables[slotIndex];
            usables[slotIndex] = null;

            GameObject icon = usablesIcons[slotIndex];
            usablesIcons[slotIndex] = null;

            return (usable, icon);
        }

        private void Run((IUsable Usable, GameObject icon) pair)
        {
            if (pair.Usable is IRevertable)
            {
                IRevertable revertable = pair.Usable as IRevertable;
                GameManager.instance.StartCoroutine(ShowDuration(pair.icon, revertable));
            }
            else
            {
                Object.Destroy(pair.icon);
            }
            usablesExecuter.RunUsable(pair.Usable);
        }

        Image OnPostRender()
        {
            LogHelper.Log("Rendering Image");
            GameObject basePanel = CanvasUtil.CreateBasePanel(canvas,
                new CanvasUtil.RectData(new Vector2(50, 50), Vector2.zero,
                new Vector2(0.97f, 0.13f - 0.06f), new Vector2(0.97f, 0.13f - 0.06f)));
            Texture2D whiteText = Texture2D.whiteTexture;
            Sprite whiteSquare = Sprite.Create(whiteText, new Rect(0.0f, 0.0f, whiteText.width, whiteText.height), new Vector2(0.5f, 0.5f), 100.0f);
            GameObject panel = CanvasUtil.CreateImagePanel(basePanel, whiteSquare, new CanvasUtil.RectData(new Vector2(50, 50), Vector2.zero, new Vector2(0f, 0f),new Vector2(0f, 0f)));
            Image img = panel.GetComponent<Image>();
            img.type = Image.Type.Filled;
            img.fillMethod = Image.FillMethod.Radial360;
            img.fillAmount = 0f;
            
            return img;
        }

        internal IEnumerator ShowDuration(GameObject usableIcon, IRevertable revertable)
        {
            LogHelper.Log($"usableIcon: {usableIcon == null}, revertable: {revertable == null}");
            LogHelper.Log($"usableIcon.GetComponentInChildren<Image>() {GameObject.Find("Image") == null}");
            Color usableIconColor = usableIcon.GetComponentInChildren<Image>().color;
            LogHelper.Log($"usableIconColor {usableIconColor == null}");
            float duration = revertable.GetDuration();
            float timer = 0f;
            Image img = OnPostRender();
          
            while (timer <= duration)
            {
                LogHelper.Log($"img.fillAmount {img.fillAmount}");

                //usableIconColor.a -= 0.1f;
                img.fillAmount += Map(timer, 1f, duration,0.1f, 1f);
                yield return new WaitForSeconds(0.1f);
            }

            Object.Destroy(usableIcon);
        }

        private float Map(float s, float a1, float a2, float b1, float b2)
        {
            return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
        }

        private IEnumerator TriggerUsablesByInputs()
        {
            while (!GameManager.instance.IsMenuScene())
            {
                if (InputHandler.Instance.inputActions.quickMap.IsPressed)
                {
                    if (InputHandler.Instance.inputActions.up.IsPressed)
                    {
                        if (usables[0] != null)
                            Run(Pop(true));
                    }
                    else if (InputHandler.Instance.inputActions.down.IsPressed)
                    {
                        if (usables[1] != null)
                            Run(Pop(false));
                    }
                }

                yield return new WaitForSeconds(0.1f);
            }
        }

        public bool IsFullyOccupied()
        {
            return usablesIcons[0] != null && usablesIcons[1] != null;
        }

        public void Show()
        {
            if (canvas == null) return;
            canvas.SetActive(true);
            usablesTriggerRoutine = GameManager.instance.StartCoroutine(TriggerUsablesByInputs());
        }

        public void Hide()
        {
            if (canvas == null) return;
            canvas.SetActive(false);
            if (usablesTriggerRoutine != null)
            {
                GameManager.instance.StopCoroutine(usablesTriggerRoutine);
                usablesTriggerRoutine = null;
            }
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
