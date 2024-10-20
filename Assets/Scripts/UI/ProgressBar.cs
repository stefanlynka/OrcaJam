using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{

    public Image progressBarImage;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateProgress(float progress) {
        transform.localScale = new Vector3(progress, 1, 1);
    }
}
