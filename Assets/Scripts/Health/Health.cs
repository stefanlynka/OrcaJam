using System;
using UnityEditor;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public float maxHealth = 100.0f;
    [SerializeField] public float currentHealth = 100.0f;
    [SerializeField] GameObject healthBarPrefab;

    private Action onDeathCallback;
    private HealthBar healthBarInstance;

    public Action<Projectile> OnProjectileCollision;

    void Start()
    {
        GameObject healthBarGameObject = Instantiate(healthBarPrefab);
        healthBarInstance = healthBarGameObject.GetComponent<HealthBar>();
        healthBarGameObject.transform.SetParent(transform, false);
    }

    //private float temp = 1.0f;
    void FixedUpdate()
    {
        //healthBarInstance.UpdateProgress(temp);
        //temp -= Time.deltaTime/10;
    }


    public void SetOnDeathCallback(Action onDeathCallback)
    {
        this.onDeathCallback = onDeathCallback;
    }

    public void SetHealth(float newHealth)
    {
        currentHealth = Mathf.Min(newHealth, maxHealth);
        healthBarInstance.UpdateProgress(currentHealth / maxHealth);
        CheckIfAlive();
    }

    public void ChangeHealth(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Max(0, currentHealth);
        healthBarInstance.UpdateProgress(currentHealth / maxHealth);
        CheckIfAlive();
    }

    public void CheckIfAlive() {
        if (currentHealth <= 0) onDeathCallback?.Invoke();
    }

    public void ProjectileCollision(Projectile projectile)
    {
        OnProjectileCollision?.Invoke(projectile);
    }
}
