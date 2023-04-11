using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerStatus : MobStatus
{
    public bool IsJumpable => (_state == StateEnum.Normal); //��Ԃ�Normal�ł����true��Ԃ�
    public bool IsStepable => (_state == StateEnum.Jump); //��Ԃ�Jump�ł����true��Ԃ�

    //�W�����v��ԂɈڍs
    public void GoToJumpStateIfPossible()
    {
        //Jump�Ɉڍs�ł��Ȃ���Ԃł����(��Ԃ�Normal�łȂ����)�߂�
        if (!IsJumpable)
        {
            return;
        }

        _state = StateEnum.Jump; //��Ԃ�Jump�ֈڍs
        //_animator.SetTrigger("Jump"); //�W�����v�A�j���[�V�������J�n����
    }
}
