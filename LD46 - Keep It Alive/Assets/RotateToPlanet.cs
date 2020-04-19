using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToPlanet : MonoBehaviour
{

    public GameObject planet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target = transform.position - planet.transform.position;
        target.Normalize();
        //Rotate to face planet
        transform.SetPositionAndRotation(
            transform.position,
            Quaternion.FromToRotation(transform.up, target) * transform.rotation
            );
    }
}
