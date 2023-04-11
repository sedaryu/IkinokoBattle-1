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

    private float startMousePosition;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        _transform = this.transform;
        _status = this.GetComponent<PlayerStatus>();
        _attack = this.GetComponent<MobAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2")) //�U��
        { 
            _attack.AttackIfPossible();
        }

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
        

        if (_status.IsJumpable) //�n��ɂ���ꍇ
        {
            if (Input.GetButton("Jump"))
            {
                _status.GoToJumpStateIfPossible(); //�W�����v��Ԃֈڍs
                moveVelocity.y = jumpPower;
            }
        }
        else //�󒆂ɂ���ꍇ
        {
            if (!characterController.isGrounded)
            {
                moveVelocity.y += Physics.gravity.y * Time.deltaTime; //�d�͂ɂ�闎���̉e�����󂯂�
                if (Input.GetButton("Jump"))
                {
                    if (Input.mousePosition.x < Screen.width * 0.5f)
                    { Debug.Log("Left"); }
                    else if (Screen.width * 0.5f < Input.mousePosition.x)
                    { Debug.Log("Right"); }
                }
            }
            else
            {
                _status.GoToNormalStateIfPossible(); //�ʏ��Ԃ֖߂�
            }
        }

        //�v���C���[�������]�����s��
        turn = Input.GetAxis("Horizontal") * groundTurnSpeed;
        transform.Rotate(0, turn * Time.deltaTime, 0);
        //�v���C���[�̌����Ă�������ɂ��킹�Ĉړ�
        characterController.Move(new Vector3(moveVelocity.z * transform.forward.x, moveVelocity.y, moveVelocity.z * transform.forward.z) * Time.deltaTime);
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
