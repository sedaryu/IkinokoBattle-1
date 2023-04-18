using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private Bullet bullet; //�e�ۂ̎�ށE�X�e�[�^�X���L�^����Ă���

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
        StartCoroutine(DestroyBullet());
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(0, 0, bullet.Speed * Time.deltaTime);
    }

    public void OnHitAttack(Collider collider) //�e�ۂ��q�b�g�����ꍇ
    {
        MobStatus targetMob = collider.GetComponent<MobStatus>();
        if (targetMob == null) return;

        targetMob.Damage(bullet.Damage); //�_���[�W��^����
        targetMob.Knockback(this.transform.forward.normalized * stopping); //�m�b�N�o�b�N�̃x�N�g����n��
        Destroy(gameObject);
    }

    private IEnumerator DestroyBullet() //��莞�Ԍo�ߌ�e�ۂ����ł��鏈��
    {
        yield return new WaitForSeconds(reach);
        Destroy(gameObject);
    }
}
