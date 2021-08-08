namespace UsablesMod.Usables
{
    interface IUsable
    {
        void Run();
        bool IsRevertable();
        float GetDuration();
        void Revert();
        string GetName();
    }
}
