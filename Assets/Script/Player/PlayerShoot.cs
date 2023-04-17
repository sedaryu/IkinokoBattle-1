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

    private List<GameObject> weapons = new List<GameObject>(); //武装プリハブ
    private List<Weapon> weaponScripts = new List<Weapon>();
    private List<GameObject> bullets = new List<GameObject>(); //弾丸プリハブ

    private int selectedWeapon = 0; //使用中の武装のインデックス番号を指定

    private bool coolDown;

    private void Awake()
    {
        _status = GetComponent<PlayerStatus>();
        mainCamera = GameObject.Find("Main Camera").GetComponent<Transform>();
        virtualCamera = GameObject.Find("Virtual Camera");

        Weapon.WeaponType[] equippedWeapons = EquippedWeaponsData.Instance.EquippedWeapons; //装備中の武器種を取得
        for (int i = 0; i < equippedWeapons.Length; i++) //Resourcesからプリハブを取得
        {
            weapons.Add(Resources.Load($"Weapon/{equippedWeapons[i].ToString()}", typeof(GameObject)) as GameObject);
            weaponScripts.Add(weapons[i].GetComponent<Weapon>());
            bullets.Add(weaponScripts[i].Bullet); //武器に対応する弾丸プリハブを取得
        }
        //武装プリハブをインスタンス（所定の位置に）

        weapons.ToList().ForEach(x => Debug.Log(x.GetComponent<Weapon>().Type.ToString()));
    }

    void Start()
    {

    }

    public void ShootIfPossible()
    {
        if (!_status.IsMovable) return;

        _status.GoToShootStateIfPossible(); //MobStatusの_stateをAttackに変更

        //カメラ操作
        virtualCamera.SetActive(false);
        mainCamera.position = this.transform.position + new Vector3(0, 0.5f, 0);
        mainCamera.rotation = this.transform.rotation;
    }

    public void CancelShoot()
    {
        _status.GoToNormalStateIfPossible(); //通常状態へ戻る

        //カメラ操作
        virtualCamera.SetActive(true);
    }

    public void Aiming(Vector3 aim)
    {
        //カメラ操作
        mainCamera.Rotate(0, aim.y * Time.deltaTime, 0);
    }

    public void Shooting() //弾を発射する
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

    public void ChangeWeapon() //使用する武装を変更する
    {
        selectedWeapon++;
        if (selectedWeapon >= weapons.Count)
        {
            selectedWeapon = 0;
        }
    }
}
