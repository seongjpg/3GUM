using System.Collections;
using UnityEngine;

public class PlayerVisualHandler : MonoBehaviour
{
    private SpriteRenderer sr;
    private Coroutine currentBlink;
    private Color originalColor;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }

    // 기본 깜빡임 (피격용)
    public void Blink(float duration, float interval = 0.1f)
    {
        StopCurrentEffect();
        currentBlink = StartCoroutine(BlinkRoutine(duration, interval));
    }

    // 색상 변경 + 깜빡임 (아이템 효과용)
    public void ColorBlink(Color color, float duration, float interval = 0.15f)
    {
        StopCurrentEffect();
        currentBlink = StartCoroutine(ColorBlinkRoutine(color, duration, interval));
    }

    // 색상만 일시 변경 (깜빡임 없이)
    public void ConvertColor(Color color, float duration)
    {
        StopCurrentEffect();
        currentBlink = StartCoroutine(ConvertColorRoutine(color, duration));
    }

    // 진행 중인 효과 즉시 중단 + 원상복구
    public void StopCurrentEffect()
    {
        if (currentBlink != null) {
            StopCoroutine(currentBlink);
            currentBlink = null;
        }

        sr.enabled = true;
        sr.color = originalColor;
    }

    // --- 코루틴들 ---

    private IEnumerator BlinkRoutine(float duration, float interval)
    {
        float timer = 0f;
        while (timer < duration)
        {
            sr.enabled = false;
            yield return new WaitForSeconds(interval);
            timer += interval;

            sr.enabled = true;
            yield return new WaitForSeconds(interval);
            timer += interval;
        }
        sr.enabled = true;
        currentBlink = null;
    }

    private IEnumerator ColorBlinkRoutine(Color color, float duration, float interval)
    {
        sr.color = color;
        float timer = 0f;
        while (timer < duration)
        {
            sr.enabled = false;
            yield return new WaitForSeconds(interval);
            timer += interval;

            sr.enabled = true;
            yield return new WaitForSeconds(interval);
            timer += interval;
        }
        sr.enabled = true;
        sr.color = originalColor;
        currentBlink = null;
    }

    private IEnumerator ConvertColorRoutine(Color color, float duration)
    {
        sr.color = color;
        yield return new WaitForSeconds(duration);
        sr.color = originalColor;
        currentBlink = null;
    }
}