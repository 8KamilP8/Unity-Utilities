using UnityEngine;
using Utilities;

public class ScriptInitiator : MonoBehaviour {
    [Header("ONLY SCRIPTS WITH IInitializable INTERFACE!")]
    public Object[] ScriptsToInitialiseReferences;
    private void Start() {
        for(int i = 0; i < ScriptsToInitialiseReferences.Length; i++) {
            IInitializable init = (IInitializable)ScriptsToInitialiseReferences[i];
            init.Init();
        }
    }
}
