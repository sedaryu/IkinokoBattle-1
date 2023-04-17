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

    private GameObject[] weapons; //武装プリハブ
    private GameObject[] bullets; //弾丸プリハブ

    private int selectedWeapon = 0; //使用中の武装のインデックス番号を指定

    void Start()
    {
        _status = GetComponent<PlayerStatus>();
        mainCamera = GameObject.Find("Main Camera").GetComponent<Transform>();
        virtualCamera = GameObject.Find("Virtual Camera");

        Weapon.WeaponType[] equippedWeapons = EquippedWeaponsData.Instance.EquippedWeapons; //装備中の武器種を取得
        weapons = new GameObject[equippedWeapons.Length];
        bullets = new GameObject[equippedWeapons.Length];
        for (int i = 0; i < equippedWeapons.Length; i++) //Resourcesからプリハブを取得
        {
            weapons[i] = Resources.Load($"Weapon/{equippedWeapons[i].ToString()}", typeof(GameObject)) as GameObject;
            bullets[i] = weapons[i].GetComponent<Weapon>().Bullet; //武器に対応する弾丸プリハブを取得
        }
        //武装プリハブをインスタンス（所定の位置に）

        weapons.ToList().ForEach(x => Debug.Log(x.GetComponent<Weapon>().Type.ToString()));
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
        Instantiate(bullets[selectedWeapon], mainCamera.position, mainCamera.rotation);
    }

    public void ChangeWeapon() //使用する武装を変更する
    {
        selectedWeapon++;
        if (selectedWeapon >= weapons.Length)
        {
            selectedWeapon = 0;
        }
    }
}
