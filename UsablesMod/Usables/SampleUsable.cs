using static UsablesMod.LogHelper;

namespace UsablesMod.Usables
{
    class SampleUsable : IUsable
    {
        public void Run()
        {
            Log("Running SampleUsable");
        }
        
        public bool IsRevertable()
        {
            return true;
        }

        public float GetDuration()
        {
            return 12f;
        }

        public void Revert()
        {
            Log("Reverting SampleUsable");
        }

        public string GetName()
        {
            return "SampleUsable";
        }
    }
}
