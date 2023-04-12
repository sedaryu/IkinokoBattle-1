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

    //ゲージの初期化
    public void Initialize(RectTransform parentRectTransform, Camera camera, MobStatus status)
    {
        _parentRectTransform = parentRectTransform;
        _camera = camera;
        _status = status;
    }

    //ゲージ更新
    private void Refresh()
    {
        fillImage.fillAmount = _status.Life / _status.LifeMax; //残りライフ表示

        //対象Mobの場所にゲージを移動する
        Vector3 screenPoint = _camera.WorldToScreenPoint(_status.transform.position);
        Vector2 localPoint;
        //スクリーン座標をローカル座標へ変換するメソッド
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_parentRectTransform, screenPoint, null, out localPoint);
        transform.localPosition = localPoint + new Vector2(0, 80); //位置調整
    }
}
