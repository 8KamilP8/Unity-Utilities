using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeCanvasChild : MonoBehaviour
{
    void Start() {
        Transform canvasTransform = FindObjectOfType<Canvas>().transform;

        transform.SetParent(canvasTransform);
    }
    
}
