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
        WaterSpout.SetActive(false);
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

        // mesh.Rotate(new Vector3(0, Input.GetAxis("Horizontal")* Time.deltaTime * speed*5, 0));
        mesh.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * Time.deltaTime * speed*10, 0));
        /*if (Input.GetAxis("Horizontal") != 0 )
        {
            mesh.rotation =
                 Quaternion.Lerp(mesh.rotation, Quaternion.FromToRotation(
            mesh.forward,
            (Input.GetAxis("Horizontal") * mesh.right)) * mesh.rotation,
            .04f
        );*/


        //MOVEMENT
        //Move position perp to rotation, or tangent to planet, ie, move in transform.right positions instead of Vector3.right... Local vs Global / Subjective v Objective...
        //rb.MovePosition(rb.position + (Input.GetAxis("Horizontal") * speed * transform.right) + (Input.GetAxis("Vertical") * speed * transform.forward));
       rb.velocity = (Input.GetAxis("Horizontal") * speed* mesh.right) + (Input.GetAxis("Vertical") * speed * mesh.forward);
       // rb.velocity =  (Input.GetAxis("Vertical") * speed * mesh.forward);



    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            WaterSpout.SetActive(!WaterSpout.activeInHierarchy);
        }
    }
}
