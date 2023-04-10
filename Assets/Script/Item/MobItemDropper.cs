using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MobStatus))]
public class MobItemDropper : MonoBehaviour
{
    [SerializeField] [Range(0, 1)] private float dropRate = 0.1f; //アイテム出現確率(デフォルト値は0.1)
    [SerializeField] private Item itemPrefab; //プレハブを格納
    [SerializeField] private int number = 1; //アイテム出現個数

    private MobStatus _status; //アイテムをドロップするキャラのステータスを取得
    private bool _isDropInvoked;

    // Start is called before the first frame update
    void Start()
    {
        _status = this.GetComponent<MobStatus>(); //ステータスを取得
    }

    // Update is called once per frame
    void Update()
    {
        if (_status.Life <= 0) //ライフが尽きた場合
        {
            DropIfNeeded(); //ドロップ判定処理が実行
        }
    }

    private void DropIfNeeded() //アイテムがキャラからドロップするかどうかを判定し処理
    {
        if (_isDropInvoked) return; //ドロップ中かどうかを判定

        _isDropInvoked = true;

        if (Random.Range(0, 1f) >= dropRate) return; //ドロップ確率判定

        for (int i = 0; i < number; i++) //指定された個数ぶん生成
        {
            Item item = Instantiate(itemPrefab, this.transform.position, Quaternion.identity); //Itemスクリプトを返す
            item.Initialize(); //出現アニメーションを実行
        }
    }
}
