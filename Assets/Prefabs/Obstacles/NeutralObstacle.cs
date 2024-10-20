
using UnityEngine;

public class NeutralObstacle : Obstacle
{

    public float rotationRate = 1.0f;
    public float widthMinScale = 1.0f;
    public float widthMaxScale = 8.0f;
    public float heightMinScale = 0.2f;
    public float heightMaxScale = 1.0f;

    public SpriteRenderer rectSprite;
    private float adjustedRotationRate;

    void Start()
    {
        rectSprite.transform.localScale = new Vector3(Random.Range(widthMinScale, widthMaxScale), Random.Range(heightMinScale, heightMaxScale), 1);
        boxCollider.size = rectSprite.bounds.size;

        adjustedRotationRate = rotationRate / rectSprite.transform.localScale.y;
        transform.Rotate(new Vector3(0, 0, Random.Range(0, 360)));
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, rotationRate * Time.deltaTime));
    }
}
