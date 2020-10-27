using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour {

    [SerializeField] private float lifeTime;

    private float timer = 0f;

    void Update() {
        timer += Time.deltaTime;
        if (timer >= lifeTime) Destroy(gameObject);
        
    }
}
