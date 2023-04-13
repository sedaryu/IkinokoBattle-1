using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField] private WeaponType type;
    [SerializeField] private GameObject bulletPrefab;
    

}
