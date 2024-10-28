using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMelee : MonoBehaviour
{

    public Transform cam;
    public LayerMask enemy;

    int meleeDamage = 50;
    bool meleeCooldown = false;
    float meleeCooldownTime = 1.2f;


    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0) && !PlayerControl.equip && !meleeCooldown) //attack animation here
        {
            //if (Physics.SphereCast(cam.position, 1f, cam.forward, out RaycastHit hit, 2f))
            {
               // if (hit.collider.gameObject.CompareTag("Enemy"))
                {
                  //  EnemyControl health = hit.transform.GetComponent<EnemyControl>();
                    //health.damageTaken(meleeDamage);
                   // meleeCooldown = true;
                }
            }
        }

        if (meleeCooldown)
            Invoke(nameof(MeleeCooldown), meleeCooldownTime);
    }

    void MeleeCooldown()
    {
        meleeCooldown = false;
    }
}
