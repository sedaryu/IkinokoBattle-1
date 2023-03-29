using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyMove : MonoBehaviour
{
    private NavMeshAgent agent; //�ǐ՗pAI

    //private RaycastHit[] raycastHits = new RaycastHit[10]; //

    public UnityEvent attackPlayer;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            attackPlayer.Invoke();
        }
    }

    //CollisionDetecter���ɐN�������v���C���[��ǐՂ���
    public void OnDetectObject(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            Vector3 positionDiff = collider.transform.position - this.transform.position;
            float distance = positionDiff.magnitude; //�G�ƃv���[���[�̋���
            Vector3 direction = positionDiff.normalized; //�G����v���C���[�ւ̕���

            if (Physics.Raycast(this.transform.position, direction, out RaycastHit hit, distance))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    Debug.Log("PlayerHIT");
                    agent.isStopped = false;
                    agent.destination = collider.transform.position;
                }
                else
                {
                    Debug.Log("HIT");
                    agent.isStopped = true;
                }
            }


            //int hitcount = physics.raycastnonalloc(this.transform.position, direction, raycasthits, distance);

            //debug.log("hitcount:" + hitcount);

            //if (hitcount == 0)
            //{
            //    agent.isstopped = false;
            //    agent.destination = collider.transform.position;
            //}
            //else
            //{
            //    agent.isstopped = true;
            //}
        }

    }
}
