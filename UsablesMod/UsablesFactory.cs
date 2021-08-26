using System;
using UsablesMod.Usables;

namespace UsablesMod
{
    class UsablesFactory
    {
        private static readonly int USABLES_AMOUNT = 10;

        private readonly Random random;

        internal static readonly string[] USABLE_NAMES = new string[USABLES_AMOUNT];
        static UsablesFactory()
        {
            for (int i = 0; i < USABLES_AMOUNT; i++)
                USABLE_NAMES[i] = CreateUsableById(i).GetName();
        }

        internal UsablesFactory()
        {
            random = new Random(RandomizerMod.RandomizerMod.Instance.Settings.Seed);
        }

        internal IUsable GetRandomUsable()
        {
            return CreateUsableById(random.Next(USABLES_AMOUNT));
        }

        private static IUsable CreateUsableById(int i)
        {
            switch (i)
            {
                case 0:
                    return new GeoMultiplierUsable();
                case 1:
                    return new HealthUsable();
                case 2:
                    return new LifebloodUsable();
                case 3:
                    return new MPCostUsable();
                case 4:
                    return new NailDamageUsable();
                case 5:
                    return new OvercharmedUsable();
                case 6:
                    return new RespawnUsable();
                case 7:
                    return new ZoomUsable();
                case 8:
                    return new BounceUsable();
                case 9:
                    return new RandomCharmsUsable();
                default:
                    return new SampleUsable();
            }
        }

        internal static bool TryCreateUsable(string descriptor, out IUsable usable)
        {
            for (int i = 0; i < USABLES_AMOUNT; i++)
            {
                IUsable _usable = CreateUsableById(i);
                if (descriptor.StartsWith(_usable.GetName()))
                {
                    usable = _usable;
                    return true;
                }
            }

            usable = null;
            return false;
        }
    }
}
