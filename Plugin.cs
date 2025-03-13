using BepInEx;
using BepInEx.Logging;

namespace Gaylatea
{
    namespace UseLooseLoot
    {
        [BepInPlugin("com.gaylatea.uselooseloot", "SPT-UseLooseLoot", "1.4.0")]
        [BepInDependency("com.SPT.core", "3.11.0")]
        public class Plugin : BaseUnityPlugin
        {
            internal static ManualLogSource logger;

            public Plugin()
            {
                logger = Logger;

                new MakeFoodMedsUsablePatch().Enable();

                Logger.LogInfo($"[UseLooseLoot] Loaded.");
            }
        }
    }
}