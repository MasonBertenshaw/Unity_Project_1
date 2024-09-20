using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private Rigidbody Player;
    private Camera playercam;

    Vector2 camrotation;

    public Transform Weapon_Slot;

    public bool sprintmode = false;

    [Header("Movement Settings")]
    public float speed = 10.0f;
    public float sprintmultiplier = 2.5f;
    public float jumpheight = 5.0f;
    public float GroundDetectDistance = 1f;

    [Header("User settings")]
    public bool sprintToggleOption = false;
    public float mouseSensitivity = 2.0f;
    public float xcamsensitivity = 2.0f;
    public float ycansensitivity = 2.0f;
    public float camRotioatonLimit = 90f;


    [Header("Player Stats")]
    public int maxHealthPoints = 100;
    public int healthPoints = 50;
    public int restoredHealthPoints = 10;
    public int maxStamina = 100;
    public int stamina = 100;

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

    [Header("Magic Stats")]
    public int Mana;
 


    // Start is called before the first frame update
    void Start()
    {
        Player = GetComponent<Rigidbody>();
        playercam = transform.GetChild(0).GetComponent<Camera>();

        camrotation = Vector2.zero;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        camrotation.x += Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        camrotation.y += Input.GetAxisRaw("Mouse Y") * mouseSensitivity;

        camrotation.y = Mathf.Clamp(camrotation.y, -camRotioatonLimit, camRotioatonLimit);

        playercam.transform.localRotation = Quaternion.AngleAxis(camrotation.y, Vector3.left);
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
            if (Input.GetKey(KeyCode.LeftShift))
                sprintmode = true;

            if (Input.GetKeyUp(KeyCode.LeftShift))
                sprintmode = false;
        }

        if (sprintToggleOption)
        {
            if (Input.GetKey(KeyCode.LeftShift) && verticalMove > 0)
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Weapons")
        {
            other.gameObject.transform.SetPositionAndRotation(Weapon_Slot.position, Weapon_Slot.rotation);
            other.gameObject.transform.SetParent(Weapon_Slot);
            switch (other.gameObject.name)
            {
                case "weapon1":
                    weaponid = 0;
                    shotVel = 10000;
                    fireMode = 0;
                    fireRate = 0.25f;
                    currentClip = 20;
                    clipSize = 20;
                    maxAmmo = 400;
                    currentAmmo = 200;
                    reloadAmount = 20;
                    bulletLifeSpan = 1;
                    break;
                default:
                    break;
            }
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if ((healthPoints < maxHealthPoints) && collision.gameObject.tag == "Health Pickup")
        {
            healthPoints += restoredHealthPoints;

            if (healthPoints > maxHealthPoints)
                healthPoints = maxHealthPoints;

            Destroy(collision.gameObject);
        }

        if ((currentAmmo < maxAmmo) && collision.gameObject.tag == "Ammo Pickup")
        {
            currentAmmo += reloadAmount;

            if (currentAmmo > maxAmmo)
                currentAmmo = maxAmmo;

            Destroy(collision.gameObject);
        }

        IEnumerator cooldownFIre()
        {
            yield return new WaitForSeconds(fireRate);
            canFire = true;

        }


    }
}


 

