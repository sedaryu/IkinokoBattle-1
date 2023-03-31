using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MobStatus))]
public class MobAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown = 0.5f;
    [SerializeField] private Collider attackCollider; //AttackHitDetectorオブジェクトにアタッチされたColider

    private MobStatus _status;

    void Start()
    {
        _status = GetComponent<MobStatus>();
    }

    //攻撃関数(OnOnAttackRangeEnterから呼ばれる)
    public void AttackIfPossible()
    {
        if (!_status.IsAttackable) return;

        _status.GoToAttackStateIfPossible(); //MobStatusの_stateをAttackに変更
    }

    //攻撃範囲内に攻撃対象が入ったときに呼ばれるイベントハンドラー(AttackRangeDetectorのCollisionDetectorスクリプトから呼ばれる)
    public void OnAttackRangeEnter(Collider collider)
    { 
        AttackIfPossible();
    }

    //攻撃開始時(Animatorから呼ばれる)
    public void OnAttackStart()
    { 
        attackCollider.enabled = true; //攻撃のコリダーを起動
    }

    //attackColliderが攻撃対象に当たった際実行(AttackHitDetectorのCollisionDetectorスクリプトから呼ばれる)
    public void OnHitAttack(Collider collider)
    { 
        MobStatus targetMob = collider.GetComponent<MobStatus>();
        if (targetMob != null) return;

        targetMob.Damage(1);
    }

    //攻撃終了時(Animatorから呼ばれる)
    public void OnAttackFinished()
    { 
        attackCollider.enabled = false;
        StartCoroutine(CoolDownCoroutine());
    }

    IEnumerator CoolDownCoroutine()
    {
        yield return new WaitForSeconds(attackCooldown);
        _status.GoToNormalStateIfPossible(); //MobStatusの_stateをNormalに変更
    }
}
