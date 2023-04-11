using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private PlayerStatus _status;

    void Start()
    {
        _status = GetComponent<PlayerStatus>();
    }

    public void ShootIfPossible()
    {
        if (!_status.IsMovable) return;

        _status.GoToShootStateIfPossible(); //MobStatus��_state��Attack�ɕύX
    }

    public void CancelShoot()
    {
        _status.GoToNormalStateIfPossible(); //�ʏ��Ԃ֖߂�
    }
}
