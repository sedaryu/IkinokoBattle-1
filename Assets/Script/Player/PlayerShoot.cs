using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private PlayerStatus _status;
    private Transform mainCamera;
    private GameObject virtualCamera;

    void Start()
    {
        _status = GetComponent<PlayerStatus>();
        mainCamera = GameObject.Find("Main Camera").GetComponent<Transform>();
        virtualCamera = GameObject.Find("Virtual Camera");
    }

    public void ShootIfPossible()
    {
        if (!_status.IsMovable) return;

        _status.GoToShootStateIfPossible(); //MobStatus‚Ì_state‚ğAttack‚É•ÏX

        //ƒJƒƒ‰‘€ì
        virtualCamera.SetActive(false);
        mainCamera.position = this.transform.position + new Vector3(0, 0.5f, 0);
        mainCamera.rotation = this.transform.rotation;
    }

    public void CancelShoot()
    {
        _status.GoToNormalStateIfPossible(); //’Êíó‘Ô‚Ö–ß‚é

        //ƒJƒƒ‰‘€ì
        virtualCamera.SetActive(true);
    }
}
