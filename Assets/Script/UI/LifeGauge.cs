using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeGauge : MonoBehaviour
{
    [SerializeField] private Image fillImage;

    private MobStatus _status;

    // Update is called once per frame
    void Update()
    {
        Refresh();
    }

    //�Q�[�W�̏�����
    public void Initialize(MobStatus status)
    {
        _status = status;
        this.gameObject.transform.parent = _status.transform;
        this.transform.localPosition = new Vector3(0, 5, 0);
        this.transform.localScale = new Vector3(7.5f, 7.5f, 7.5f);
        Refresh();
    }

    //�Q�[�W�X�V
    private void Refresh()
    {
        fillImage.fillAmount = _status.Life / _status.LifeMax; //�c�胉�C�t�\��
        this.transform.rotation = Camera.main.transform.rotation; //�Q�[�W���J���������Ɍ����悤����
    }
}
