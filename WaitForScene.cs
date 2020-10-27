using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
public class WaitForScene : MonoBehaviour
{
    SceneMarker flag;
    private void Update() {
        flag = FindObjectOfType<SceneMarker>();
        if (flag != null) {            
            
            Wait();

            Destroy(gameObject);
        }

    }
    public void Wait() {
        MonoBehaviourMultiScene[] MMS = FindObjectsOfType<MonoBehaviourMultiScene>();
        foreach (MonoBehaviourMultiScene mms in MMS) {
            mms.OnMultiSceneStart();
        }
    }
    
}


