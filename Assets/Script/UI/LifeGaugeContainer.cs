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

    [SerializeField] private Camera mainCamera; //���C�t�Q�[�W�\���Ώۂ�Mob���ڂ��Ă���J����
    [SerializeField] private LifeGauge lifeGaugePrefab; //���C�t�Q�[�W��Prefab

    private RectTransform rectTransform; //���C�t�Q�[�W����莝�g
    //�A�N�e�B�u�ȃ��C�t�Q�[�W��ێ�����R���e�i
    private readonly Dictionary<MobStatus, LifeGauge> _statusLifeGaugeMap = new Dictionary<MobStatus, LifeGauge>();

    private void Awake()
    {
        if (null != _instance) throw new Exception("LifeGaugeContainer instance already exists");
        _instance = this;
        rectTransform = GetComponent<RectTransform>(); //�g��RectTransform���擾
    }

    //���C�t�Q�[�W��ǉ�
    public void Add(MobStatus status)
    { 
        LifeGauge lifeGauge = Instantiate(lifeGaugePrefab, this.transform); //���C�t�Q�[�WUI�𐶐�
        lifeGauge.Initialize(rectTransform, mainCamera, status);
        _statusLifeGaugeMap.Add(status, lifeGauge);
    }

    //���C�t�Q�[�W��j��
    public void Remove(MobStatus status)
    {
        Destroy(_statusLifeGaugeMap[status].gameObject);
        _statusLifeGaugeMap.Remove(status);
    }
}
