using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    //public int type; // Type of plant, to be looked up from a table?
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

    void Start()
    {
        if (randomized) randomizeAll();
    }

  
    void Update()
    {
        CheckGerminate();
        CheckGrowth();
        CheckWater();
        CheckSun();
        CheckPollination();
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
}
