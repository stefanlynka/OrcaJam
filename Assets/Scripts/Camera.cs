using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] Transform followTarget;
    [SerializeField] float lerpSpeed = 0.3f;
    [SerializeField] float mouseOffsetFactor = 2.0f;
    [SerializeField] Vector3 offset;
    [SerializeField] float zoomScale = 1.0f;
    [SerializeField] Camera cam;

    private Vector2 velocity = Vector2.zero;
    private Vector3 targetMouseOffset = Vector3.zero;

    void LateUpdate()
    {
        // transform.position = Vector2.SmoothDamp(transform.position, followTarget.position, ref velocity, smoothTime);
        transform.position = Vector2.Lerp(transform.position, followTarget.position + targetMouseOffset, lerpSpeed * Time.deltaTime);
        transform.position += offset;

        cam.orthographicSize = zoomScale;

    }


    
    void Update() 
    {
        // update mouse camera offset
        targetMouseOffset = Camera.main.ScreenToViewportPoint(Input.mousePosition) - new Vector3(0.5f, 0.5f, 0);
        targetMouseOffset = targetMouseOffset * mouseOffsetFactor;

        //DEBUG
        if (Input.GetKey(KeyCode.Z)) {
            zoomScale += 5 * Time.deltaTime;
        } else if (Input.GetKey(KeyCode.C)) {
            zoomScale -= 5 * Time.deltaTime;
        }
        zoomScale = Mathf.Max(zoomScale, 1);
    }
}
