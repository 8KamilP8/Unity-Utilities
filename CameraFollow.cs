using UnityEngine;
using System.Collections;
public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float z = -10f;
    [SerializeField] private float cameraMoveTime;

    private Transform cameraTransform;
    private float time;
    private Rigidbody2D rb;
    private Vector3 velocity = Vector3.zero;
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        cameraTransform = Camera.main.transform;
    }
    private void FixedUpdate() {
        Vector3 targetPosition = transform.position;
        targetPosition.z = z;

        cameraTransform.position = Vector3.SmoothDamp(cameraTransform.position, targetPosition, ref velocity, cameraMoveTime);
    }

}
