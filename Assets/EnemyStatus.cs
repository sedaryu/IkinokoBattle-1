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
        base.Start(); //���N���X��Start�֐������s
        _agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        //�ړ����x�ɂ���ăA�j���[�V�������J��
        _animator.SetFloat("MoveSpeed", _agent.velocity.magnitude);
    }

    protected override void OnDie()
    {
        base.OnDie(); //���N���X��OnDie�֐������s
        StartCoroutine(DestroyCroutine());

    }

    IEnumerator DestroyCroutine()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(this.gameObject);
    }
}
