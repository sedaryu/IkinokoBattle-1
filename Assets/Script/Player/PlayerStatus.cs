using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerStatus : MobStatus
{
    public bool IsJumpable => (_state == StateEnum.Normal); //状態がNormalであればtrueを返す
    public bool IsStepable => (_state == StateEnum.Jump); //状態がJumpであればtrueを返す

    //ジャンプ状態に移行
    public void GoToJumpStateIfPossible()
    {
        //Jumpに移行できない状態であれば(状態がNormalでなければ)戻る
        if (!IsJumpable)
        {
            return;
        }

        _state = StateEnum.Jump; //状態をJumpへ移行
        //_animator.SetTrigger("Jump"); //ジャンプアニメーションを開始する
    }
}
