using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class EquippedWeaponsData
{
    private const string JsonDataKey = "EQUIPPED_WEAPONS_DATA"; //Jsonファイル名(不変)

    public static EquippedWeaponsData Instance //インスタンスを返す
    {
        get
        {
            if (null == _instance) //_instanceがnullだった場合
            {
                if (LoadJsonEquippedWeaponsData(out EquippedWeaponsData equippedWeaponsData)) //指定のJsonファイルにデータがあった場合
                {
                    _instance = equippedWeaponsData;
                }
                else //なかった場合
                {
                    _instance = new EquippedWeaponsData();
                }
            }

            return _instance;
        }
    }

    private static EquippedWeaponsData _instance; //インスタンスが格納されている

    static private bool LoadJsonEquippedWeaponsData(out EquippedWeaponsData equippedWeaponsData) //Jsonファイルから武装データを読み込む
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
