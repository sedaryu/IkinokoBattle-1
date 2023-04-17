using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static OwnedItemsData;

[Serializable]
public class EquippedWeaponsData
{
    private const string JsonDataKey = "EQUIPPED_WEAPONS_DATA"; //Json�t�@�C����(�s��)

    public static EquippedWeaponsData Instance //�C���X�^���X��Ԃ�
    {
        get
        {
            if (null == _instance) //_instance��null�������ꍇ
            {
                if (LoadJsonEquippedWeaponsData(out EquippedWeaponsData equippedWeaponsData)) //�w���Json�t�@�C���Ƀf�[�^���������ꍇ
                {
                    _instance = equippedWeaponsData;
                }
                else //�Ȃ������ꍇ
                {
                    _instance = new EquippedWeaponsData();
                }
            }

            return _instance;
        }
    }

    private static EquippedWeaponsData _instance; //�C���X�^���X���i�[����Ă���

    public Weapon.WeaponType[] EquippedWeapons //���������ꗗ��Ԃ�
    {
        get { return equippedWeapons.ToArray(); }
    }

    [SerializeField] private List<Weapon.WeaponType> equippedWeapons = new List<Weapon.WeaponType>(); //�������̕������i�[

    private EquippedWeaponsData()
    { 
    }

    static private bool LoadJsonEquippedWeaponsData(out EquippedWeaponsData equippedWeaponsData) //Json�t�@�C�����畐���f�[�^��ǂݍ���
    {
        string datastr = "";
        StreamReader reader;
        reader = new StreamReader(Application.dataPath + $"/StreamingAssets/JsonData/{JsonDataKey}.json");
        datastr = reader.ReadToEnd();
        reader.Close();

        if (datastr == "")
        {
            equippedWeaponsData = null;
            return false;
        }
        else
        {
            equippedWeaponsData = JsonUtility.FromJson<EquippedWeaponsData>(datastr);
            return true;
        }
    }
}
