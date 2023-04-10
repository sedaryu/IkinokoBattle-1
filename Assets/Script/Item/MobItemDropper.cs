using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MobStatus))]
public class MobItemDropper : MonoBehaviour
{
    [SerializeField] [Range(0, 1)] private float dropRate = 0.1f; //�A�C�e���o���m��(�f�t�H���g�l��0.1)
    [SerializeField] private Item itemPrefab; //�v���n�u���i�[
    [SerializeField] private int number = 1; //�A�C�e���o����

    private MobStatus _status; //�A�C�e�����h���b�v����L�����̃X�e�[�^�X���擾
    private bool _isDropInvoked;

    // Start is called before the first frame update
    void Start()
    {
        _status = this.GetComponent<MobStatus>(); //�X�e�[�^�X���擾
    }

    // Update is called once per frame
    void Update()
    {
        if (_status.Life <= 0) //���C�t���s�����ꍇ
        {
            DropIfNeeded(); //�h���b�v���菈�������s
        }
    }

    private void DropIfNeeded() //�A�C�e�����L��������h���b�v���邩�ǂ����𔻒肵����
    {
        if (_isDropInvoked) return; //�h���b�v�����ǂ����𔻒�

        _isDropInvoked = true;

        if (Random.Range(0, 1f) >= dropRate) return; //�h���b�v�m������

        for (int i = 0; i < number; i++) //�w�肳�ꂽ���Ԃ񐶐�
        {
            Item item = Instantiate(itemPrefab, this.transform.position, Quaternion.identity); //Item�X�N���v�g��Ԃ�
            item.Initialize(); //�o���A�j���[�V���������s
        }
    }
}
