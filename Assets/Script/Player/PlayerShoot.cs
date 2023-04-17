using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private PlayerStatus _status;
    private Transform mainCamera;
    private GameObject virtualCamera;

    private List<GameObject> weapons = new List<GameObject>(); //�����v���n�u
    private List<Weapon> weaponScripts = new List<Weapon>();
    private List<GameObject> bullets = new List<GameObject>(); //�e�ۃv���n�u

    private int selectedWeapon = 0; //�g�p���̕����̃C���f�b�N�X�ԍ����w��

    private bool coolDown;

    private void Awake()
    {
        _status = GetComponent<PlayerStatus>();
        mainCamera = GameObject.Find("Main Camera").GetComponent<Transform>();
        virtualCamera = GameObject.Find("Virtual Camera");

        Weapon.WeaponType[] equippedWeapons = EquippedWeaponsData.Instance.EquippedWeapons; //�������̕������擾
        for (int i = 0; i < equippedWeapons.Length; i++) //Resources����v���n�u���擾
        {
            weapons.Add(Resources.Load($"Weapon/{equippedWeapons[i].ToString()}", typeof(GameObject)) as GameObject);
            weaponScripts.Add(weapons[i].GetComponent<Weapon>());
            bullets.Add(weaponScripts[i].Bullet); //����ɑΉ�����e�ۃv���n�u���擾
        }
        //�����v���n�u���C���X�^���X�i����̈ʒu�Ɂj

        weapons.ToList().ForEach(x => Debug.Log(x.GetComponent<Weapon>().Type.ToString()));
    }

    void Start()
    {

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
        if (coolDown) return;
        BulletController bullet = Instantiate(bullets[selectedWeapon], mainCamera.position, mainCamera.rotation).GetComponent<BulletController>();
        bullet.SettingDependOnWeaponStatus(weaponScripts[selectedWeapon].Stopping, weaponScripts[selectedWeapon].Reach);
        coolDown = true;
        StartCoroutine(CoolDownTime(weaponScripts[selectedWeapon].Cooldown));
    }

    private IEnumerator CoolDownTime(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        coolDown = false;
    }

    public void ChangeWeapon() //�g�p���镐����ύX����
    {
        selectedWeapon++;
        if (selectedWeapon >= weapons.Count)
        {
            selectedWeapon = 0;
        }
    }
}
