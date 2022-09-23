using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/*
 BundleFix is a mod that fix the inconsistencies of LLB Characater bundles (some things are impossible without a fix)
 Execution order: BundleFixPlugin → StartPatch → Fix Classes
 */

namespace BundleFix {

    [BepInPlugin(BundleFixInfos.PLUGIN_ID, BundleFixInfos.PLUGIN_NAME, BundleFixInfos.PLUGIN_VERSION)]
    [BepInDependency(LLBML.PluginInfos.PLUGIN_ID, BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("no.mrgentle.plugins.llb.modmenu", BepInDependency.DependencyFlags.SoftDependency)]

    public class BundleFixPlugin : BaseUnityPlugin {
        public static BundleFixPlugin instance;
        public static DirectoryInfo PluginDir => LLBML.Utils.ModdingFolder.GetModSubFolder(instance.Info);
        internal static ManualLogSource Log { get; private set; }

        void Awake() {
            instance = this;
            Log = this.Logger;

            var harmony = new Harmony(BundleFixInfos.PLUGIN_NAME);
            harmony.PatchAll();

            Log.LogInfo(PluginDir);
            log("BundleFix is loaded");
        }

        static public void log(string message) {
            Debug.Log($"<color=cyan>[BundleFix] {message}</color>");
        }

        static public void SetLayerAllChildren(Transform root, int layer) {
            var children = root.GetComponentsInChildren<Transform>(includeInactive: true);
            foreach (var child in children) {
                //            Debug.Log(child.name);
                child.gameObject.layer = layer;
            }
        }
    }
}
