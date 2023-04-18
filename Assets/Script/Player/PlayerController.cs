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
    private float turnSpeed; //回転スピード
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

        //射撃
        if (_status.IsMovable)
        {
            if (Input.GetButtonDown("Fire2"))
            {
                _shoot.ShootIfPossible(); //射撃状態へ移行
            }
        }
        else if (_status.IsShootable)
        {
            //発射する
            if (Input.GetButton("LeftArrow"))
            {
                _shoot.Shooting();
            }
            //武装を変更する
            if (Input.GetButtonDown("RightArrow"))
            {
                _shoot.ChangeWeapon();
            }
            //通常状態へ戻る
            if (Input.GetButtonUp("Fire2"))
            {
                _shoot.CancelShoot(); 
            }
        }

        //前後移動
        if (_status.IsMovable || _status.IsStepable) //移動可能な状態かどうか判別
        {
            moveVelocity.z = Input.GetAxis("Vertical") * moveSpeed; //キー入力を受け移動する
        }
        else //移動そのものが不可能な状態であった場合
        {
            moveVelocity.z = 0;
        }
        
        //ジャンプ
        if (_status.IsMovable) //地上にいる場合
        {
            if (Input.GetButtonDown("UpArrow"))
            {
                _status.GoToJumpStateIfPossible(); //ジャンプ状態へ移行
                moveVelocity.y = jumpPower;
            }
        }
        else if (_status.IsStepable) //空中にいる場合
        {
            //近接攻撃
            if (Input.GetButtonDown("DownArrow"))
            {
                Debug.Log("Attack");
            }
            //着地
            if (isGrounded)
            {
                _status.GoToNormalStateIfPossible(); //通常状態へ戻る
            }
        }

        //重力
        if (!isGrounded)
        {
            moveVelocity.y += Physics.gravity.y * Time.deltaTime; //重力による落下の影響を受ける
        }

        //方向転換
        if (_status.IsMovable) //地上にいる場合
        {
            turn = Input.GetAxis("Horizontal") * groundTurnSpeed;
        }
        else if (_status.IsStepable) //空中にいる場合
        {
            turn = Input.GetAxis("Horizontal") * jumpTurnSpeed;
        }
        else if (_status.IsShootable) //射撃状態の場合
        {
            turn = Input.GetAxis("Horizontal") * groundTurnSpeed;
            Vector3 aim = new Vector3(Input.GetAxis("Vertical") * groundTurnSpeed, turn, 0);
            _shoot.Aiming(aim); //反映
        }
        transform.Rotate(0, turn * Time.deltaTime, 0); //回転を反映

        //空中ステップ
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

        //プレイヤーの向いている方向にあわせて前後移動
        characterController.Move(new Vector3(moveVelocity.z * transform.forward.x, 0, moveVelocity.z * transform.forward.z) * Time.deltaTime);
        //プレイヤーの向いている方向にあわせて左右・高低移動
        characterController.Move(new Vector3(moveVelocity.x * _transform.right.x, moveVelocity.y, moveVelocity.x * _transform.right.z) * Time.deltaTime);

        //アニメーターに値を代入
        animator.SetFloat("MoveSpeed", new Vector3(0, 0, moveVelocity.z).magnitude);
    }

    private void JudgeGrounded() //接地判定処理を行う
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
