using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeGauge : MonoBehaviour
{
    [SerializeField] private Image fillImage;

    private RectTransform _parentRectTransform;
    private Camera _camera;
    private MobStatus _status;

    // Update is called once per frame
    void Update()
    {
        
    }

    //�Q�[�W�̏�����
    public void Initialize(RectTransform parentRectTransform, Camera camera, MobStatus status)
    {
        _parentRectTransform = parentRectTransform;
        _camera = camera;
        _status = status;
    }

    //�Q�[�W�X�V
    private void Refresh()
    {
        fillImage.fillAmount = _status.Life / _status.LifeMax; //�c�胉�C�t�\��

        //�Ώ�Mob�̏ꏊ�ɃQ�[�W���ړ�����
        Vector3 screenPoint = _camera.WorldToScreenPoint(_status.transform.position);
        Vector2 localPoint;
        //�X�N���[�����W�����[�J�����W�֕ϊ����郁�\�b�h
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_parentRectTransform, screenPoint, null, out localPoint);
        transform.localPosition = localPoint + new Vector2(0, 80); //�ʒu����
    }
}
