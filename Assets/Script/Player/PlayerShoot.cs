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

        _status.GoToShootStateIfPossible(); //MobStatusの_stateをAttackに変更
    }

    public void CancelShoot()
    {
        _status.GoToNormalStateIfPossible(); //通常状態へ戻る
    }
}
