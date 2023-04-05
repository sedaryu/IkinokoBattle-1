using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyStatus : MobStatus
{
    NavMeshAgent _agent;

    protected override void Start()
    {
        base.Start(); //基底クラスのStart関数も実行
        _agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        //移動速度によってアニメーションが遷移
        _animator.SetFloat("MoveSpeed", _agent.velocity.magnitude);
    }

    protected override void OnDie()
    {
        base.OnDie(); //基底クラスのOnDie関数も実行
        StartCoroutine(DestroyCroutine());

    }

    IEnumerator DestroyCroutine()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(this.gameObject);
    }
}
