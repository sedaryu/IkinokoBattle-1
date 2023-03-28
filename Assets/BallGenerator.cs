using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGenerator : MonoBehaviour
{
    private IEnumerator coroutine;

    [SerializeField] private GameObject ballPrefab;

    void Start()
    {
        coroutine = SpawnObjects();
        StartCoroutine(coroutine);
    }

    IEnumerator SpawnObjects()
    {
        while (true)
        {
            float x = transform.position.x + Random.Range(-0.25f, 0.25f);
            float z = transform.position.z + Random.Range(-0.25f, 0.25f);
            // �I�u�W�F�N�g�𐶐�
            Instantiate(ballPrefab, new Vector3(x, transform.position.y, z), Quaternion.identity);
            // 1�b�ҋ@
            yield return new WaitForSeconds(1);
        }
    }

    void StopCoroutine()
    {
        StopCoroutine(coroutine);
    }
}
