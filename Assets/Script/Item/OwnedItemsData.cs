using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

[Serializable]
public class OwnedItemsData
{
    private const string JsonDataKey = "OWNED_ITEMS_DATA"; //Json�t�@�C����(�s��)

    public static OwnedItemsData Instance //�C���X�^���X��Ԃ�
    {
        get 
        {
            if (null == _instance) //_instance��null�������ꍇ
            {
                if (LoadJsonOwnedItemData(out OwnedItemsData ownedItemsData)) //�w���Json�t�@�C���Ƀf�[�^���������ꍇ
                {
                    _instance = ownedItemsData;
                }
                else //�Ȃ������ꍇ
                { 
                    _instance = new OwnedItemsData();
                }
            }

            return _instance;
        }
    }

    private static OwnedItemsData _instance; //�C���X�^���X���i�[����Ă���

    public OwnedItem[] OwnedItems //�����A�C�e���ꗗ��Ԃ�
    {
        get { return ownedItems.ToArray(); }
    }

    [SerializeField] private List<OwnedItem> ownedItems = new List<OwnedItem>(); //�ǂ̃A�C�e�������������Ă��邩(OwnedItem�N���X)���i�[

    private OwnedItemsData() //OwnedItemData�N���X���C���X�^���X�����̂�h��
    {
    }

    public void Save() //�w���Json�t�@�C���Ƀf�[�^���㏑���ۑ�
    {
        StreamWriter writer;
        string jsonstr = JsonUtility.ToJson(this);
        writer = new StreamWriter(Application.dataPath + $"/StreamingAssets/JsonData/{JsonDataKey}.json", false);
        writer.WriteLine(jsonstr);
        writer.Close();
    }

    public void Add(Item.ItemType type, int number = 1)
    { 
        OwnedItem item = GetItem(type);
        if (null == item) //������̎�ނ̃A�C�e���������ꍇ
        {
            item = new OwnedItem(type); //���̎�ނ�OwnedItem���C���X�^���X����
            ownedItems.Add(item); //�ǉ�
        }
        item.Add(number); //����ǉ�
    }

    public void Use(Item.ItemType type, int number = 1)
    {
        OwnedItem item = GetItem(type);
        if (null == item || item.Number < number)
        {
            throw new Exception("�A�C�e��������܂���");
        }
        item.Use(number); //��������
    }

    public OwnedItem GetItem(Item.ItemType type) //���̃A�C�e��������ς݂̎�ނ��ǂ������ׂ郁�\�b�h
    {
        return ownedItems.FirstOrDefault(x => x.Type == type); //������̏ꍇnull��Ԃ�
    }

    [Serializable]
    public class OwnedItem //�A�C�e���̏������Ǘ��pModel
    { 
        public Item.ItemType Type
        {
            get { return type; }
        }
        public int Number
        {
            get { return number; }
        }

        //�A�C�e���̎��
        [SerializeField] private Item.ItemType type;
        //�A�C�e���̏�����
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

    static private bool LoadJsonOwnedItemData(out OwnedItemsData ownedItemsData)
    {
        string datastr = "";
        StreamReader reader;
        reader = new StreamReader(Application.dataPath + $"/StreamingAssets/JsonData/{JsonDataKey}.json");
        datastr = reader.ReadToEnd();
        reader.Close();

        if (datastr == "")
        {
            ownedItemsData = null;
            return false;
        }
        else
        {
            ownedItemsData = JsonUtility.FromJson<OwnedItemsData>(datastr);
            return true;
        }
    }
}
