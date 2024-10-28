using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;


public class GameManager : MonoBehaviour
{
    public bool isPaused = false;

    public GameObject pauseMenu;
    public PlayerControl playerdata;

    public Image healthBar;
    //public TextMeshPro 


    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex > 0)
        {
            playerdata = GameObject.Find("Player").GetComponent<PlayerControl>();
        }
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex > 0)
        {
            healthBar.fillAmount = Mathf.Clamp((float)playerdata.healthPoints / (float)playerdata.maxHealthPoints, 0, 1);

            if (Input.GetKeyUp(KeyCode.Escape))
            {
                if (!isPaused)
                {
                    pauseMenu.SetActive(true);

                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;


                    isPaused = true;

                    Time.timeScale = 0;
                }
                else
                    Resume();

            }
        }
        
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);

        Time.timeScale = 1;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        isPaused = false;
    }


    public void QuitGame()
    {
        SceneManager.LoadScene(0);
    }

    public void Respawn()
    {
        playerdata.healthPoints = 0;

       
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        isPaused = false;

    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OnCollisionEnter (Collision collision)
    {
        if (collision.gameObject.tag == "ToTheFeilds")
        {
            SceneManager.LoadScene(2);
        }

        if (collision.gameObject.tag == "ToTheVillage")
        {
            SceneManager.LoadScene(1);
        }

        if (collision.gameObject.tag == "GetOut")
        {
            SceneManager.LoadScene(0);
        }

        if (collision.gameObject.tag == "StartGame")
        {
            SceneManager.LoadScene(1);
        }
    }
}
