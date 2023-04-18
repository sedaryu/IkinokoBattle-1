using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Unity�G�f�B�^���̂�using����K�v������
#if UNITY_EDITOR
using UnityEditor;
#endif

public abstract class MobStatus : MonoBehaviour
{
    // Unity�G�f�B�^���̂�Gizmo�\�����s��
    #if UNITY_EDITOR

    void OnDrawGizmos()
    {
        // �\���F���w��
        GUI.color = Color.black;

        // �I�u�W�F�N�g�̈ʒu�Ɂu�I�u�W�F�N�g���v�����x���\��
        Handles.Label(transform.position, $"{name}\n{Life}\n{_state.ToString()}");
    }
    #endif

    protected enum StateEnum //�L�����N�^�[�̏��
    { 
        Normal, //�ʏ펞(Attack��Die�Ɉڍs�\)
        Attack, //�U����(���Ԍo�߂�Normal�Ɉڍs)
        Die, //���S��(�ǂ̏�Ԃɂ��ڍs���Ȃ�)
        Jump, //�W�����v��
        Shoot //�ˌ���
    }

    protected StateEnum _state = StateEnum.Normal; //�����l��Normal

    public bool IsMovable => (_state == StateEnum.Normal); //��Ԃ�Normal�ł����true��Ԃ�
    public bool IsAttackable => (_state == StateEnum.Normal); //��Ԃ�Normal�ł����true��Ԃ�

    float _life;
    public float Life => _life; //HP

    [SerializeField]
    float _lifeMax;
    public float LifeMax => _lifeMax; //�ő�HP

    protected Animator _animator;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _life = _lifeMax; //Hp�𖞃^����Ԃɐݒ�
        LifeGaugeContainer.Instance.Add(this); //���C�t�Q�[�WUI�𐶐����\��������

        _animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(float damage)
    {
        if (_state == StateEnum.Die) //���łɎ��S���Ă�ꍇ
        {
            return;
        }

        _life -= damage; //�_���[�W�Ԃ񃉃C�t������

        if (Life > 0) //��_���[�W�̌��ʐ����Ă����ꍇ
        {
            return;
        }

        _state = StateEnum.Die; //���S�����ꍇ
        _animator.SetTrigger("Die"); //���S�A�j���[�V����
        OnDie(); //���S���̏���
    }

    public void Knockback(Vector3 knockbackVector) //�U�����󂯂��ۂ̃m�b�N�o�b�N����
    {
        this.transform.Translate(knockbackVector, Space.World);
    }

    protected virtual void OnDie() 
    { 
        LifeGaugeContainer.Instance.Remove(this);
    }

    //�U����ԂɈڍs
    public void GoToAttackStateIfPossible()
    {
        //Attack�Ɉڍs�ł��Ȃ���Ԃł����(��Ԃ�Normal�łȂ����)�߂�
        if(!IsAttackable)
        {
            return;
        }

        _state = StateEnum.Attack; //��Ԃ�Attack�ֈڍs
        _animator.SetTrigger("Attack"); //�U���A�j���[�V�������J�n����
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

        Debug.Log("Normal");
    }
}
