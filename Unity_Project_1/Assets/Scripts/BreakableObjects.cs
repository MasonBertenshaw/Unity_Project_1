   using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class BreakableObjects : MonoBehaviour
{
    public int maxHealth = 10;
    public int health = 10;

    // Start is called before the first frame update
    void Start()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
