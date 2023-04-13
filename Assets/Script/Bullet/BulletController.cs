using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Bullet bullet; //弾丸の種類・ステータスが記録されている

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyBullet());
        bullet = GetComponent<Bullet>();
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

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(gameObject);
    }
}
