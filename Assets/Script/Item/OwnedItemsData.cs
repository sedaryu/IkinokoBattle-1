using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class OwnedItemsData
{
    private const string PlayerPrefsKey = "OWNED_ITEMS_DATA"; //PlayerPrefs保存先キー(不変)

    public static OwnedItemsData Instance //インスタンスを返す
    {
        get 
        {
            if (null == _instance) //_instanceがnullだった場合
            {
                if (PlayerPrefs.HasKey(PlayerPrefsKey)) //指定したPlayerPrefのキーにデータがあった場合
                {
                    _instance = JsonUtility.FromJson<OwnedItemsData>(PlayerPrefs.GetString(PlayerPrefsKey));
                }
                else //なかった場合
                { 
                    _instance = new OwnedItemsData();
                }
            }

            return _instance;
        }
    }

    private static OwnedItemsData _instance;

    public OwnedItem[] OwnedItems //所持アイテム一覧を返す
    {
        get { return ownedItems.ToArray(); }
    }

    [SerializeField] private List<OwnedItem> ownedItems = new List<OwnedItem>(); //どのアイテムを何個所持しているかを格納

    private OwnedItemsData() //OwnedItemDataクラスがインスタンスされるのを防ぐ
    {
    }

    public void Save() //string型のJsonデータにコンバートしてPlayerPrefsに保存
    { 
        string jsonString = JsonUtility.ToJson(this);
        PlayerPrefs.SetString(PlayerPrefsKey, jsonString);
        PlayerPrefs.Save();
    }

    public void Add(Item.ItemType type, int number = 1)
    { 
        OwnedItem item = GetItem(type);
        if (null == item) //未入手の種類のアイテムだった場合
        {
            item = new OwnedItem(type); //その種類のOwnedItemをインスタンスする
            ownedItems.Add(item); //追加
        }
        item.Add(number); //個数を追加
    }

    public void Use(Item.ItemType type, int number = 1)
    {
        OwnedItem item = GetItem(type);
        if (null == item || item.Number < number)
        {
            throw new Exception("アイテムが足りません");
        }
        item.Use(number); //個数を減少
    }

    public OwnedItem GetItem(Item.ItemType type) //そのアイテムが入手済みの種類かどうか調べるメソッド
    {
        return ownedItems.FirstOrDefault(x => x.Type == type);
    }

    [Serializable]
    public class OwnedItem //アイテムの所持数管理用Model
    { 
        public Item.ItemType Type
        {
            get { return type; }
        }
        public int Number
        {
            get { return number; }
        }

        //アイテムの種類
        [SerializeField] private Item.ItemType type;
        //アイテムの所持個数
        [SerializeField] private int number;

        public OwnedItem(Item.ItemType type)
        {
            this.type = type;
        }
        public void Add(int number = 1)
        {
            this.number += number;
        }
        public void Use(int number = 1)
        {
            this.number -= number;
        }
    }
}
