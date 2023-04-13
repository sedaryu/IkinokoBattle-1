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

    //ゲージの初期化
    public void Initialize(MobStatus status)
    {
        _status = status;
        this.gameObject.transform.parent = _status.transform;
        this.transform.localPosition = new Vector3(0, 5, 0);
        this.transform.localScale = new Vector3(7.5f, 7.5f, 7.5f);
        Refresh();
    }

    //ゲージ更新
    private void Refresh()
    {
        fillImage.fillAmount = _status.Life / _status.LifeMax; //残りライフ表示
        this.transform.rotation = Camera.main.transform.rotation; //ゲージがカメラ方向に向くよう調整
    }
}
