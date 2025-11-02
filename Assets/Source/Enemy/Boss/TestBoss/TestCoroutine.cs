using UnityEngine;
using System.Collections;
public enum BossState { Idle, Dash, Shoot, Dead }

public class TestCoroutine : MonoBehaviour
{
    [SerializeField] GameObject NextGate;
    [SerializeField] BossManager bm;
    BossState state = BossState.Idle;
    bool isDeadHandled = false;

    WaitForSeconds fiveSec = new WaitForSeconds(5f);

    void Start()
    {
        StartCoroutine(StateLoop());
    }

    IEnumerator StateLoop()
    {
        while (true)
        {
            if (bm.curHealth <= 0 && state != BossState.Dead)
            {
                state = BossState.Dead;
            }

            switch (state)
            {
                case BossState.Idle:
                    // 기본 이동 처리
                    Debug.Log("Idle state");
                    yield return fiveSec;
                    // 랜덤으로 상태 결정
                    state = (Random.value < 0.5f) ? BossState.Dash : BossState.Shoot;
                    break;

                case BossState.Dash:
                    yield return StartCoroutine(DoDash());
                    state = BossState.Idle;
                    break;

                case BossState.Shoot:
                    yield return StartCoroutine(DoShoot());
                    state = BossState.Idle;
                    break;

                case BossState.Dead:
                    if (!isDeadHandled)
                    {
                        isDeadHandled = true;
                        yield return StartCoroutine(DoDie());
                        // 보스 비활성화 등
                        //그러고 보스 사망 자리에서 게이트 설치
                        GameObject.Instantiate(NextGate, this.transform.position, Quaternion.identity);
                        Destroy(this.gameObject);
                    }
                    yield break;
            }

            yield return null;
        }
    }

    IEnumerator DoDash()
    {
        Debug.Log("DoDash");
        // 대시 동작, 시간 기반 처리
        float t = 0f;
        float duration = 1f;
        while (t < duration)
        {
            t += Time.deltaTime;
            // 이동 처리
            yield return null;
        }
    }

    IEnumerator DoShoot()
    {
        Debug.Log("DoShoot");
        // 사격 동작
        // yield return null 또는 시간 대기
        yield return null;
    }

    IEnumerator DoDie()
    {
        // 사망 처리
        Debug.Log("Die");
        yield return null;
    }
}