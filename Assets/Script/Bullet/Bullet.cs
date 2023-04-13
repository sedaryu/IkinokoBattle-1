using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum BulletType
    {
        Shell90mm,
        Pulse,
        Beam
    }

    [SerializeField] private BulletType type;

    public float Speed
    {
        get => _speed;
    }

    [SerializeField] private float _speed; //�e�ۂ̑���

    public float Damage
    {
        get => _damage;
    }

    [SerializeField] private float _damage; //�q�b�g�����ꍇ�A�^�[�Q�b�g�ɗ^����_���[�W��
}
