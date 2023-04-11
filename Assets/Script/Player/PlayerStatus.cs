using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerStatus : MobStatus
{
    public bool IsStepable => (_state == StateEnum.Jump); //状態がJumpであればtrueを返す
    public bool IsShootable => (_state == StateEnum.Shoot);

    //ジャンプ状態に移行
    public void GoToJumpStateIfPossible()
    {
        //Jumpに移行できない状態であれば(状態がNormalでなければ)戻る
        if (!IsMovable)
        {
            return;
        }

        _state = StateEnum.Jump; //状態をJumpへ移行
        //_animator.SetTrigger("Jump"); //ジャンプアニメーションを開始する
    }

    //射撃状態に移行
    public void GoToShootStateIfPossible()
    {
        //Shootに移行できない状態であれば(状態がNormalでなければ)戻る
        if (!IsMovable)
        {
            return;
        }

        _state = StateEnum.Shoot; //状態をShootへ移行
    }
}
