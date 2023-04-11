using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsDialog : MonoBehaviour
{
    [SerializeField] private int buttonNumber = 15; //�{�^���̐�
    [SerializeField] private ItemButton itemButton; //��������{�^���I�u�W�F�N�g

    private ItemButton[] _itemButtons;

    // Start is called before the first frame update
    void Start()
    {
        //������Ԃ͔�A�N�e�B�u
        gameObject.SetActive(false);

        //�A�C�e�����𐶐�
        for (int i = 0; i < buttonNumber - 1; i++)
        {
            Instantiate(itemButton, this.transform);
        }

        _itemButtons = GetComponentsInChildren<ItemButton>(); //�q�v�f����ꊇ�擾���ێ�����
    }

    public void Toggle()
    { 
        gameObject.SetActive(!gameObject.activeSelf); //��A�N�e�B�u�Ȃ�A�N�e�B�u��

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
