using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Item : MonoBehaviour
{
    public enum ItemType //�A�C�e���̎�ޒ�`
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
        //�o���A�j���[�V����
        Collider colliderCache = this.GetComponent<Collider>(); //�R���_�[���擾�i�A�j���V�������I���܂Ŗ��������邽�߁j
        colliderCache.enabled = false;

        Transform transformCache = this.transform; //�g�����X�t�H�[�����擾
        Vector3 dropPosition = transform.localPosition + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)); //�o���������
        transformCache.DOLocalMove(dropPosition, 0.5f); //�o���A�j���[�V����(�ړ�)

        Vector3 defaultScale = transformCache.localScale; //���̃A�C�e���̃X�P�[�����擾
        transformCache.localScale = Vector3.zero; //�[���ɂ���
        transformCache.DOScale(defaultScale, 0.5f) //�o���A�j���[�V����(���剻)
            .SetEase(Ease.OutBounce) //�o�E���h���̃C�[�W���O��t�^
            .OnComplete(() =>
            {
                colliderCache.enabled = true; //�A�j���[�V��������������΃R���_�[��L����
            });
    }

    private void OnTriggerEnter(Collider other) //�ڐG��
    {
        if (!other.CompareTag("Player")) return; //�v���C���[�ȊO���ڐG�����ꍇ�A����
        OwnedItemsData.Instance.Add(type);
        OwnedItemsData.Instance.Save();
        foreach (var item in OwnedItemsData.Instance.OwnedItems)
        {
            Debug.Log(item.Type + "��" + item.Number + "����");
        }

        Destroy(gameObject);
    }
}
