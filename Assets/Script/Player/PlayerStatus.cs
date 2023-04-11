using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerStatus : MobStatus
{
    public bool IsStepable => (_state == StateEnum.Jump); //��Ԃ�Jump�ł����true��Ԃ�
    public bool IsShootable => (_state == StateEnum.Shoot);

    //�W�����v��ԂɈڍs
    public void GoToJumpStateIfPossible()
    {
        //Jump�Ɉڍs�ł��Ȃ���Ԃł����(��Ԃ�Normal�łȂ����)�߂�
        if (!IsMovable)
        {
            return;
        }

        _state = StateEnum.Jump; //��Ԃ�Jump�ֈڍs
        //_animator.SetTrigger("Jump"); //�W�����v�A�j���[�V�������J�n����
    }

    //�ˌ���ԂɈڍs
    public void GoToShootStateIfPossible()
    {
        //Shoot�Ɉڍs�ł��Ȃ���Ԃł����(��Ԃ�Normal�łȂ����)�߂�
        if (!IsMovable)
        {
            return;
        }

        _state = StateEnum.Shoot; //��Ԃ�Shoot�ֈڍs
    }
}
