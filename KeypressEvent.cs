using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using ContentLibrary;
using KeypressEvent.Content;
using UnityEngine;

namespace KeypressEvent
{
    [ContentWarningPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_VERSION, false)]
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    [BepInDependency(ContentLibrary.MyPluginInfo.PLUGIN_GUID, BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency(MyceliumNetworking.MyPluginInfo.PLUGIN_GUID, BepInDependency.DependencyFlags.HardDependency)]
    public class KeypressEvent : BaseUnityPlugin
    {
        public static KeypressEvent Instance { get; private set; } = null!;
        internal new static ManualLogSource Logger { get; private set; } = null!;
        internal static Harmony? Harmony { get; set; }
        internal static ushort modID = (ushort)Hash128.Compute(MyPluginInfo.PLUGIN_GUID).GetHashCode();
        private void Awake()
        {
            Logger = base.Logger;
            Instance = this;

            ContentHandler.AssignEvent(new KeypressContentEvent());

            // Initialize mod on persistent GameObject
            var scritpGameObject = new GameObject("KeypressEvent Persistent");
            scritpGameObject.AddComponent<KeypressHandler>();
            scritpGameObject.hideFlags = HideFlags.HideAndDontSave;
            DontDestroyOnLoad(scritpGameObject);

            Patch();

            Logger.LogInfo($"{MyPluginInfo.PLUGIN_GUID} v{MyPluginInfo.PLUGIN_VERSION} has loaded!");
        }

        internal static void Patch()
        {
            Harmony ??= new Harmony(MyPluginInfo.PLUGIN_GUID);

            Logger.LogDebug("Patching...");

            Harmony.PatchAll();

            Logger.LogDebug("Finished patching!");
        }

        internal static void Unpatch()
        {
            Logger.LogDebug("Unpatching...");

            Harmony?.UnpatchSelf();

            Logger.LogDebug("Finished unpatching!");
        }
    }
}
