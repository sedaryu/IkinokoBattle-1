using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MobStatus))]
public class MobAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown = 0.5f; //�U����̃N�[���_�E������
    [SerializeField] private Collider attackCollider; //AttackHitDetector�I�u�W�F�N�g�ɃA�^�b�`���ꂽColider

    private MobStatus _status;

    void Start()
    {
        _status = GetComponent<MobStatus>();
    }

    //�U�����\�b�h
    public void AttackIfPossible()
    {
        if (!_status.IsAttackable) return;

        _status.GoToAttackStateIfPossible(); //MobStatus��_state��Attack�ɕύX
    }

    //�U���͈͓��ɍU���Ώۂ��������Ƃ��ɌĂ΂��C�x���g�n���h���[(AttackRangeDetector��CollisionDetector�X�N���v�g����Ă΂��)
    public void OnAttackRangeEnter(Collider collider)
    { 
        AttackIfPossible();
    }

    //�U���J�n��(Animator����Ă΂��)
    public void OnAttackStart()
    { 
        attackCollider.enabled = true; //�U���̃R���_�[���N��
        Debug.Log("Attack!");
    }

    //attackCollider���U���Ώۂɓ��������ێ��s(AttackHitDetector��CollisionDetector�X�N���v�g����Ă΂��)
    public void OnHitAttack(Collider collider)
    { 
        MobStatus targetMob = collider.GetComponent<MobStatus>();
        Debug.Log("Hit");
        if (targetMob == null) return;

        targetMob.Damage(1);
        Debug.Log("Damage");
    }

    //�U���I����(Animator����Ă΂��)
    public void OnAttackFinished()
    { 
        attackCollider.enabled = false;
        StartCoroutine(CoolDownCoroutine());
    }

    IEnumerator CoolDownCoroutine()
    {
        yield return new WaitForSeconds(attackCooldown);
        _status.GoToNormalStateIfPossible(); //MobStatus��_state��Normal�ɕύX
    }
}
