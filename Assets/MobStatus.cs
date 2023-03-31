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
        if (_state == StateEnum.Die) //���łɎ��S���Ă�ꍇ
        {
            return;
        }

        _life -= damage; //�_���[�W�Ԃ񃉃C�t������

        if (_life > 0) //��_���[�W�̌��ʐ����Ă����ꍇ
        {
            return;
        }

        _state = StateEnum.Die; //���S�����ꍇ
        _animator.SetTrigger("Die"); //���S�A�j���[�V����
        OnDie(); //���S���̏���
    }

    protected virtual void OnDie() 
    { 
        //����
    }

    //�U����ԂɈڍs
    public void GoToAttackStateIfPossible()
    {
        //�U���ł��Ȃ����(_state��Normal�łȂ����)�߂�
        if(!IsAttackable)
        {
            return;
        }

        _state = StateEnum.Attack;
        _animator.SetTrigger("Attack");
    }

    //�ʏ��ԂɈڍs
    public void GoToNormalStateIfPossible()
    {
        //����ł��(_state��Die�ł����)�߂�
        if (_state == StateEnum.Die)
        {
            return;
        }

        _state = StateEnum.Normal;
    }
}
