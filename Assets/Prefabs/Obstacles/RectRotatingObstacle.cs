
using UnityEngine;

public class RectRotatingObstacle : Obstacle
{

    public float rotationRate = 1.0f;
    public float maxWidth = 15.0f;
    public float minWidth = 1.0f;

    public SpriteRenderer rectSprite;
    private float adjustedRotationRate;

    void Start()
    {
        rectSprite.transform.localScale = new Vector3(rectSprite.transform.localScale.x, Random.Range(minWidth, maxWidth), 1);
        boxCollider.size = rectSprite.bounds.size;

        adjustedRotationRate = rotationRate / rectSprite.transform.localScale.y;
        transform.Rotate(new Vector3(0, 0, Random.Range(0, 360)));
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, rotationRate * Time.deltaTime));
    }
}
