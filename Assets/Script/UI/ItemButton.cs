using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ItemButton : MonoBehaviour
{
    public OwnedItemsData.OwnedItem OwnedItem
    {
        get { return _ownedItem; }
        set
        {
            _ownedItem = value;

            bool isEmpty = null == _ownedItem; //�l��null�Ȃ��true
            image.gameObject.SetActive(!isEmpty); //true�Ȃ�΃C���[�W����A�N�e�B�u��
            number.gameObject.SetActive(!isEmpty); //true�Ȃ�΃i���o�[����A�N�e�B�u��
            _button.interactable = !isEmpty; //�{�^�����g�p�s�\��

            if (!isEmpty) //�l��null�łȂ������ꍇ
            {
                image.sprite = Resources.Load($"Image/Item/{_ownedItem.Type.ToString()}", typeof(Sprite)) as Sprite;
                number.text = "" + _ownedItem.Number;
            }
        }
    }

    [SerializeField] private Image image;
    [SerializeField] private Text number;

    private Button _button;
    private OwnedItemsData.OwnedItem _ownedItem;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnClick()
    { 
        
    }
}
