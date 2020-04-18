using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderFaceCamera : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {




        transform.LookAt(Camera.main.transform.position);


       

        //transform.SetPositionAndRotation(transform.position, Quaternion.FromToRotation(transform.up, Vector3.up) * transform.rotation);

    }
}
