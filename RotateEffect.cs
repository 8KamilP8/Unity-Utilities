using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
public class RotateEffect : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;
    void Update() {
        transform.Rotate(new Vector3(0f,0f,rotateSpeed * Time.deltaTime));
    }
}
