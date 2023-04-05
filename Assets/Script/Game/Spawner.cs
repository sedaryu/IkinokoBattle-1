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

    //エネミーを一定間隔で出現させるコルーチン
    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            SpawnFromPlayer(); //プレイヤーの周辺からエネミーを生成

            SpawnFromField();

            yield return new WaitForSeconds(10); //10秒待機

            if (playerStatus.Life <= 0)
            {
                break;
            }
        }
    }

    //プレイヤーの周辺からエネミーを生成するメソッド
    private void SpawnFromPlayer()
    {
        Vector3 distanceVector = new Vector3(20, 0, 0); //プレイヤーと生成地点の間の距離
        Vector3 spawnPositionFromPlayer = Quaternion.Euler(0, Random.Range(0, 360), 0) * distanceVector; //360度ランダムな場所から出現
        Vector3 spawnPosition = playerStatus.transform.position + spawnPositionFromPlayer; //生成地点を決定

        NavMeshHit navMeshHit;
        if (NavMesh.SamplePosition(spawnPosition, out navMeshHit, distanceVector.magnitude, NavMesh.AllAreas)) //NavMesh的に生成可能な場所を選定
        {
            Instantiate(enemyPrefab, navMeshHit.position, Quaternion.identity); //エネミーオブジェクトを生成
        }
    }

    //フィールドの指定した地点からエネミーを生成するメソッド
    private void SpawnFromField()
    {
        //エネミーオブジェクトを生成
        Instantiate(enemyPrefab, new Vector3(Random.Range(-19.0f, 19.0f), 0, Random.Range(-19.0f, 19.0f)), Quaternion.Euler(0, Random.Range(0, 360), 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
