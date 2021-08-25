using System;

namespace UsablesMod.Usables
{
    class NailDamageUsable : IUsable, IRevertable
    {
        private int originalValue;

        public void Run()
        {
            originalValue = PlayerData.instance.nailDamage;

            Random random = new Random(RandomizerMod.RandomizerMod.Instance.Settings.Seed);
            int damageBuffMultiplier = random.Next(1, 3) == 1 ? 1 : -1;
            int damageBuffAmount = random.Next(1, PlayerData.instance.nailDamage);
            if (damageBuffMultiplier < 0 && PlayerData.instance.nailDamage == 1) return;

            PlayerData.instance.nailDamage += damageBuffMultiplier * damageBuffAmount;
            PlayMakerFSM.BroadcastEvent("UPDATE NAIL DAMAGE");
        }

        public float GetDuration()
        {
            return 20;
        }

        public void Revert()
        {
            PlayerData.instance.nailDamage = originalValue;
            PlayMakerFSM.BroadcastEvent("UPDATE NAIL DAMAGE");
        }

        public string GetName()
        {
            return "NailDamageUsable";
        }
        public string GetDisplayName()
        {
            return "Nail Modifier";
        }
        public string GetDescription()
        {
            return "Feel like your nail is capable of more! Or perhaps you seek for a weaker one.";
        }
        public string GetItemSpriteKey()
        {
            return "ShopIcons.Upslash";
        }
    }
}
