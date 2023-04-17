using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Weapon : MonoBehaviour
{
    public enum WeaponType //����̎��
    {
        HandPistol,
        PistolCarbine,
        PulseGun,
        PulseRifle,
        BeamLauncher,
    }

    public WeaponType Type
    {
        get => _type;
    }

    [SerializeField] private WeaponType _type;

    public GameObject Bullet
    {
        get => _bulletPrefab;
    }

    [SerializeField] private GameObject _bulletPrefab; //��������e�ۃI�u�W�F�N�g

    public float Cooldown
    {
        get => _coolDown;
    }

    [SerializeField] private float _coolDown; //���e���˂܂ł̊Ԋu

    public float Stopping
    {
        get => _stopping;
    }

    [SerializeField] private float _stopping; //�e���q�b�g�����ۂ̃m�b�N�o�b�N�̑傫��

    public float Reach
    { 
        get => _reach;
    }

    [SerializeField] private float _reach; //�e�ۂ̎˒�����
}
