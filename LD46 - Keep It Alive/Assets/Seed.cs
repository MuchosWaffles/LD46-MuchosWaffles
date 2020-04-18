﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    //public int type; // Type of plant, to be looked up from a table?
    public Transform planet;//planet duh...


    public bool randomized; //Set all public vars below to random range for a give type.

    public float germWaterReq; //How much water till sprout
    public float thirstRate; //How  quickly does it get thirsty (percent per second)
    public float sunlightReq; //How long does sunlight bar drop  (percent per second)
    public float gestationPeriod; //How long till seeds/fruit are ripe (seconds)
    public float damageRate; //how much damage per second?  (dmg per second)

    float health     = 100; //Total health - drops when something is drained, heals only when all is good
    float water      =   0;//amount of water          0-100
    float sun        =   0;//amount sun               0-100
    float size       =   0; //Amount of growth        0-100
    
    float gestationTimer = 0;  //How long since pollination, checked against gestationPeriod

    bool  polinated  = false; //Check if polinated
    bool  germinated = false; //Is germinated?
    bool  adult      = false; //Is done growing?

    //Meshy Components
    MeshRenderer MR;
    MeshFilter MF;
   
    Mesh mesh;
    List<Vector3> Verts;
    List<int> Tris;
    void Start()
    {
        Verts = new List<Vector3>();
        Tris = new List<int>();
        MR = gameObject.GetComponent<MeshRenderer>();
        MF = gameObject.GetComponent<MeshFilter>();
        
        if (randomized) randomizeAll();
    }

  
    void Update()
    {
        CheckGerminate();
        CheckGrowth();
        CheckWater();
        CheckSun();
        CheckPollination();



        FacePlanet();
    }

    void CheckPollination()
    {
        if (polinated)
        {
            gestationTimer += Time.deltaTime;
            if (gestationTimer >= gestationPeriod)
            {
                //POLINATION & SEEDMAKING
            }

        }
    }
    void CheckWater()
    {
        //HANDLE WATER and shits


        if (germinated)
        {
            if (water >= thirstRate * Time.deltaTime)
            {
                water -= thirstRate * Time.deltaTime;
            }
            else
            {
                health -= damageRate * Time.deltaTime;
                water = 0;
            }
        }



    }
    void CheckSun()
    {
        if (germinated)
        {
            //HANDLE SUN by checking if light is on it (and how much)
        }
    }
    void CheckGrowth()
    {
        if (size >= 100)
        {
            size = 100;
            adult = true;
        }

        if (!adult && water>thirstRate && germinated)
        {
            Grow();
        }
    }
    void Grow()
    {
        //GROW SHIT
        //--------------
        size += 10 * Time.deltaTime;
    }
    void CheckGerminate()
    {
        if (!germinated && water >= germWaterReq)
        {
            //FIRST SPROUT
            //---------------------------
            //initialize mesh
            mesh = new Mesh();

            //init scale dependant vars
            float xs = transform.localScale.x * 0.25f;
            float ys = transform.localScale.y;
            float zs = transform.localScale.z * 0.25f;
            //Set up first verts????? tri prisms? = 6 pts & 6 tris & 3 rects

            //edge 1
            Verts.Add(Vector3.zero);
            Verts.Add(new Vector3(0, ys, 0));

            //Edge2
            Verts.Add(new Vector3(xs, 0, 0)) ;
            Verts.Add(new Vector3(xs, ys, 0));

            //Edge 3
            Verts.Add(new Vector3(xs/2, 0, zs));
            Verts.Add(new Vector3(xs/2, ys, zs));


            //ADDING TRIANGLES
            addTri(0, 1, 2);
            addTri(1, 3, 2);
            addTri(1, 0, 4);
            addTri(4, 5, 1);
            addTri(5, 4, 2);
            //TOP - OPT?
            addTri(5, 2, 3);
            addTri(1, 5, 3);
          


            mesh.Clear();
           
            
            mesh.vertices = Verts.ToArray();
            mesh.triangles = Tris.ToArray();
            mesh.RecalculateNormals();
            MF.mesh = mesh;
          
            germinated = true;
        }
    }
    void randomizeAll()
    {
        germWaterReq    = Random.Range( 10, 30);
        thirstRate      = Random.Range(.5f,  2);
        sunlightReq     = Random.Range(.5f,  2);
        gestationPeriod = Random.Range( 10, 25);
        damageRate      = Random.Range(  1, 10);
    }
    void FacePlanet()
    {
        Vector3 target = transform.position - planet.position;
        target.Normalize();
        //Rotate to face planet
        transform.SetPositionAndRotation(
            transform.position,
            Quaternion.FromToRotation(transform.up, target) * transform.rotation
            );
    }

    void addTri(int a, int b, int c)
    {
        Tris.Add(a); Tris.Add(b); ; Tris.Add(c);
    }



    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            water += 20* Time.deltaTime;
            Debug.Log(water);
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), collision.collider);
        }
    }
}
