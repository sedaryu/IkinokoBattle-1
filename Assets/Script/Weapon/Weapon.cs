using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Weapon : MonoBehaviour
{
    public enum WeaponType //武器の種類
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

    [SerializeField] private GameObject _bulletPrefab; //生成する弾丸オブジェクト

    public float Cooldown
    {
        get => _coolDown;
    }

    [SerializeField] private float _coolDown; //次弾発射までの間隔

    public float Stopping
    {
        get => _stopping;
    }

    [SerializeField] private float _stopping; //弾がヒットした際のノックバックの大きさ

    public float Reach
    { 
        get => _reach;
    }

    [SerializeField] private float _reach; //弾丸の射程距離
}
