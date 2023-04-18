using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private Bullet bullet; //弾丸の種類・ステータスが記録されている

    private float stopping; //弾がヒットした際のノックバックの大きさ

    private float reach; //弾丸の射程距離

    public void SettingDependOnWeaponStatus(float _stopping, float _reach)
    {
        stopping = _stopping;
        reach = _reach;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyBullet());
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(0, 0, bullet.Speed * Time.deltaTime);
    }

    public void OnHitAttack(Collider collider) //弾丸がヒットした場合
    {
        MobStatus targetMob = collider.GetComponent<MobStatus>();
        if (targetMob == null) return;

        targetMob.Damage(bullet.Damage); //ダメージを与える
        targetMob.Knockback(this.transform.forward.normalized * stopping); //ノックバックのベクトルを渡す
        Destroy(gameObject);
    }

    private IEnumerator DestroyBullet() //一定時間経過後弾丸が消滅する処理
    {
        yield return new WaitForSeconds(reach);
        Destroy(gameObject);
    }
}
