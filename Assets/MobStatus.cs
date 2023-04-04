using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MobStatus : MonoBehaviour
{
    protected enum StateEnum //キャラクターの状態
    { 
        Normal, //通常時(AttackやDieに移行可能)
        Attack, //攻撃時(時間経過でNormalに移行)
        Die //死亡時(どの状態にも移行しない)
    }

    protected StateEnum _state = StateEnum.Normal; //初期値はNormal

    public bool IsMovable => (_state == StateEnum.Normal); //状態がNormalであればtrueを返す
    public bool IsAttackable => (_state == StateEnum.Normal); //状態がNormalであればtrueを返す

    float _life;
    public float Life => _life; //HP

    [SerializeField]
    float _lifeMax;
    public float LifeMax => _lifeMax; //最大HP

    [SerializeField]
    protected Animator _animator;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _life = _lifeMax; //Hpを満タン状態に設定
        //_animator = this.GetComponent<Animator>();
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

        Debug.Log(Life + $"{this.gameObject.name}");

        if (Life > 0) //被ダメージの結果生きていた場合
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
        //Attackに移行できない状態であれば(状態がNormalでなければ)戻る
        if(!IsAttackable)
        {
            return;
        }

        _state = StateEnum.Attack; //状態をAttackへ移行
        _animator.SetTrigger("Attack"); //攻撃アニメーションを開始する
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
