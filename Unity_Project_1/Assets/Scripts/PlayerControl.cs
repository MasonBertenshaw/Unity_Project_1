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
    public int attackSpeed;
    public int weaponid = 0;
    public float maxAmmo = 0;
    public float currentAmmo = 0;
    public float minAmmo = 0;
    public bool canFire = true;
    public int reloadAmount = 0;
   

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

      //if (sprintmode == true) 
      


        if (stamina <= 0)
            sprintmode = false;

     //if (Input.GetKeyDown(KeyCode.Q)
        {
            
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
            yield return new WaitForSeconds(attackSpeed);
            canFire = true;

        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Weapons")
        {
            other.gameObject.transform.SetPositionAndRotation(Weapon_Slot.position, Weapon_Slot.rotation);
            other.gameObject.transform.SetParent(Weapon_Slot);
        }
    }
}

