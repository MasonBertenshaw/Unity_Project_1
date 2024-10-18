using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public void SetParent(Transform p);
    public void game
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if ((tag = "CRWeapons") & transform.parent)
        {


        }
        if ((tag = "LRWeapons") & transform.parent)
        {


        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CRWeapons") or (other.gameObject.tag == "LRWeapons")
        {
            other.gameObject.transform.SetPositionAndRotation(Weapon_Slot.position, Weapon_Slot.rotation);
            other.gameObject.transform.SetParent(Weapon_Slot);
            switch (other.gameObject.name)
            {
                case "BowArrows":
                    weaponid = 0;
                    shotVel = 10000;
                    fireMode = 0;
                    fireRate = 0.25f;
                    currentClip = 1;
                    clipSize = 1;
                    maxAmmo = 25;
                    currentAmmo = 5;
                    reloadAmount = 1;
                    bulletLifeSpan = 30;
                    break;
                default:
                    break;
                case "ArmingSword":
                    weaponid = 1;
                    shotVel = 0;
                    fireMode = 0;
                    fireRate = 1f;
                    currentClip = 1;
                    clipSize = 1;
                    maxAmmo = 99999999999999999999999999;
                    currentAmmo = 99999999999999999999999999;
                    reloadAmount = 1;
                    bulletLifeSpan = 1;
                    break;
                default:
                    break;
                case ""

            }
        }
    }
}
