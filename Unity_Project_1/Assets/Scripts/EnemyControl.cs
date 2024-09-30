using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControl : MonoBehaviour
{

    public PlayerControl player;
    public NavMeshAgent agent;

    [Header("EnemyStats")]
    public int health = 3;
    public int maxhealth = 3;
    public int damageGiven = 5;
    public int damageTaken = 1;
    public float knockbackforce = 5;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerControl>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }

        agent.destination = player.transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Shot")
        {
            Destroy(collision.gameObject);
            health -= damageTaken;
        }

        if (collision.gameObject.tag == "Player")
        {

        }
    }
}
