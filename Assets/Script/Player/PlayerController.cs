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

    //private Vector2 shootStartMousePosition;
    //private float jumpStartMousePosition;

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
            if (Input.GetButtonDown("LeftStep"))
            {
                _shoot.Shooting();
            }

            if (Input.GetButtonUp("Fire2"))
            {
                _shoot.CancelShoot(); //�ʏ��Ԃ֖߂�
            }
        }

        //�O��ړ�
        if (_status.IsMovable) //�n��ړ��\�ȏ�Ԃ��ǂ�������
        {
            moveVelocity.z = Input.GetAxis("Vertical") * moveSpeed; //�L�[���͂��󂯈ړ�����
        }
        else if (_status.IsStepable) //�󒆈ړ��\�ȏ�Ԃ��ǂ�������
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
            if (Input.GetButtonDown("Jump"))
            {
                _status.GoToJumpStateIfPossible(); //�W�����v��Ԃֈڍs
                moveVelocity.y = jumpPower;
                //jumpStartMousePosition = Input.mousePosition.x; //�W�����v���̃}�E�X�ʒu���L�^
            }
        }
        else if (_status.IsStepable) //�󒆂ɂ���ꍇ
        {
            if (!characterController.isGrounded)
            {
                moveVelocity.y += Physics.gravity.y * Time.deltaTime; //�d�͂ɂ�闎���̉e�����󂯂�
            }
            else
            {
                _status.GoToNormalStateIfPossible(); //�ʏ��Ԃ֖߂�
                //jumpStartMousePosition = Screen.width * 0.5f;
            }
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
            if (Input.GetButton("Jump"))
            {
                if (Input.GetButton("LeftStep"))
                {
                    moveVelocity.x = -stepSpeed;
                }
                else if (Input.GetButton("RightStep"))
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

        ////�ړ�����
        //if (characterController.isGrounded) //�n��ɂ���ꍇ
        //{
        //    turnSpeed = groundTurnSpeed; //�n��ɂ���ꍇ�A�󒆂ɂ�����^�[������X�s�[�h���x��
        //    if (Input.GetMouseButtonDown(0)) //�w�肵���{�^�������͂����ƃW�����v
        //    { 
        //        moveVelocity.y = jumpPower;
        //        startMousePosition = Input.mousePosition.x; //�󒆈ړ�����̂��߁A�W�����v�����u�Ԃ̃}�E�X�ʒu���L�^����
        //    }
        //}
        //else
        //{
        //    turnSpeed = jumpTurnSpeed; //�󒆂ɂ���ꍇ�A�n��ɂ�����^�[������X�s�[�h������
        //    moveVelocity.y += Physics.gravity.y * Time.deltaTime; //�d�͂̉e�����󂯂�

        //    if (Input.GetMouseButton(0)) //�w�肵���{�^�������������Ă����ꍇ�A�󒆈ړ����\
        //    {
        //        if (Input.mousePosition.x < startMousePosition) //�W�����v�����u�Ԃ̃}�E�X�ʒu��荶�Ƀ}�E�X�𓮂����΁A���ֈړ�
        //        {
        //            moveVelocity.x = -stepSpeed;
        //        }
        //        else if (startMousePosition < Input.mousePosition.x) //�W�����v�����u�Ԃ̃}�E�X�ʒu���E�Ƀ}�E�X�𓮂����΁A�E�ֈړ�
        //        {
        //            moveVelocity.x = stepSpeed;
        //        }

        //        //�v���C���[�̕����ɍ��킹�A�ړ��x�N�g���𒲐�
        //        Vector3 step = new Vector3(moveVelocity.x * _transform.right.x, moveVelocity.y, moveVelocity.x * _transform.right.z);
        //        characterController.Move(step * Time.deltaTime); //�ړ������s
        //    }
        //}

        //turn = Input.GetAxis("Horizontal") * turnSpeed;
        //_transform.Rotate(0, turn * Time.deltaTime, 0);

        //moveVelocity.z = Input.GetAxis("Vertical") * moveSpeed;

        //Vector3 move = new Vector3(moveVelocity.z * _transform.forward.x, moveVelocity.y, moveVelocity.z * _transform.forward.z);

        //characterController.Move(move * Time.deltaTime);

        //animator.SetFloat("MoveSpeed", new Vector3(0, 0, moveVelocity.z).magnitude);
    }
}
