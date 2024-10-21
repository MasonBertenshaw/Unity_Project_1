using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public GameObject swing;
    public GameObject shot;
    public float shotVel = 0;
    public int fireMode = 0;
    public float currentClip = 0;
    public int clipSize = 0;
    public float fireRate = 0;
    public int weaponid = -1;
    public float maxAmmo = 0;
    public float currentAmmo = 0;
    public float minAmmo = 0;
    public bool canFire = true;
    public int reloadAmount = 0;
    public float bulletLifeSpan = 0;
    public float reloadtimer = 2f;

    public Transform Weapon_Slot;
    private Camera playercam;

    // Start is called before the first frame update
    void Start()
    {
        PlayerControl playerControl = GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && canFire && currentClip > 0 && weaponid >= 0)
        {
            GameObject s = Instantiate(shot, Weapon_Slot.position, Weapon_Slot.rotation);
            s.GetComponent<Rigidbody>().AddForce(playercam.transform.forward * shotVel);
            Destroy(s, bulletLifeSpan);

            canFire = false;
            currentClip--;
            StartCoroutine("cooldownFire");
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CRWeapons" || other.gameObject.tag == "LRWeapons")
        {
            other.gameObject.transform.SetPositionAndRotation(Weapon_Slot.position, Weapon_Slot.rotation);
            other.gameObject.transform.SetParent(Weapon_Slot);
            switch (other.gameObject.name)
            {
                case "SplitDagger":
                    weaponid = 0;
                    shotVel = 0;
                    fireMode = 0;
                    fireRate = 3f;
                    currentClip = 1;
                    clipSize = 1;
                    maxAmmo = 9999999999999999999;
                    currentAmmo = 9999999999999999999;
                    reloadAmount = 1;
                    bulletLifeSpan = 1;
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
                    maxAmmo = 9999999999999999999;
                    currentAmmo = 9999999999999999999;
                    reloadAmount = 1;
                    bulletLifeSpan = 1;
                    break;
                
               

            }
        }
    }

    public void reloadclip()
    {
        if (currentClip >= clipSize)
            return;

        else
        {
            float reloadCount = clipSize - currentClip;

            if (currentAmmo < reloadCount)
            {
                currentClip += currentAmmo;
                currentAmmo = 0;
                return;
            }

            else
            {
                currentClip += reloadCount;
                currentAmmo -= reloadCount;
                return;
            }
        }

    }

    IEnumerator cooldownFire()
    {
        yield return new WaitForSeconds(fireRate);
        canFire = true;

    }

}
