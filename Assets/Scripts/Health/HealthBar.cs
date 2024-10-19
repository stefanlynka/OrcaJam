using System;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

    [SerializeField] GameObject progressBarRootContainer;
    [SerializeField] SpriteRenderer progressBarRenderer;
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
        percentage = Mathf.Max(0, percentage);
        progressBarRootContainer.transform.localScale = new Vector3(percentage, 1, 1);
        progressBarRenderer.color = colorGradient.Evaluate(percentage);
    }
}
