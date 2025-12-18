using UnityEngine;

public class HealthGauge : MonoBehaviour
{
    SpriteRenderer sr;
    [SerializeField] Sprite[] HPSprites;

    private Color originalColor;

    [SerializeField] private Color flashColor = Color.red; // 데미지 시 깜빡일 색
    [SerializeField] private float flashDuration = 0.25f;   // 깜빡이는 시간

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }

    public void TakeDamage()
    {
        // 데미지를 입었을 때 호출
        StartCoroutine(Flash());
    }

    private System.Collections.IEnumerator Flash()
    {
        // 빨갛게 변경
        sr.color = flashColor;

        // 일정 시간 대기
        yield return new WaitForSeconds(flashDuration);

        // 원래 색으로 복귀
        sr.color = originalColor;
    }

    public void ChangeBar(float per)
    {
        int hpRate = (int)per +1;
        Debug.Log($"현재 적 체력 비율: {hpRate}");
        hpRate = 60 <= hpRate ? 59 : hpRate;
        hpRate = 0 >= hpRate ? 0 : hpRate;
        TakeDamage();
        sr.sprite = HPSprites[hpRate];
    }
}
