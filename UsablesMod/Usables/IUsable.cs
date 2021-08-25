namespace UsablesMod.Usables
{
    interface IUsable
    {
        void Run();
        string GetName();
        string GetDisplayName();
        string GetDescription();
        string GetItemSpriteKey();
    }
}
