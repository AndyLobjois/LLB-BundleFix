using HarmonyLib; //https://harmony.pardeike.net/articles/patching-postfix.html
using StageBackground;

namespace BundleFix {

    [HarmonyPatch(typeof(BG), nameof(BG.StartUp))]
    public class StartPatch {
        public static void Postfix() { // A postfix is a method that is executed after the original method

            NitroFix swap = BG.instance.gameObject.GetComponent<NitroFix>();
            if (swap != null) {
                BG.DestroyImmediate(swap);
            }
            BG.instance.gameObject.AddComponent<NitroFix>();
        }
    }
}
