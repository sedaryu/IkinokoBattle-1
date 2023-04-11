using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class OwnedItemsData
{
    private const string PlayerPrefsKey = "OWNED_ITEMS_DATA"; //PlayerPrefs�ۑ���L�[(�s��)

    public static OwnedItemsData Instance //�C���X�^���X��Ԃ�
    {
        get 
        {
            if (null == _instance) //_instance��null�������ꍇ
            {
                if (PlayerPrefs.HasKey(PlayerPrefsKey)) //�w�肵��PlayerPref�̃L�[�Ƀf�[�^���������ꍇ
                {
                    _instance = JsonUtility.FromJson<OwnedItemsData>(PlayerPrefs.GetString(PlayerPrefsKey));
                }
                else //�Ȃ������ꍇ
                { 
                    _instance = new OwnedItemsData();
                }
            }

            return _instance;
        }
    }

    private static OwnedItemsData _instance;

    public OwnedItem[] OwnedItems //�����A�C�e���ꗗ��Ԃ�
    {
        get { return ownedItems.ToArray(); }
    }

    [SerializeField] private List<OwnedItem> ownedItems = new List<OwnedItem>(); //�ǂ̃A�C�e�������������Ă��邩���i�[

    private OwnedItemsData() //OwnedItemData�N���X���C���X�^���X�����̂�h��
    {
    }

    public void Save() //string�^��Json�f�[�^�ɃR���o�[�g����PlayerPrefs�ɕۑ�
    { 
        string jsonString = JsonUtility.ToJson(this);
        PlayerPrefs.SetString(PlayerPrefsKey, jsonString);
        PlayerPrefs.Save();
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
        return ownedItems.FirstOrDefault(x => x.Type == type);
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
}
