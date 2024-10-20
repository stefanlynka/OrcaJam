using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public SpriteRenderer levelRenderer;
    public GameObject[] enemyPrefabs;
    public float spawnRate = 7.0f;

    private float levelRadius;

    void Start()
    {
        levelRadius = levelRenderer.bounds.extents.x;
        TimerManager.Instance.AddTimer(new SimpleTimer(SpawnEnemy, gameObject, spawnRate, true));
    }

    void SpawnEnemy()
    {   
        GameObject enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length - 1)]);
        enemy.transform.position = Random.insideUnitCircle * levelRadius;
    }
}
