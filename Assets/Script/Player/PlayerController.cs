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
            if (Input.GetButtonDown("LeftStep"))
            {
                _shoot.Shooting();
            }

            if (Input.GetButtonUp("Fire2"))
            {
                _shoot.CancelShoot(); //通常状態へ戻る
            }
        }

        //前後移動
        if (_status.IsMovable) //地上移動可能な状態かどうか判別
        {
            moveVelocity.z = Input.GetAxis("Vertical") * moveSpeed; //キー入力を受け移動する
        }
        else if (_status.IsStepable) //空中移動可能な状態かどうか判別
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
            if (Input.GetButtonDown("Jump"))
            {
                _status.GoToJumpStateIfPossible(); //ジャンプ状態へ移行
                moveVelocity.y = jumpPower;
                //jumpStartMousePosition = Input.mousePosition.x; //ジャンプ時のマウス位置を記録
            }
        }
        else if (_status.IsStepable) //空中にいる場合
        {
            if (!characterController.isGrounded)
            {
                moveVelocity.y += Physics.gravity.y * Time.deltaTime; //重力による落下の影響を受ける
            }
            else
            {
                _status.GoToNormalStateIfPossible(); //通常状態へ戻る
                //jumpStartMousePosition = Screen.width * 0.5f;
            }
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

        //プレイヤーの向いている方向にあわせて前後移動
        characterController.Move(new Vector3(moveVelocity.z * transform.forward.x, 0, moveVelocity.z * transform.forward.z) * Time.deltaTime);
        //プレイヤーの向いている方向にあわせて左右・高低移動
        characterController.Move(new Vector3(moveVelocity.x * _transform.right.x, moveVelocity.y, moveVelocity.x * _transform.right.z) * Time.deltaTime);

        //アニメーターに値を代入
        animator.SetFloat("MoveSpeed", new Vector3(0, 0, moveVelocity.z).magnitude);

        ////移動処理
        //if (characterController.isGrounded) //地上にいる場合
        //{
        //    turnSpeed = groundTurnSpeed; //地上にいる場合、空中にいるよりターンするスピードが遅い
        //    if (Input.GetMouseButtonDown(0)) //指定したボタンが入力されるとジャンプ
        //    { 
        //        moveVelocity.y = jumpPower;
        //        startMousePosition = Input.mousePosition.x; //空中移動操作のため、ジャンプした瞬間のマウス位置を記録する
        //    }
        //}
        //else
        //{
        //    turnSpeed = jumpTurnSpeed; //空中にいる場合、地上にいるよりターンするスピードが速い
        //    moveVelocity.y += Physics.gravity.y * Time.deltaTime; //重力の影響を受ける

        //    if (Input.GetMouseButton(0)) //指定したボタンを押し続けていた場合、空中移動が可能
        //    {
        //        if (Input.mousePosition.x < startMousePosition) //ジャンプした瞬間のマウス位置より左にマウスを動かせば、左へ移動
        //        {
        //            moveVelocity.x = -stepSpeed;
        //        }
        //        else if (startMousePosition < Input.mousePosition.x) //ジャンプした瞬間のマウス位置より右にマウスを動かせば、右へ移動
        //        {
        //            moveVelocity.x = stepSpeed;
        //        }

        //        //プレイヤーの方向に合わせ、移動ベクトルを調整
        //        Vector3 step = new Vector3(moveVelocity.x * _transform.right.x, moveVelocity.y, moveVelocity.x * _transform.right.z);
        //        characterController.Move(step * Time.deltaTime); //移動を実行
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
