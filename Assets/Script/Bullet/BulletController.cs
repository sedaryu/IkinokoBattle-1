using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Bullet bullet; //�e�ۂ̎�ށE�X�e�[�^�X���L�^����Ă���

    private float stopping; //�e���q�b�g�����ۂ̃m�b�N�o�b�N�̑傫��

    private float reach; //�e�ۂ̎˒�����

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
