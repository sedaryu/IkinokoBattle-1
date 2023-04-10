using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Item : MonoBehaviour
{
    public enum ItemType //アイテムの種類定義
    { 
        Wood,
        Stone,
        ThrowAxe
    }

    [SerializeField] private ItemType type;

    public void Initialize()
    {
        SpawnAnimation();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnAnimation()
    {
        //出現アニメーション
        Collider colliderCache = this.GetComponent<Collider>(); //コリダーを取得（アニメションが終わるまで無効化するため）
        colliderCache.enabled = false;

        Transform transformCache = this.transform; //トランスフォームを取得
        Vector3 dropPosition = transform.localPosition + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)); //出現先を決定
        transformCache.DOLocalMove(dropPosition, 0.5f); //出現アニメーション(移動)

        Vector3 defaultScale = transformCache.localScale; //元のアイテムのスケールを取得
        transformCache.localScale = Vector3.zero; //ゼロにする
        transformCache.DOScale(defaultScale, 0.5f) //出現アニメーション(巨大化)
            .SetEase(Ease.OutBounce) //バウンド風のイージングを付与
            .OnComplete(() =>
            {
                colliderCache.enabled = true; //アニメーションが完了すればコリダーを有効化
            });
    }

    private void OnTriggerEnter(Collider other) //接触時
    {
        if (!other.CompareTag("Player")) return; //プレイヤー以外が接触した場合、無効
        OwnedItemsData.Instance.Add(type);
        OwnedItemsData.Instance.Save();
        foreach (var item in OwnedItemsData.Instance.OwnedItems)
        {
            Debug.Log(item.Type + "を" + item.Number + "個所持");
        }

        Destroy(gameObject);
    }
}
