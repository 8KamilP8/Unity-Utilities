using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
public class RotateToTransform : MonoBehaviour {
    
    #region Serialize Fields
    [SerializeField] private Transform target;
    [SerializeField] private float delay;
    #endregion

    private float timer = 0f;

    private void Update() {
        if(delay <= 0f) {
            UtilFunc.LookAt2D(transform, target.position);
            return;
        }
        if(timer >= delay) {
            timer = 0f;
        }
        timer += Time.deltaTime;
        transform.rotation = Quaternion.Lerp(transform.rotation, UtilFunc.GetQuaternionFromLookAt2D(transform, target.position), timer / delay);
    }

    public void SetTarget(Transform target) {
        this.target = target;
    }

}
