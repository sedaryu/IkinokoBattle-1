using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerStatus))]
[RequireComponent(typeof(MobAttack))]
public class PlayerController : MonoBehaviour
{
    private float turnSpeed; //��]�X�s�[�h
    [SerializeField] private float groundTurnSpeed = 90;
    [SerializeField] private float jumpTurnSpeed = 180;
    [SerializeField] private float moveSpeed = 6;
    [SerializeField] private float stepSpeed = 5.5f;
    [SerializeField] private float jumpPower = 3.5f;

    [SerializeField] private Animator animator;

    private CharacterController characterController;
    private Transform _transform;
    private float turn;
    private Vector3 moveVelocity;
    private PlayerStatus _status;
    private MobAttack _attack;
    private PlayerShoot _shoot;

    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        _transform = this.transform;
        _status = this.GetComponent<PlayerStatus>();
        _attack = this.GetComponent<MobAttack>();
        _shoot = this.GetComponent<PlayerShoot>();
    }

    // Update is called once per frame
    void Update()
    {
        JudgeGrounded();

        //�ˌ�
        if (_status.IsMovable)
        {
            if (Input.GetButtonDown("Fire2"))
            {
                _shoot.ShootIfPossible(); //�ˌ���Ԃֈڍs
            }
        }
        else if (_status.IsShootable)
        {
            //���˂���
            if (Input.GetButton("LeftArrow"))
            {
                _shoot.Shooting();
            }
            //������ύX����
            if (Input.GetButtonDown("RightArrow"))
            {
                _shoot.ChangeWeapon();
            }
            //�ʏ��Ԃ֖߂�
            if (Input.GetButtonUp("Fire2"))
            {
                _shoot.CancelShoot(); 
            }
        }

        //�O��ړ�
        if (_status.IsMovable || _status.IsStepable) //�ړ��\�ȏ�Ԃ��ǂ�������
        {
            moveVelocity.z = Input.GetAxis("Vertical") * moveSpeed; //�L�[���͂��󂯈ړ�����
        }
        else //�ړ����̂��̂��s�\�ȏ�Ԃł������ꍇ
        {
            moveVelocity.z = 0;
        }
        
        //�W�����v
        if (_status.IsMovable) //�n��ɂ���ꍇ
        {
            if (Input.GetButtonDown("UpArrow"))
            {
                _status.GoToJumpStateIfPossible(); //�W�����v��Ԃֈڍs
                moveVelocity.y = jumpPower;
            }
        }
        else if (_status.IsStepable) //�󒆂ɂ���ꍇ
        {
            //�ߐڍU��
            if (Input.GetButtonDown("DownArrow"))
            {
                Debug.Log("Attack");
            }
            //���n
            if (isGrounded)
            {
                _status.GoToNormalStateIfPossible(); //�ʏ��Ԃ֖߂�
            }
        }

        //�d��
        if (!isGrounded)
        {
            moveVelocity.y += Physics.gravity.y * Time.deltaTime; //�d�͂ɂ�闎���̉e�����󂯂�
        }

        //�����]��
        if (_status.IsMovable) //�n��ɂ���ꍇ
        {
            turn = Input.GetAxis("Horizontal") * groundTurnSpeed;
        }
        else if (_status.IsStepable) //�󒆂ɂ���ꍇ
        {
            turn = Input.GetAxis("Horizontal") * jumpTurnSpeed;
        }
        else if (_status.IsShootable) //�ˌ���Ԃ̏ꍇ
        {
            turn = Input.GetAxis("Horizontal") * groundTurnSpeed;
            Vector3 aim = new Vector3(Input.GetAxis("Vertical") * groundTurnSpeed, turn, 0);
            _shoot.Aiming(aim); //���f
        }
        transform.Rotate(0, turn * Time.deltaTime, 0); //��]�𔽉f

        //�󒆃X�e�b�v
        if (_status.IsStepable)
        {
            if (Input.GetButton("UpArrow"))
            {
                if (Input.GetButton("LeftArrow"))
                {
                    moveVelocity.x = -stepSpeed;
                }
                else if (Input.GetButton("RightArrow"))
                {
                    moveVelocity.x = stepSpeed;
                }
                //if (Math.Abs(jumpStartMousePosition - Input.mousePosition.x) > Screen.width * 0.1f)
                //{
                //    if (Input.mousePosition.x < Screen.width * 0.5f)
                //    {
                //        moveVelocity.x = -stepSpeed;
                //    }
                //    else if (Screen.width * 0.5f < Input.mousePosition.x)
                //    {
                //        moveVelocity.x = stepSpeed;
                //    }
                //}
            }
        }
        else
        {
            moveVelocity.x = 0;
        }

        //�v���C���[�̌����Ă�������ɂ��킹�đO��ړ�
        characterController.Move(new Vector3(moveVelocity.z * transform.forward.x, 0, moveVelocity.z * transform.forward.z) * Time.deltaTime);
        //�v���C���[�̌����Ă�������ɂ��킹�č��E�E����ړ�
        characterController.Move(new Vector3(moveVelocity.x * _transform.right.x, moveVelocity.y, moveVelocity.x * _transform.right.z) * Time.deltaTime);

        //�A�j���[�^�[�ɒl����
        animator.SetFloat("MoveSpeed", new Vector3(0, 0, moveVelocity.z).magnitude);
    }

    private void JudgeGrounded() //�ڒn���菈�����s��
    {
        Ray ray = new Ray(this.transform.position, Vector3.down);
        Debug.DrawRay(this.transform.position, Vector3.down * 0.1f, Color.red);

        if (Physics.Raycast(ray, 0.1f, 1 << 10))
        {
            isGrounded = true;
        }
        else
        { 
            isGrounded = false;
        }
    }
}
