// ReSharper disable file UnusedMember.Global

namespace UsablesMod
{
    public static class LogHelper
    {
        public static void Log(string message)
        {
            UsablesMod.Instance.Log(message);
        }

        public static void Log(object message)
        {
            UsablesMod.Instance.Log(message);
        }

        public static void LogDebug(string message)
        {
            UsablesMod.Instance.LogDebug(message);
        }

        public static void LogDebug(object message)
        {
            UsablesMod.Instance.LogDebug(message);
        }

        public static void LogError(string message)
        {
            UsablesMod.Instance.LogError(message);
        }

        public static void LogError(object message)
        {
            UsablesMod.Instance.LogError(message);
        }

        public static void LogFine(string message)
        {
            UsablesMod.Instance.LogFine(message);
        }

        public static void LogFine(object message)
        {
            UsablesMod.Instance.LogFine(message);
        }

        public static void LogWarn(string message)
        {
            UsablesMod.Instance.LogWarn(message);
        }

        public static void LogWarn(object message)
        {
            UsablesMod.Instance.LogWarn(message);
        }
    }
}