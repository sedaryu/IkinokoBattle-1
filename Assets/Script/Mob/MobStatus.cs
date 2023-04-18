using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Unityエディタ時のみusingする必要がある
#if UNITY_EDITOR
using UnityEditor;
#endif

public abstract class MobStatus : MonoBehaviour
{
    // Unityエディタ時のみGizmo表示を行う
    #if UNITY_EDITOR

    void OnDrawGizmos()
    {
        // 表示色を指定
        GUI.color = Color.black;

        // オブジェクトの位置に「オブジェクト名」をラベル表示
        Handles.Label(transform.position, $"{name}\n{Life}\n{_state.ToString()}");
    }
    #endif

    protected enum StateEnum //キャラクターの状態
    { 
        Normal, //通常時(AttackやDieに移行可能)
        Attack, //攻撃時(時間経過でNormalに移行)
        Die, //死亡時(どの状態にも移行しない)
        Jump, //ジャンプ時
        Shoot //射撃時
    }

    protected StateEnum _state = StateEnum.Normal; //初期値はNormal

    public bool IsMovable => (_state == StateEnum.Normal); //状態がNormalであればtrueを返す
    public bool IsAttackable => (_state == StateEnum.Normal); //状態がNormalであればtrueを返す

    float _life;
    public float Life => _life; //HP

    [SerializeField]
    float _lifeMax;
    public float LifeMax => _lifeMax; //最大HP

    protected Animator _animator;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _life = _lifeMax; //Hpを満タン状態に設定
        LifeGaugeContainer.Instance.Add(this); //ライフゲージUIを生成し表示させる

        _animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(float damage)
    {
        if (_state == StateEnum.Die) //すでに死亡してる場合
        {
            return;
        }

        _life -= damage; //ダメージぶんライフが減る

        if (Life > 0) //被ダメージの結果生きていた場合
        {
            return;
        }

        _state = StateEnum.Die; //死亡した場合
        _animator.SetTrigger("Die"); //死亡アニメーション
        OnDie(); //死亡時の処理
    }

    public void Knockback(Vector3 knockbackVector) //攻撃を受けた際のノックバック処理
    {
        this.transform.Translate(knockbackVector, Space.World);
    }

    protected virtual void OnDie() 
    { 
        LifeGaugeContainer.Instance.Remove(this);
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

        Debug.Log("Normal");
    }
}
