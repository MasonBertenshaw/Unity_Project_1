using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame 
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "NextScene")
        {
            SceneManager.LoadScene(1);
        }
    }
}
