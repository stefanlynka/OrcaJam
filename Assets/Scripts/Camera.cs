using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] Transform followTarget;
    [SerializeField] float smoothTime = 0.3f;
    [SerializeField] Vector3 offset;
    [SerializeField] float zoomScale = 1.0f;
    [SerializeField] Camera cam;

    private Vector2 velocity = Vector2.zero;

    void LateUpdate()
    {
        transform.position = Vector2.SmoothDamp(transform.position, followTarget.position, ref velocity, smoothTime);
        transform.position += offset;

        cam.orthographicSize = zoomScale;

    }


    //DEBUG
    void Update() 
    {
        if (Input.GetKey(KeyCode.Z)) {
            zoomScale += 5 * Time.deltaTime;
        } else if (Input.GetKey(KeyCode.C)) {
            zoomScale -= 5 * Time.deltaTime;
        }
        zoomScale = Math.Max(zoomScale, 1);
    }
}
