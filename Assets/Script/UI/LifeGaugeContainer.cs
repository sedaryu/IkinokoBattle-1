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

    //�A�N�e�B�u�ȃ��C�t�Q�[�W��ێ�����R���e�i
    private readonly Dictionary<MobStatus, LifeGauge> _statusLifeGaugeMap = new Dictionary<MobStatus, LifeGauge>();

    private void Awake()
    {
        if (null != _instance) throw new Exception("LifeGaugeContainer instance already exists");
        _instance = this;
        playerStatus = GameObject.Find("Query-Chan-SD").GetComponent<PlayerStatus>();
    }

    private void Update() 
    {
        if (playerStatus.IsShootable) //�v���C���[���ˌ���Ԃ̏ꍇ
        {
            _statusLifeGaugeMap.Values.ToList().ForEach(x => x.gameObject.SetActive(true)); //���C�t�Q�[�W���A�N�e�B�u��
        }
        else //����ȊO�̏ꍇ
        {
            _statusLifeGaugeMap.Values.ToList().ForEach(x => x.gameObject.SetActive(false)); //��A�N�e�B�u��
        }
    }

    //���C�t�Q�[�W��ǉ�
    public void Add(MobStatus status)
    {
        LifeGauge lifeGauge = Instantiate(lifeGaugeCanvasPrefab);

        lifeGauge.Initialize(status);
        _statusLifeGaugeMap.Add(status, lifeGauge);
    }

    //���C�t�Q�[�W��j��
    public void Remove(MobStatus status)
    {
        if (_statusLifeGaugeMap[status] == null) return;

        Destroy(_statusLifeGaugeMap[status].gameObject);
        _statusLifeGaugeMap.Remove(status);
    }
}
