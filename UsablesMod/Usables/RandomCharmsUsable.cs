using System.Collections.Generic;
namespace UsablesMod.Usables
{
    class RandomCharmsUsable : IUsable
    {
        List<int> ownedCharms = new List<int>();
        public void Run()
        {
            System.Random rnd = new System.Random();
            for (int i = 1; i < 41; i++)
            {
                if (PlayerData.instance.GetBool("gotCharm_" + i))
                {
                    ownedCharms.Add(i);
                }
                if (PlayerData.instance.GetBool("equippedCharm_" + i))
                {
                    PlayerData.instance.SetBool("equippedCharm_" + i, false);
                    GameManager.instance.UnequipCharm(i);
                }
            }
            if (ownedCharms.Count == 0) return;
            int equippedCharmsCount = rnd.Next(1, ownedCharms.Count + 1);
            for (int i = 0; i < equippedCharmsCount; i++)
            {
                int charmIndex = rnd.Next(ownedCharms.Count);
                PlayerData.instance.SetBool("equippedCharm_" + charmIndex, true);
                GameManager.instance.EquipCharm(ownedCharms[charmIndex]);
                ownedCharms.RemoveAt(charmIndex);
            }
            HeroController.instance.CharmUpdate();
            GameManager.instance.RefreshOvercharm();
        }

        public bool IsRevertable()
        {
            return false;
        }

        public float GetDuration()
        {
            return 10f;
        }

        public void Revert()
        {
        }

        public string GetName()
        {
            return "RandomCharmsUsable";
        }
    }
}

