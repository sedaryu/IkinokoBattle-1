using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsDialog : MonoBehaviour
{
    [SerializeField] private int buttonNumber = 15; //ボタンの数
    [SerializeField] private ItemButton itemButton; //生成するボタンオブジェクト

    private ItemButton[] _itemButtons;

    // Start is called before the first frame update
    void Start()
    {
        //初期状態は非アクティブ
        gameObject.SetActive(false);

        //アイテム欄を生成
        for (int i = 0; i < buttonNumber - 1; i++)
        {
            Instantiate(itemButton, this.transform);
        }

        _itemButtons = GetComponentsInChildren<ItemButton>(); //子要素から一括取得し保持する
    }

    public void Toggle()
    { 
        gameObject.SetActive(!gameObject.activeSelf); //非アクティブならアクティブ化

        if (gameObject.activeSelf)
        {
            for (int i = 0; i < buttonNumber; i++)
            {
                if (OwnedItemsData.Instance.OwnedItems.Length > i)
                {
                    _itemButtons[i].OwnedItem = OwnedItemsData.Instance.OwnedItems[i];
                }
                else
                {
                    _itemButtons[i].OwnedItem = null;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
