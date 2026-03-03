using UnityEngine;
using System.Collections;

public class BossCoroutine : MonoBehaviour
{
    [SerializeField] GameObject nextGate;
    [SerializeField] float idleMinTime = 2f;
    [SerializeField] float idleMaxTime = 5f;

    private MobManager mm;
    private AnimationHandler animHandler;
    private VisualEffectHandler vfx;
    private Transform playerTarget;
    private System.Func<IEnumerator>[] patterns;

    void Awake()
    {
        mm = GetComponent<MobManager>();
        animHandler = GetComponent<AnimationHandler>();
        vfx = GetComponent<VisualEffectHandler>();
    }

    void Start()
    {
        playerTarget = GameObject.FindWithTag("Player").transform;
        patterns = new System.Func<IEnumerator>[]
        {
            DoDash,
            DoShoot,
        };
        StartCoroutine(StateLoop());
    }

    private bool CheckDead()
    {
        return mm.curHealth <= 0;
    }

    IEnumerator StateLoop()
    {
        while (true)
        {
            if (CheckDead())
            {
                yield return StartCoroutine(DoDie());
                Instantiate(nextGate, transform.position, Quaternion.identity);
                Destroy(gameObject);
                yield break;
            }

            float waitTime = Random.Range(idleMinTime, idleMaxTime);
            yield return new WaitForSeconds(waitTime);

            if (CheckDead()) continue;

            int index = Random.Range(0, patterns.Length);
            yield return StartCoroutine(patterns[index]());

            yield return null;
        }
    }

    IEnumerator DoDash()
    {
        // 플레이어 방향 계산
        Vector2 dir = (playerTarget.position - transform.position).normalized;

        animHandler.PlayTrigger("DashReady");
        yield return new WaitForSeconds(0.5f);

        animHandler.SetBool("isDashing", true);
        float t = 0f;
        float duration = 1f;
        while (t < duration)
        {
            if (CheckDead()) yield break;
            transform.position += (Vector3)(dir * mm.speed * Time.deltaTime);
            t += Time.deltaTime;
            yield return null;
        }
        animHandler.SetBool("isDashing", false);
    }

    IEnumerator DoShoot()
    {
        animHandler.PlayTrigger("ShootReady");
        if (CheckDead()) yield break;

        // 투사체 발사
        Instantiate(mm.projectTile[0], transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator DoDie()
    {
        animHandler.PlayTrigger("Die");
        vfx.FlashColor(Color.red, 1f);
        yield return new WaitForSeconds(1f);
    }
}