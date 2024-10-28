using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class PlayerControl : MonoBehaviour
{
    private Rigidbody Player;
    public Camera playercam;
    public PlayerStamina playerStamina;
    

    public Transform cameraHolder;

    Vector2 camrotation;

    public Transform Weapon_Slot;

    public bool sprintmode = false;

    public GameManager gm;

     

    [Header("Movement Settings")]
    public float speed = 10.0f;
    public float sprintmultiplier = 2.5f;
    public float jumpheight = 5.0f;
    public float GroundDetectDistance = 1f;
    bool isMoving;

    [Header("User settings")]
    public bool sprintToggleOption = false;
    public float mouseSensitivity = 2.0f;
    public float xcamsensitivity = 2.0f;
    public float ycansensitivity = 2.0f;
    public float camRotioatonLimit = 90f;


    [Header("Player Stats")]
    public int maxHealthPoints = 100;
    public int healthPoints = 100;
    public int restoredHealthPoints = 10;
    public int maxStamina = 100;
    public int stamina = 100;
    public float staminaDrain = 0.25f;
    public int minStamina = 0;

    [Header("Weapon Stats")]
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

    [Header("Magic Stats")]
    public int maxMana = 100;
    public int mana = 100;
    
 


    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        Player = GetComponent<Rigidbody>();
        playercam = GameObject.Find("Player").GetComponentInChildren<Camera>();
        cameraHolder = transform.GetChild(0);
        

        camrotation = Vector2.zero;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gm.isPaused)
        {
            playercam.transform.position = cameraHolder.position;
        

            camrotation.x += Input.GetAxisRaw("Mouse X") * mouseSensitivity;
            camrotation.y += Input.GetAxisRaw("Mouse Y") * mouseSensitivity;

            camrotation.y = Mathf.Clamp(camrotation.y, -camRotioatonLimit, camRotioatonLimit);
    
            playercam.transform.rotation = Quaternion.Euler(-camrotation.y, camrotation.x, 0);
            transform.localRotation = Quaternion.AngleAxis(camrotation.x, Vector3.up);

            if (Input.GetMouseButton(0) && canFire && currentClip > 0 && weaponid >= 0)
            {
                GameObject s = Instantiate(shot, Weapon_Slot.position, Weapon_Slot.rotation);
                s.GetComponent<Rigidbody>().AddForce(playercam.transform.forward * shotVel);
                Destroy(s, bulletLifeSpan);

                canFire = false;
                currentClip--;
                StartCoroutine("cooldownFire");
            }

            Vector3 temp = Player.velocity;

            float verticalMove = Input.GetAxisRaw("Vertical");
            float horizontalMove = Input.GetAxisRaw("Horizontal");


            if (!sprintToggleOption)
            {
             if (Input.GetKey(KeyCode.LeftShift) && playerStamina.currentStamina > 0)
                    sprintmode = true;

                if (Input.GetKeyUp(KeyCode.LeftShift))
                    sprintmode = false;
            }

            if (sprintToggleOption)
            {
                if (Input.GetKey(KeyCode.LeftShift) && verticalMove > 0 && playerStamina.currentStamina > 0)
                    sprintmode = true;

                // if (Input.GetAxisRaw("Vertical") <= 0)
                //sprintmode = false;
            }

            if (!sprintmode)
                temp.x = verticalMove * speed;

            if (sprintmode)
                temp.x = verticalMove * speed * sprintmultiplier;

            temp.z = horizontalMove * speed;

            if (Input.GetKeyUp(KeyCode.LeftShift))
                sprintmode = false;

            if (Input.GetKeyDown(KeyCode.Space) && Physics.Raycast(transform.position, -transform.up, GroundDetectDistance))
                temp.y = jumpheight;


            Player.velocity = (temp.x * transform.forward) + (temp.z * transform.right) + (temp.y * transform.up);
        }

        if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
            isMoving = true;
        else
            isMoving = false;
        {
            if (sprintmode)
                playerStamina.isSprint = true;
            
            else
                playerStamina.isSprint = false;
        }

        //if (Input.GetKeyDown(KeyCode.Z) && mana >= 50)
       // {

       // }



        
    }


    private void OnCollisionEnter(Collision collision)
    {
        //HealthPickup
        if ((healthPoints < maxHealthPoints) && collision.gameObject.tag == "Health Pickup")
        {
            healthPoints += restoredHealthPoints;

            if (healthPoints > maxHealthPoints)
                healthPoints = maxHealthPoints;

            Destroy(collision.gameObject);
        }
        //AmmoPickup
        if ((currentAmmo < maxAmmo) && collision.gameObject.tag == "Ammo Pickup")
        {
            currentAmmo += reloadAmount;

            if (currentAmmo > maxAmmo)
                currentAmmo = maxAmmo;

            Destroy(collision.gameObject);
        }
        //Damage to player
        if (tag == "Enemy")
        {
            
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



 

