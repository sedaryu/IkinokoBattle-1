using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyMove : MonoBehaviour
{
    private PlayerController player;
    private NavMeshAgent agent;

    public UnityEvent attackPlayer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Query-Chan-SD").GetComponent<PlayerController>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = player.transform.position;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            attackPlayer.Invoke();
        }
    }
}
