using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraMove : MonoBehaviour
{
    public Transform finalCameraPos;
    // Update is called once per frame


    public async void MoveCamera()
    {
        float time = 0;
        float travelTime = 2f;
        Vector3 startPosition = transform.position;

        while (time < travelTime)
        {
            transform.position = Vector3.Lerp(startPosition, finalCameraPos.transform.position, time/travelTime);
            time += Time.deltaTime;
        }

      
  
       
    }

 
}
