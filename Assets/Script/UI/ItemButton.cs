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

            bool isEmpty = null == _ownedItem; //値がnullならばtrue
            image.gameObject.SetActive(!isEmpty); //trueならばイメージが非アクティブ化
            number.gameObject.SetActive(!isEmpty); //trueならばナンバーも非アクティブ化
            _button.interactable = !isEmpty; //ボタンも使用不能に

            if (!isEmpty) //値がnullでなかった場合
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
