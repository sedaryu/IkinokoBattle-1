using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class LifeGaugeContainer : MonoBehaviour
{
    public static LifeGaugeContainer Instance
    {
        get { return _instance; }
    }

    private static LifeGaugeContainer _instance;

    [SerializeField] private Camera mainCamera; //ライフゲージ表示対象のMobを移しているカメラ
    [SerializeField] private LifeGauge lifeGaugePrefab; //ライフゲージのPrefab

    private RectTransform rectTransform; //ライフゲージを取り持つ枠
    //アクティブなライフゲージを保持するコンテナ
    private readonly Dictionary<MobStatus, LifeGauge> _statusLifeGaugeMap = new Dictionary<MobStatus, LifeGauge>();

    private void Awake()
    {
        if (null != _instance) throw new Exception("LifeGaugeContainer instance already exists");
        _instance = this;
        rectTransform = GetComponent<RectTransform>(); //枠のRectTransformを取得
    }

    //ライフゲージを追加
    public void Add(MobStatus status)
    { 
        LifeGauge lifeGauge = Instantiate(lifeGaugePrefab, this.transform); //ライフゲージUIを生成
        lifeGauge.Initialize(rectTransform, mainCamera, status);
        _statusLifeGaugeMap.Add(status, lifeGauge);
    }

    //ライフゲージを破棄
    public void Remove(MobStatus status)
    {
        Destroy(_statusLifeGaugeMap[status].gameObject);
        _statusLifeGaugeMap.Remove(status);
    }
}
