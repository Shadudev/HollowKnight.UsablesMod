using System;

namespace UsablesMod.Usables
{
    class NailModifierUsable : IUsable, IRevertable
    {
        private readonly Random random;
        private int damageBuffAmount;
        private float nailScaleMultiplier;

        public NailModifierUsable(int randomSeed)
        {
            random = new Random(randomSeed);
            damageBuffAmount = 0;
        }

        public void Run()
        {
            if (random.Next(2) == 0)
                nailScaleMultiplier = random.Next(15, 25) / 10f;
            else
                nailScaleMultiplier = random.Next(2, 6) / 10f;
            On.NailSlash.StartSlash += ChangeNailScale;

            int damageBuffMultiplier = random.Next(1, 3) == 1 ? 1 : -1;
            damageBuffAmount = random.Next(1, PlayerData.instance.nailDamage) * damageBuffMultiplier;
            if (damageBuffMultiplier < 0 && PlayerData.instance.nailDamage == 1) return;

            PlayerData.instance.nailDamage += damageBuffAmount;
            PlayMakerFSM.BroadcastEvent("UPDATE NAIL DAMAGE");
        }

        private void ChangeNailScale(On.NailSlash.orig_StartSlash orig, NailSlash self)
        {
            orig(self);

            self.transform.localScale *= nailScaleMultiplier;
        }

        public float GetDuration()
        {
            return 90;
        }

        public void Revert()
        {
            On.NailSlash.StartSlash -= ChangeNailScale;

            PlayerData.instance.nailDamage -= damageBuffAmount;
            PlayMakerFSM.BroadcastEvent("UPDATE NAIL DAMAGE");
        }

        public string GetName()
        {
            return "Nail_Modifier_Usable";
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
