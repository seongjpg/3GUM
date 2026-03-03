using System.Collections;
using UnityEngine;

public class VisualEffectHandler : MonoBehaviour
{
    private SpriteRenderer sr;
    private Coroutine currentEffect;
    private Color originalColor;

    void Awake()
    {
        // 자식의 SpriteRenderer를 찾음 (이미지가 자식에 있으므로)
        sr = GetComponentInChildren<SpriteRenderer>();
        originalColor = sr.color;
    }

    // 기본 깜빡임 (피격용)
    public void Blink(float duration, float interval = 0.1f)
    {
        StopCurrentEffect();
        currentEffect = StartCoroutine(BlinkRoutine(duration, interval));
    }

    // 색상 변경 + 깜빡임 (디버프 등)
    public void ColorBlink(Color color, float duration, float interval = 0.15f)
    {
        StopCurrentEffect();
        currentEffect = StartCoroutine(ColorBlinkRoutine(color, duration, interval));
    }

    // 색상만 일시 변경 (사망 연출 등)
    public void FlashColor(Color color, float duration)
    {
        StopCurrentEffect();
        currentEffect = StartCoroutine(FlashColorRoutine(color, duration));
    }

    // 진행 중인 효과 즉시 중단 + 원상복구
    public void StopCurrentEffect()
    {
        if (currentEffect != null)
        {
            StopCoroutine(currentEffect);
            currentEffect = null;
        }
        sr.enabled = true;
        sr.color = originalColor;
    }

    // --- 코루틴 ---

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
        currentEffect = null;
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
        currentEffect = null;
    }

    private IEnumerator FlashColorRoutine(Color color, float duration)
    {
        sr.color = color;
        yield return new WaitForSeconds(duration);
        sr.color = originalColor;
        currentEffect = null;
    }
}