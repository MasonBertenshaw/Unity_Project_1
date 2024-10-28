using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Vector3 = UnityEngine.Vector3;

public class EnemyControl : MonoBehaviour
{
    public PlayerControl healthPoints;
    public PlayerControl player;
    public NavMeshAgent agent;

    [Header("EnemyStats")]
    public int health = 3;
    public int maxhealth = 3;
    public int damageGiven = 5;
    public int damageTaken = 1;
    public float knockbackforce = 500;
    public float maxDistance = 300;

    // Start is called before the first frame update
    void Start()
    { 
        player = GameObject.Find("Player").GetComponent<PlayerControl>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //float distanceToPlayer = Vector3.Distance(transform.position, player.position);
       // if (distanceToPlayer <= maxDistance)
        {
            agent.destination = player.transform.position;
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Shot")
        {
            health -= damageTaken; 
            Destroy(collision.gameObject);
            
        }

    }
}
