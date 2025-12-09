using UnityEngine;

//아이템이 필드 아이템으로 있을 때 표현을 위한 스크립트
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
