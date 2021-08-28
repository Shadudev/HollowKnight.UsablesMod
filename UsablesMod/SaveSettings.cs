using Modding;
using SereCore;

namespace UsablesMod
{
	public class SaveSettings : BaseSettings
	{
		internal SerializableDictionary<string, float> activeUsablesDuration = new SerializableDictionary<string, float>();
		internal SerializableDictionary<int, string> usablesSlots = new SerializableDictionary<int, string>();
	}
}
