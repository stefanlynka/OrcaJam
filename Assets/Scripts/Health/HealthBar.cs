using System;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] GameObject progressBarRootContainer;
    [SerializeField] SpriteRenderer progressBarRenderer;
    [SerializeField] SpriteRenderer progressBarBackground;
    [SerializeField] Gradient colorGradient;

    void LateUpdate()
    {
        // ensure healthbar stays horizontal
        transform.SetPositionAndRotation(
            transform.position,
            Quaternion.identity
        );
    }

    public void UpdateProgress(float percentage)
    {
        // Only draw if less than 100%
        if (percentage >= 1.0f)
        {
            progressBarRenderer.color = new Color(0, 0, 0, 0);
            progressBarBackground.color = new Color(0, 0, 0, 0);
        }
        else
        {
            progressBarBackground.color = new Color(0, 0, 0, 1);
            percentage = Mathf.Max(0, percentage);
            progressBarRootContainer.transform.localScale = new Vector3(percentage, 1, 1);
            progressBarRenderer.color = colorGradient.Evaluate(percentage);
        }
    }
}
