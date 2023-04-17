using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Bullet bullet; //弾丸の種類・ステータスが記録されている

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
        bullet = this.GetComponent<Bullet>();
        StartCoroutine(DestroyBullet());
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(0, 0, bullet.Speed * Time.deltaTime);
        Debug.Log("Flying");
    }

    public void OnHitAttack(Collider collider)
    {
        MobStatus targetMob = collider.GetComponent<MobStatus>();
        Debug.Log("BulletHitting");
        if (targetMob == null) return;

        targetMob.Damage(bullet.Damage);
        Destroy(gameObject);
    }

    private IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(reach);
        Destroy(gameObject);
    }
}
