using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    public GameObject WaterSpout;
    Rigidbody rb;
    public Transform mesh;
    public Rigidbody planet;
   
    public float grav = -10;
    public float speed;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.useGravity = false;
       
    }

   
    void FixedUpdate()
    {
        //GRAVITY & ROTATION
        //Point from planet to rb
        Vector3 target = rb.position - planet.position;
        target.Normalize();
        //Rotate to face planet
        rb.MoveRotation(Quaternion.FromToRotation(transform.up, target) * rb.rotation);
        //apply gravity
        rb.AddForce(target * grav);

        //ROTATION
        //rotate in movement dir
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            mesh.rotation =
                 Quaternion.Lerp(mesh.rotation, Quaternion.FromToRotation(
            mesh.forward,
            ((Input.GetAxis("Horizontal") * transform.right) + (Input.GetAxis("Vertical") * transform.forward)).normalized) * mesh.rotation,
            .15f
        );
            mesh.SetPositionAndRotation(mesh.position, Quaternion.FromToRotation(mesh.up,transform.up)*mesh.rotation);
        }

       
        //MOVEMENT
        //Move position perp to rotation, or tangent to planet, ie, move in transform.right positions instead of Vector3.right... Local vs Global / Subjective v Objective...
        rb.MovePosition(rb.position + (Input.GetAxis("Horizontal") * speed * transform.right) + (Input.GetAxis("Vertical") * speed * transform.forward));


      
    }

     void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            WaterSpout.SetActive(!WaterSpout.activeInHierarchy);
        }
    }
}
