using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Seed : MonoBehaviour
{
    public Material[] treeColor;
    public GameObject Leaves;


    //public int type; // Type of plant, to be looked up from a table?
    public Transform planet;//planet duh...
    public Slider healthBar;
    public Slider waterBar;
    public Slider sunBar;

    GameObject player;
    public GameObject DirLight;
    Vector3 dirLightDir;


    public bool randomized; //Set all public vars below to random range for a give type.

    public int type; //Type of tree
    public float germWaterReq; //How much water till sprout
    public float thirstRate; //How  quickly does it get thirsty (percent per second)
    public float sunlightReq; //How long does sunlight bar drop  (percent per second)
    public float gestationPeriod; //How long till seeds/fruit are ripe (seconds)
    public float damageRate; //how much damage per second?  (dmg per second)
    public int maxGrowths;//When is too big? (Number of growth spurts b4 adulthood)

    float health     = 100; //Total health - drops when something is drained, heals only when all is good
    float water      =   0;//amount of water          0-100
    float sun        =   0;//amount sun               0-100
    float size       =   0; //Amount of growth        0-100
    
    float gestationTimer = 0;  //How long since pollination, checked against gestationPeriod
    float growthPeriod = 5; //How long till growths (seconds)
    float GrowthTimer = 0; //How long with water between growths 

    bool  polinated  = false; //Check if polinated
    bool  germinated = false; //Is germinated?
    bool  adult      = false; //Is done growing?

    bool touchingLantern = false;//lantern uses triggers instead of raycasts, so much be checked separate before removing sun...


    BoxCollider meshTrigger;
    //Meshy Components
    MeshRenderer MR;
    MeshFilter MF;
   
    Mesh mesh;
    List<Vector3> Verts;
    List<int> Tris;
    void Start()
    {
        meshTrigger = gameObject.AddComponent<BoxCollider>();
        meshTrigger.isTrigger = true;
        

        player = GameObject.FindGameObjectWithTag("Player");
        Physics.IgnoreCollision(player.GetComponent<Collider>(), gameObject.GetComponent<Collider>());
        Verts = new List<Vector3>();
        Tris = new List<int>();
        MR = gameObject.GetComponent<MeshRenderer>();
        MF = gameObject.GetComponent<MeshFilter>();
        
        if (randomized) randomizeAll();

        float x;
        float y;
        float z;

        y = Mathf.Sin(DirLight.transform.rotation.x);
        x = Mathf.Sin(DirLight.transform.rotation.y) * Mathf.Cos(DirLight.transform.rotation.x);
        z = Mathf.Cos(DirLight.transform.rotation.y) * Mathf.Cos(DirLight.transform.rotation.x);
        dirLightDir = new Vector3(x, y, z);
    }

  
    void Update()
    {
        CheckGerminate(); //good
        CheckGrowth();    //good
        CheckWater();     //good
        CheckSun();       //good
        CheckPollination();  //NOPE
        UpdateBars();    //good
        UpdateTrigger(); //good

        FacePlanet(); //good
    }

    void UpdateTrigger()
    {
        //Trigger surrounding mesh to see where seeds can be planted.
        if (mesh != null)
        {
            meshTrigger.size = mesh.bounds.size;
            meshTrigger.center = mesh.bounds.center;
        }
    }
    void UpdateBars()
    {
        healthBar.value = health / 100;
        waterBar.value =  water / 100;
        sunBar.value = sun / 100;

        if (health <= 0)
        {
            GameObject.Destroy(gameObject);
        }
        if(water>75 && sun > 75)
        {
            health += Time.deltaTime * damageRate/2;
            if (health > 100) health = 100;
        }
    }
    void CheckPollination()
    {

        if (!polinated)
        {




        }

        if (polinated)
        {
            gestationTimer += Time.deltaTime;
            if (gestationTimer >= gestationPeriod)
            {
                //POLINATION & SEEDMAKING



                polinated = false;
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
            //Use collider for point lights... raycast for directional
            if(!Physics.Raycast(transform.position + transform.up*5, dirLightDir) || touchingLantern)
            {
                sun += Time.deltaTime * 20;
                if (sun > 100) sun = 100;
            }
            else
            {
                
                if (sun >= sunlightReq * Time.deltaTime)
                {
                    sun -= sunlightReq * Time.deltaTime;
                }
                else
                {
                    health -= damageRate * Time.deltaTime;
                    sun = 0;
                }
            }
            
        }
    }
    void CheckGrowth()
    {
        if (size >= maxGrowths)
        {
            size = maxGrowths;
            adult = true;
        }
       
        if (!adult && water>thirstRate && sun>sunlightReq && germinated)
        {
            if (GrowthTimer >= growthPeriod)
            {
                GrowthTimer = 0;
                Grow();
            }
            else
            {
                GrowthTimer += Time.deltaTime;
            }
        }
    }
    void Grow()
    {
        /*GROW SHIT --- YIKES --- another all nighter to get this done?
        //--------------

        //Steps. ? How do I want to grow these. WHAT ARE THESE?
        //Are they trees, bushes, flowers, a random one of those? 
        //what am i making, first?

        //Trees, seem easy enough, branch outward semi-randomly

        //Bushes, small trees with colored planes randomly thrown in. not easy to deal with imo

        //flowers, meh, i'd rather have something that grows a lot.

        //I think imma go with trees.


        //Trees need leaves tho... halp.

        //maybe leaves can be a bunch of random tris made out of a series of random points within a leafy range?
        //might just look buggy tho

        //2-3 branches at a time. all new verts can just be old verts moved around, like moving the rings not the individual verts.

        //btw, 1,3,5 are the top ring front left, right, back
        */

        //STEP 1 adding new verts.

        //keep track of init size, last three verts will always be top ring except for first time.

        List<Vector3> tempVerts = new List<Vector3>();
        float xs = transform.localScale.x;
        float ys = transform.localScale.y;
        float zs = transform.localScale.z;
        int scale = 1;
        if (size != 0)
        {
            for (int i = 0; i < 2; i++)
            {
                int dir = 0;
                if (i == 0) dir = -scale;
                if (i == 1) dir = scale;

               

                float len = 3 * Mathf.Pow(2, size);
                float rHeight = Random.Range(.25f, 1.25f);
                float rZ = Random.Range(.25f, 1.25f);
                float rX = Random.Range(.25f, 1.25f);
                for (int j = Verts.Count - Mathf.RoundToInt(len); j < Verts.Count; j++)
                {
                    if ((Verts.Count - j) % 3 == 0)
                    {
                        rZ = Random.Range(-2.5f, 2.5f);
                        rHeight = Random.Range(.25f, 1.25f);
                        rX = Random.Range(.25f, 1.25f);
                    }
                    tempVerts.Add(
                        new Vector3(
                            Verts[j].x + dir * xs * rX,
                            Verts[j].y + ys*rHeight,
                            Verts[j].z + zs * rZ
                        )
                    );
                }


            }

            int beg = Verts.Count - Mathf.RoundToInt(3 * Mathf.Pow(2, size));
            int end = Verts.Count;
            int baseTris = (end - beg) / 3;

            Verts.AddRange(tempVerts);

            for (int i = 0; i < baseTris; i++)
            {
                makePrism(beg + i * 3, beg + 1 + i * 3, beg + 2 + i * 3, (beg + i * 3) + baseTris * 3, (beg + 1 + i * 3) + baseTris * 3, (beg + 2 + i * 3) + baseTris * 3);
                makePrism(beg + i * 3, beg + 1 + i * 3, beg + 2 + i * 3, (beg + i * 3) + baseTris * 6, (beg + 1 + i * 3) + baseTris * 6, (beg + 2 + i * 3) + baseTris * 6);
            }
            //SYSTEMATICALLY FORM TRIS FUUUUUUUUUUUUCK
        }
        else
        {
            //Wonky ass first layer...
            //top ring is 1 3 5 (l r b )
            for (int i = 0; i < 2; i++)
            {
                int dir = 0;
                if (i == 0) dir = -scale;
                if (i == 1) dir = scale;

                int zdir = Random.Range(-scale, scale);

                float len = 3 * Mathf.Pow(2, size);

                
                    tempVerts.Add(
                            new Vector3(
                                Verts[1].x + dir * xs,
                                Verts[1].y + ys,
                                Verts[1].z + zdir * zs
                            )
                    );

                    tempVerts.Add(
                            new Vector3(
                                Verts[3].x + dir * xs,
                                Verts[3].y + ys,
                                Verts[3].z + zdir * zs
                            )
                        );

                    tempVerts.Add(
                            new Vector3(
                                Verts[5].x + dir * xs,
                                Verts[5].y + ys,
                                Verts[5].z + zdir * zs
                            )
                        );


            }

            

            Verts.AddRange(tempVerts);


            makePrism(1, 3, 5, 6, 7,   8);
            makePrism(1, 3, 5, 9, 10, 11);


            
            
        }


        mesh.Clear();

        mesh.vertices = Verts.ToArray();
        mesh.triangles = Tris.ToArray();
        mesh.RecalculateNormals();
        MF.mesh = mesh;

        size += 1;



        void makePrism(int a, int b, int c,    int x, int y, int z)
        {
            addTri(a, x, b);
            addTri(x, y, b);
            addTri(x, a, c);
            addTri(c, z, x);
            addTri(z, c, b);
            addTri(z, b, y);

            addTri(x, z, y);
        }
        GrowLeaves();
    }
    void GrowLeaves()
    {
        MeshFilter   LMF = Leaves.GetComponent<MeshFilter>();
        MeshRenderer LMR = Leaves.GetComponent<MeshRenderer>();
        Mesh LeafyBois = new Mesh();
        LMR.material = treeColor[type];

        List<Vector3> LVerts = new List<Vector3>();
        List<int> LTris = new List<int>();
        float leafSize = .5f;
        for (int i = 0; i < size * 150; i++)
        {
            Vector3 bl = new Vector3(Random.Range(mesh.bounds.min.x, mesh.bounds.max.x), Random.Range(mesh.bounds.min.y+1.25f, mesh.bounds.max.y), Random.Range(mesh.bounds.min.z, mesh.bounds.max.z));
            Vector3 br = new Vector3(bl.x + Random.Range(-leafSize, leafSize), bl.y + Random.Range(-leafSize, leafSize), bl.z + Random.Range(-leafSize, leafSize));
            Vector3 tl = new Vector3(br.x + Random.Range(-leafSize, leafSize), br.y + Random.Range(-leafSize, leafSize), br.z + Random.Range(-leafSize, leafSize));
            Vector3 tr = new Vector3(tl.x + Random.Range(-leafSize, leafSize), tl.y + Random.Range(-leafSize, leafSize), tl.z + Random.Range(-leafSize, leafSize));

            LVerts.Add(bl); LVerts.Add(br); LVerts.Add(tl); LVerts.Add(tr);
            LTris.Add(0+4*i); LTris.Add(1+4*i); LTris.Add(2+4*i); LTris.Add(2 + 4 *i); LTris.Add(1 + 4 *i); LTris.Add(3 + 4 *i);
        }
        LeafyBois.vertices = LVerts.ToArray();
        LeafyBois.triangles = LTris.ToArray();
        LMF.mesh = LeafyBois;
        LeafyBois.RecalculateNormals();
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
            float ys = transform.localScale.y*2;
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
            addTri(5, 2, 3);
            //TOP - OPT?
            addTri(1, 5, 3);
          


            mesh.Clear();
           
            
            mesh.vertices = Verts.ToArray();
            mesh.triangles = Tris.ToArray();
            mesh.RecalculateNormals();
            MF.mesh = mesh;
           // MR.material = treeColor[type];
            germinated = true;
        }
    }
    void randomizeAll()
    {
        //Fix Values
        germWaterReq    = Random.Range( 5, 15);
        thirstRate      = Random.Range(1f,  4);
        sunlightReq     = Random.Range(1f,  4);
        gestationPeriod = Random.Range( 10, 25);
        damageRate      = Random.Range(  4, 10);
        maxGrowths      = Random.Range(  2,  4);
        
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
       

        if (other.CompareTag("Sun"))
        {
            touchingLantern = true;
        }
    }

    public void increaseWater()
    {
        water += 20 * Time.deltaTime;
        if (water > 100) water = 100;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Sun"))
        {
            touchingLantern = false;
        }
    }

}
