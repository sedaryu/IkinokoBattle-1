using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MobStatus : MonoBehaviour
{
    protected enum StateEnum
    { 
        Normal,
        Attack,
        Die
    }

    protected StateEnum _state = StateEnum.Normal;

    public bool IsMovable => (_state == StateEnum.Normal);
    public bool IsAttackable => (_state == StateEnum.Normal);

    float _life;
    public float Life => _life;

    [SerializeField]
    float _lifeMax;
    public float LifeMax => _lifeMax;

    protected Animator _animator;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _life = _lifeMax;
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(int damage)
    {
        if (_state == StateEnum.Die) //すでに死亡してる場合
        {
            return;
        }

        _life -= damage; //ダメージぶんライフが減る

        if (_life > 0) //被ダメージの結果生きていた場合
        {
            return;
        }

        _state = StateEnum.Die; //死亡した場合
        _animator.SetTrigger("Die"); //死亡アニメーション
        OnDie(); //死亡時の処理
    }

    protected virtual void OnDie() 
    { 
        //処理
    }

    //攻撃状態に移行
    public void GoToAttackStateIfPossible()
    {
        //攻撃できなければ(_stateがNormalでなければ)戻る
        if(!IsAttackable)
        {
            return;
        }

        _state = StateEnum.Attack;
        _animator.SetTrigger("Attack");
    }

    //通常状態に移行
    public void GoToNormalStateIfPossible()
    {
        //死んでれば(_stateがDieであれば)戻る
        if (_state == StateEnum.Die)
        {
            return;
        }

        _state = StateEnum.Normal;
    }
}
