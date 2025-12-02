using UnityEngine;

public class ItemIMGMovement : MonoBehaviour
{
    private float amplitude = 0.3f;
    [SerializeField] public float speed = 8f;

    private Vector2 startPos;

    private void Start()
    {
        startPos = this.transform.position;
    }
    void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * speed) * amplitude;
        transform.position = new Vector2(startPos.x, newY+0.15f);

    }
}
