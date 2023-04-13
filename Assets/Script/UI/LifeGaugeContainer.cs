using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Search;
using UnityEngine;

public class LifeGaugeContainer : MonoBehaviour
{
    public static LifeGaugeContainer Instance
    {
        get { return _instance; }
    }

    private static LifeGaugeContainer _instance;

    [SerializeField] private LifeGauge lifeGaugeCanvasPrefab;
    private PlayerStatus playerStatus;

    //アクティブなライフゲージを保持するコンテナ
    private readonly Dictionary<MobStatus, LifeGauge> _statusLifeGaugeMap = new Dictionary<MobStatus, LifeGauge>();

    private void Awake()
    {
        if (null != _instance) throw new Exception("LifeGaugeContainer instance already exists");
        _instance = this;
        playerStatus = GameObject.Find("Query-Chan-SD").GetComponent<PlayerStatus>();
    }

    private void Update() 
    {
        if (playerStatus.IsShootable) //プレイヤーが射撃状態の場合
        {
            _statusLifeGaugeMap.Values.ToList().ForEach(x => x.gameObject.SetActive(true)); //ライフゲージをアクティブ化
        }
        else //それ以外の場合
        {
            _statusLifeGaugeMap.Values.ToList().ForEach(x => x.gameObject.SetActive(false)); //非アクティブ化
        }
    }

    //ライフゲージを追加
    public void Add(MobStatus status)
    {
        LifeGauge lifeGauge = Instantiate(lifeGaugeCanvasPrefab);

        lifeGauge.Initialize(status);
        _statusLifeGaugeMap.Add(status, lifeGauge);
    }

    //ライフゲージを破棄
    public void Remove(MobStatus status)
    {
        if (_statusLifeGaugeMap[status] == null) return;

        Destroy(_statusLifeGaugeMap[status].gameObject);
        _statusLifeGaugeMap.Remove(status);
    }
}
