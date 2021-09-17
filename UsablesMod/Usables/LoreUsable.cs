using RandomizerMod.Randomization;
using SereCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static RandomizerMod.Actions.ChangeShinyIntoText;

namespace UsablesMod.Usables
{
    class LoreUsable : IUsable
    {
        private readonly System.Random random;

        public LoreUsable(int randomSeed)
        {
            random = new System.Random(randomSeed);
        }

        public void Run()
        {
            GameObject dialogueManager = GameObject.Find("DialogueManager");
            GameObject textObj = dialogueManager.transform.Find("Text").gameObject;

            ReqDef lore = GetRandomLore();

            // Set position of text box
            if (lore.textType == TextType.MajorLore)
            {
                textObj.transform.SetPositionY(2.44f);
                dialogueManager.transform.Find("Stop").gameObject.transform.SetPositionY(-0.23f);
                dialogueManager.transform.Find("Arrow").gameObject.transform.SetPositionY(-0.3f);
            }

            switch (lore.textType)
            {
                default:
                case TextType.LeftLore:
                    dialogueManager.LocateMyFSM("Box Open").SendEvent("BOX UP");
                    break;

                case TextType.Lore:
                    dialogueManager.LocateMyFSM("Box Open").SendEvent("BOX UP");
                    break;

                case TextType.MajorLore:
                    PlayMakerFSM.BroadcastEvent("LORE PROMPT UP");
                    break;
            }
            textObj.GetComponent<TMPro.TextMeshPro>().alignment = lore.textType == TextType.LeftLore ?
                TMPro.TextAlignmentOptions.TopLeft : TMPro.TextAlignmentOptions.Top;

            textObj.LocateMyFSM("Dialogue Page Control").GetState("End Conversation").AddAction(
                new FsmStateLambda(() => HideAndReset(dialogueManager, textObj, lore.textType)));
            
            textObj.GetComponent<DialogueBox>().StartConversation(lore.loreKey, string.IsNullOrEmpty(lore.loreSheet) ? "Lore Tablets" : lore.loreSheet);
        }

        private void HideAndReset(GameObject dialogueManager, GameObject textObj, TextType textType)
        {
            HideLoreDialogue(dialogueManager, textType);
            ResetTextBox(dialogueManager, textObj);
        }

        private void HideLoreDialogue(GameObject dialogueManager, TextType textType)
        {
            switch (textType)
            {
                default:
                case TextType.LeftLore:
                case TextType.Lore:
                    dialogueManager.LocateMyFSM("Box Open").SendEvent("BOX DOWN");
                    break;

                case TextType.MajorLore:
                    PlayMakerFSM.BroadcastEvent("LORE PROMPT DOWN");
                    break;
            }
        }

        private void ResetTextBox(GameObject dialogueManager, GameObject textObj)
        {
            textObj.GetComponent<TMPro.TextMeshPro>().alignment = TMPro.TextAlignmentOptions.TopLeft;
            dialogueManager.transform.Find("Arrow").gameObject.transform.SetPositionY(1.695f);
            textObj.transform.SetPositionY(4.49f);
            dialogueManager.transform.Find("Stop").gameObject.transform.SetPositionY(1.695f);
        }

        private ReqDef GetRandomLore()
        {
            HashSet<string> lores = LogicManager.GetItemsByPool("Lore");
            return LogicManager.GetItemDef(lores.ElementAt(random.Next(lores.Count)));
        }

        public void Revert() { }

        public string GetName()
        {
            return "Lore_Usable";
        }
        public string GetDisplayName()
        {
            return "Lore Usable";
        }
        public string GetDescription()
        {
            return "Don't we all love some game lore, provided by Team Schydu.";
        }
        public string GetItemSpriteKey()
        {
            return "ShopIcons.Lore";
        }
    }
}
