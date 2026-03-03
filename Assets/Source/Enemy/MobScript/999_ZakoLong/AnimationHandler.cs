using System.Collections;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private Animator anim;
    private Coroutine currentAnimRoutine;

    void Awake()
    {
        // 자식에 Animator가 있는 경우 대비
        anim = GetComponentInChildren<Animator>();
    }

    // 트리거 발동 (피격, 공격 준비 등 1회성 애니메이션)
    public void PlayTrigger(string triggerName)
    {
        anim.SetTrigger(triggerName);
    }

    // Bool 파라미터 설정 (이동, 대시 등 지속 애니메이션)
    public void SetBool(string paramName, bool value)
    {
        anim.SetBool(paramName, value);
    }

    // 일정 시간 후 Bool 자동 복구 (피격 모션 후 Idle 복귀 등)
    public void SetBoolTimed(string paramName, bool value, float duration)
    {
        if (currentAnimRoutine != null)
            StopCoroutine(currentAnimRoutine);

        currentAnimRoutine = StartCoroutine(BoolTimedRoutine(paramName, value, duration));
    }

    // Float 파라미터 설정 (블렌드 트리 등)
    public void SetFloat(string paramName, float value)
    {
        anim.SetFloat(paramName, value);
    }

    // 애니메이션 속도 조절 (슬로우 연출, 일시정지 등)
    public void SetSpeed(float speed)
    {
        anim.speed = speed;
    }

    // 특정 애니메이션 강제 재생
    public void PlayState(string stateName, int layer = 0)
    {
        anim.Play(stateName, layer);
    }

    // 현재 애니메이션이 끝날 때까지 대기하는 코루틴
    public IEnumerator WaitForAnimation(string stateName, int layer = 0)
    {
        // 상태 진입 대기
        yield return null;

        // 해당 애니메이션 재생이 끝날 때까지 대기
        while (anim.GetCurrentAnimatorStateInfo(layer).IsName(stateName)
            && anim.GetCurrentAnimatorStateInfo(layer).normalizedTime < 1f)
        {
            yield return null;
        }
    }

    // --- 코루틴 ---

    private IEnumerator BoolTimedRoutine(string paramName, bool value, float duration)
    {
        anim.SetBool(paramName, value);
        yield return new WaitForSeconds(duration);
        anim.SetBool(paramName, !value);
        currentAnimRoutine = null;
    }
}