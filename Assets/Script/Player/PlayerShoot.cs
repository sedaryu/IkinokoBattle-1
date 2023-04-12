using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private PlayerStatus _status;
    private Transform mainCamera;
    private GameObject virtualCamera;

    [SerializeField] private GameObject bulletPrefab; //�o���b�h�v���n�u

    void Start()
    {
        _status = GetComponent<PlayerStatus>();
        mainCamera = GameObject.Find("Main Camera").GetComponent<Transform>();
        virtualCamera = GameObject.Find("Virtual Camera");
    }

    public void ShootIfPossible()
    {
        if (!_status.IsMovable) return;

        _status.GoToShootStateIfPossible(); //MobStatus��_state��Attack�ɕύX

        //�J��������
        virtualCamera.SetActive(false);
        mainCamera.position = this.transform.position + new Vector3(0, 0.5f, 0);
        mainCamera.rotation = this.transform.rotation;
    }

    public void CancelShoot()
    {
        _status.GoToNormalStateIfPossible(); //�ʏ��Ԃ֖߂�

        //�J��������
        virtualCamera.SetActive(true);
    }

    public void Aiming(Vector3 aim)
    {
        //�J��������
        mainCamera.Rotate(0, aim.y * Time.deltaTime, 0);
    }

    public void Shooting()
    {
        Instantiate(bulletPrefab, mainCamera.position, mainCamera.rotation);
    }
}
