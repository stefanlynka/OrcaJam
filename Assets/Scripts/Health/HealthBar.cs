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
        if (percentage >= 1.0f || percentage <= 0) SetVisibility(false);        
        else {
            SetVisibility(true);
            percentage = Mathf.Max(0, percentage);
            progressBarRootContainer.transform.localScale = new Vector3(percentage, 1, 1);
            progressBarRenderer.color = colorGradient.Evaluate(percentage);
        }
    }

    public void SetVisibility(bool isVisible) {
        if (isVisible) transform.localScale = Vector3.one;
        else transform.localScale = Vector3.zero;
    }
}
