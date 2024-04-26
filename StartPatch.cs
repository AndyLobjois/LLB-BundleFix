using HarmonyLib; //https://harmony.pardeike.net/articles/patching-postfix.html
using StageBackground;

namespace BundleFix {

    [HarmonyPatch(typeof(BG), nameof(BG.StartUp))]
    public class StartPatch {
        public static void Postfix() { // A postfix is a method that is executed after the original method

            // Nitro Fix
            /// 
            if (!BG.instance.gameObject.GetComponent<NitroFix>())
                BG.instance.gameObject.AddComponent<NitroFix>();

            //// Doombox Fix
            //if (!BG.instance.gameObject.GetComponent<DoomboxFix>())
            //    BG.instance.gameObject.AddComponent<DoomboxFix>();

            //// Dust&Ashes Fix
            //if (!BG.instance.gameObject.GetComponent<DustAndAshesFix>())
            //    BG.instance.gameObject.AddComponent<DustAndAshesFix>();

            //// Candyman Fix
            //if (!BG.instance.gameObject.GetComponent<CandymanFix>())
            //    BG.instance.gameObject.AddComponent<CandymanFix>();
        }
    }
}
