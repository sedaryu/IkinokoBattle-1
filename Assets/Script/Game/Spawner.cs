using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    [SerializeField] private PlayerStatus playerStatus;
    [SerializeField] private GameObject enemyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnLoop()) ;
    }

    //�G�l�~�[�����Ԋu�ŏo��������R���[�`��
    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            SpawnFromPlayer(); //�v���C���[�̎��ӂ���G�l�~�[�𐶐�

            SpawnFromField();

            yield return new WaitForSeconds(10); //10�b�ҋ@

            if (playerStatus.Life <= 0)
            {
                break;
            }
        }
    }

    //�v���C���[�̎��ӂ���G�l�~�[�𐶐����郁�\�b�h
    private void SpawnFromPlayer()
    {
        Vector3 distanceVector = new Vector3(20, 0, 0); //�v���C���[�Ɛ����n�_�̊Ԃ̋���
        Vector3 spawnPositionFromPlayer = Quaternion.Euler(0, Random.Range(0, 360), 0) * distanceVector; //360�x�����_���ȏꏊ����o��
        Vector3 spawnPosition = playerStatus.transform.position + spawnPositionFromPlayer; //�����n�_������

        NavMeshHit navMeshHit;
        if (NavMesh.SamplePosition(spawnPosition, out navMeshHit, distanceVector.magnitude, NavMesh.AllAreas)) //NavMesh�I�ɐ����\�ȏꏊ��I��
        {
            Instantiate(enemyPrefab, navMeshHit.position, Quaternion.identity); //�G�l�~�[�I�u�W�F�N�g�𐶐�
        }
    }

    //�t�B�[���h�̎w�肵���n�_����G�l�~�[�𐶐����郁�\�b�h
    private void SpawnFromField()
    {
        //�G�l�~�[�I�u�W�F�N�g�𐶐�
        Instantiate(enemyPrefab, new Vector3(Random.Range(-19.0f, 19.0f), 0, Random.Range(-19.0f, 19.0f)), Quaternion.Euler(0, Random.Range(0, 360), 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
