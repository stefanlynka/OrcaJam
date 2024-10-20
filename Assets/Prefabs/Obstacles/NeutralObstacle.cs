
using UnityEngine;

public class NeutralObstacle : Obstacle
{
    public float minScale = 0.5f;
    public float maxScale = 1.0f;

    public SpriteRenderer rectSprite;

    void Start()
    {
        float scale = transform.localScale.x * Random.Range(minScale, maxScale);
        rectSprite.transform.localScale = new Vector3(scale, scale, 1);
        // boxCollider.size = rectSprite.bounds.size;
        GetComponent<CircleCollider2D>().radius *= scale;

        transform.Rotate(new Vector3(0, 0, Random.Range(0, 360)));
    }
}
