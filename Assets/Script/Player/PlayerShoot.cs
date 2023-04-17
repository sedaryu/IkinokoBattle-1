using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private PlayerStatus _status;
    private Transform mainCamera;
    private GameObject virtualCamera;

    private GameObject[] weapons; //�����v���n�u
    private GameObject[] bullets; //�e�ۃv���n�u

    private int selectedWeapon = 0; //�g�p���̕����̃C���f�b�N�X�ԍ����w��

    void Start()
    {
        _status = GetComponent<PlayerStatus>();
        mainCamera = GameObject.Find("Main Camera").GetComponent<Transform>();
        virtualCamera = GameObject.Find("Virtual Camera");

        Weapon.WeaponType[] equippedWeapons = EquippedWeaponsData.Instance.EquippedWeapons; //�������̕������擾
        weapons = new GameObject[equippedWeapons.Length];
        bullets = new GameObject[equippedWeapons.Length];
        for (int i = 0; i < equippedWeapons.Length; i++) //Resources����v���n�u���擾
        {
            weapons[i] = Resources.Load($"Weapon/{equippedWeapons[i].ToString()}", typeof(GameObject)) as GameObject;
            bullets[i] = weapons[i].GetComponent<Weapon>().Bullet; //����ɑΉ�����e�ۃv���n�u���擾
        }
        //�����v���n�u���C���X�^���X�i����̈ʒu�Ɂj

        weapons.ToList().ForEach(x => Debug.Log(x.GetComponent<Weapon>().Type.ToString()));
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

    public void Shooting() //�e�𔭎˂���
    {
        Instantiate(bullets[selectedWeapon], mainCamera.position, mainCamera.rotation);
    }

    public void ChangeWeapon() //�g�p���镐����ύX����
    {
        selectedWeapon++;
        if (selectedWeapon >= weapons.Length)
        {
            selectedWeapon = 0;
        }
    }
}
