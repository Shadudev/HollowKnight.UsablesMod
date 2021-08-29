using System;
using UsablesMod.Usables;

namespace UsablesMod
{
    class UsablesFactory
    {
        internal static readonly string[] USABLES_NAMES;

        private readonly Random random;

        static UsablesFactory()
        {
            USABLES_NAMES = new string[CountUsables()];
            for (int i = 0; i < USABLES_NAMES.Length; i++)
                USABLES_NAMES[i] = CreateUsableById(i).GetName();
        }

        internal UsablesFactory()
        {
            random = new Random(RandomizerMod.RandomizerMod.Instance.Settings.Seed);
        }

        private static int CountUsables()
        {
            int i;
            for (i = 0; !(CreateUsableById(i) is SampleUsable); i++);
            return i;
        }

        internal IUsable GetRandomUsable(int randomSeed = -1)
        {
            return CreateUsableById(random.Next(USABLES_NAMES.Length), randomSeed);
        }

        private static IUsable CreateUsableById(int i, int randomSeed = -1)
        {
            switch (i)
            {
                case 0:
                    return new GeoMultiplierUsable(randomSeed);
                case 1:
                    return new HealthUsable(randomSeed);
                case 2:
                    return new LifebloodUsable(randomSeed);
                case 3:
                    return new MPCostUsable(randomSeed);
                case 4:
                    return new NailModifierUsable(randomSeed);
                case 5:
                    return new OvercharmedUsable();
                case 6:
                    return new RespawnUsable();
                case 7:
                    return new ZoomUsable(randomSeed);
                case 8:
                    return new BounceUsable(randomSeed);
                case 9:
                    return new RandomCharmsUsable(randomSeed);
                default:
                    return new SampleUsable(randomSeed);
            }
        }

        internal static bool TryCreateUsable(string descriptor, out IUsable usable)
        {
            int usableId = NameFormatter.GetIdFromString(descriptor);
            if (usableId != -1)
            {
                for (int i = 0; i < USABLES_NAMES.Length; i++)
                {
                    IUsable _usable = CreateUsableById(i, randomSeed: usableId);
                    if (descriptor.StartsWith(_usable.GetName()))
                    {
                        usable = _usable;
                        return true;
                    }
                }
            }

            usable = null;
            return false;
        }
    }
}
