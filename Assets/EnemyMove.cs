using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Debug = UnityEngine.Debug;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyStatus))]
public class EnemyMove : MonoBehaviour
{
    [SerializeField] private LayerMask raycastLayerMask;

    private NavMeshAgent _agent; //追跡用AI
    private RaycastHit[] raycastHits = new RaycastHit[10]; //追跡用Ray
    private EnemyStatus _status;

    public UnityEvent attackPlayer;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _status = GetComponent<EnemyStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //CollisionDetecter内に侵入したプレイヤーを追跡する
    public void OnDetectObject(Collider collider)
    {
        if (!_status.IsMovable) //MobStatusの_statusがNormal以外の場合
        {
            _agent.isStopped = true;
            return;
        }

        if (collider.CompareTag("Player"))
        {
            Vector3 positionDiff = collider.transform.position - this.transform.position;
            float distance = positionDiff.magnitude; //敵とプレーヤーの距離
            Vector3 direction = positionDiff.normalized; //敵からプレイヤーへの方向

            int hitcount = Physics.RaycastNonAlloc(this.transform.position, direction, raycastHits, distance, raycastLayerMask);

            Debug.Log("hitcount:" + hitcount);

            if (hitcount == 0)
            {
                _agent.isStopped = false;
                _agent.destination = collider.transform.position;
            }
            else
            {
                _agent.isStopped = true;
            }

            //if (Physics.Raycast(this.transform.position, direction, out RaycastHit hit, distance))
            //{
            //    if (hit.collider.CompareTag("Player"))
            //    {
            //        Debug.Log("PlayerHIT");
            //        agent.isStopped = false;
            //        agent.destination = collider.transform.position;
            //    }
            //    else
            //    {
            //        Debug.Log("HIT");
            //        agent.isStopped = true;
            //    }
            //}
        }

    }
}
