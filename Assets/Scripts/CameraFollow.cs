using System;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] Transform followTarget;
    [SerializeField] float lerpSpeed = 0.3f;
    [SerializeField] float mouseOffsetFactor = 2.0f;
    [SerializeField] Vector3 offset;
    [SerializeField] float zoomScale = 1.0f;
    [SerializeField] float cameraZoomLerpSpeed = 0.5f;
    [SerializeField] Camera cam;
    [SerializeField] GameObject levelBackground;
    [SerializeField] float zoomOutOnLevelCompleteSpeed = 5.0f;
    [SerializeField] float maxZoomOnLevelComplete = 100.0f;

    private Vector2 velocity = Vector2.zero;
    private Vector3 targetMouseOffset = Vector3.zero;
    private float currentZoom;

    private bool isZoomingOutForLevelCompletion = false;
    private Action onFinishZoomOutCallback;

    void Start()
    {
        currentZoom = zoomScale;
    }

    void LateUpdate()
    {
        // transform.position = Vector2.SmoothDamp(transform.position, followTarget.position, ref velocity, smoothTime);
        transform.position = Vector2.Lerp(transform.position, followTarget.position + targetMouseOffset, lerpSpeed * Time.deltaTime);
        transform.position += offset;

        // smoothly lerp to target camera zoom scale
        currentZoom = Mathf.Lerp(currentZoom, zoomScale, cameraZoomLerpSpeed * Time.deltaTime);
        cam.orthographicSize = currentZoom;

        if (isZoomingOutForLevelCompletion) {
            if (currentZoom >= maxZoomOnLevelComplete) onFinishZoomOutCallback?.Invoke();
            else ChangeZoomExp(zoomOutOnLevelCompleteSpeed * Time.deltaTime);
            currentZoom = Mathf.Min(currentZoom, maxZoomOnLevelComplete);
        }

    }

    void Update()
    {
        if (isZoomingOutForLevelCompletion)
        {
            targetMouseOffset = new Vector3(0, 0, 0);
        }
        else
        {

            // update mouse camera offset
            targetMouseOffset = Camera.main.ScreenToViewportPoint(Input.mousePosition) - new Vector3(0.5f, 0.5f, 0);
            targetMouseOffset = targetMouseOffset * mouseOffsetFactor;

        }
        //DEBUG
        float mouseScrollWheelAmount = Input.GetAxis("Mouse ScrollWheel");
        float change = -1 * mouseScrollWheelAmount * 750 * Time.deltaTime;
        zoomScale += change;
        //if (Input.GetKey(KeyCode.Z))
        //{
        //    zoomScale += 5 * Time.deltaTime;
        //}
        //else if (Input.GetKey(KeyCode.C))
        //{
        //    zoomScale -= 5 * Time.deltaTime;
        //}
        zoomScale = Mathf.Max(zoomScale, 1);
    }

    public void SetZoom(float newZoom)
    {
        zoomScale = newZoom;
    }

    public void ChangeZoom(float delta)
    {
        zoomScale += delta;
        zoomScale = Mathf.Max(zoomScale, 5);
    }

    public void ChangeZoomExp(float value) {
        zoomScale *= 1 + value;
    }

    public void ZoomOutOnLevelCompletion(Action onFinish)
    {
        isZoomingOutForLevelCompletion = true;
        followTarget = levelBackground.transform;
        onFinishZoomOutCallback = onFinish;
    }
}
