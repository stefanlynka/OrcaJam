using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{

    public Image progressBarImage;

    private float currentProgress = 0;
    private float targetProgress = 0;
    float lerpSpeed = 0.7f;
    // Update is called once per frame
    void Update()
    {
        currentProgress = Mathf.Lerp(currentProgress, targetProgress, lerpSpeed * Time.deltaTime);
        transform.localScale = new Vector3(currentProgress, 1, 1);
    }

    public void UpdateProgress(float progress) {
        targetProgress = progress;
    }
}
