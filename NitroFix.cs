using UnityEngine;
using UnityEngine.SceneManagement;
using LLHandlers;
using GameplayEntities;

namespace BundleFix {

    public class NitroFix : MonoBehaviour {

        private GameObject cuffHolder;
        private GameObject cuffReference;
        private Transform holderPlayer;
        private Transform holderBalls;
        private int playerSpot;

        public void Start() {
            // If Main Menu, don't apply
            if (SceneManager.GetActiveScene().name == "title") return;

            Check();
        }

        void Check() {
            // Init
            holderPlayer = GameObject.Find("holderPlayers").transform;
            holderBalls = GameObject.Find("holderBalls").transform;

            foreach (Transform child in holderPlayer) {
                if (child.name == "copPlayer") {
                    playerSpot = child.GetSiblingIndex();

                    CharacterVariant _characterVariant = child.GetComponent<CopPlayerModel>().player.AIINAIDBHJI;
                    switch (_characterVariant) {
                        case CharacterVariant.DEFAULT:
                        case CharacterVariant.ALT0:
                        case CharacterVariant.ALT1:
                        case CharacterVariant.ALT2:
                        case CharacterVariant.ALT3:
                        case CharacterVariant.ALT4:
                        case CharacterVariant.ALT5:
                        case CharacterVariant.ALT6:
                            CuffFix("cuffVisual", "cuff");
                            break;

                        case CharacterVariant.MODEL_ALT :
                        case CharacterVariant.MODEL_ALT2 :
                            CuffFix("cuffDetectiveVisual", "cuffDetective");
                            break;
                    }
                }
            }
        }

        void CuffFix(string target, string value) {
            for (int a = 0; a < 2; a++) {
                // Assign the cuffHolder
                cuffHolder = holderBalls.GetChild(a).Find(target).gameObject;

                // Add Custom Cuff
                for (int b = 0; b < BundleHandler.assetReferences["characters/cop"].Length; b++) {
                    // Search the Cuff
                    if (BundleHandler.assetReferences["characters/cop"][b].name == value) {
                        // Hide Old Cuff
                        foreach (Transform child in cuffHolder.transform) {
                            child.gameObject.SetActive(false);
                        }

                        // Reference
                        cuffReference = BundleHandler.assetReferences["characters/cop"][b] as GameObject;

                        // Instantiate, change Material, set Layer
                        GameObject _cuff = Instantiate(cuffReference, cuffHolder.transform);
                        BundleFixPlugin.SetLayerAllChildren(_cuff.transform, 8);
                        Material _mat = GameObject.Find("holderPlayers/copPlayer/main/buntEffect").GetComponent<SkinnedMeshRenderer>().materials[0];
                        _cuff.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = _mat;

                        // Stop the search
                        BundleFixPlugin.log($"Nitro {cuffHolder.name}.{target} have been replaced for Player {playerSpot}");
                        break;
                    }

                    // If it doesn't find the Cuff
                    if (b == BundleHandler.assetReferences["characters/cop"].Length - 1) {
                        // For now, do nothing ...
                        /// Issue: Original Nitro bundle doesn't have a "cuff", so BundleFix can't fix it which is annoying.
                        /// But is there anything to fix if you play OG Nitro ?
                    }
                }
            }
        }
    }
}
