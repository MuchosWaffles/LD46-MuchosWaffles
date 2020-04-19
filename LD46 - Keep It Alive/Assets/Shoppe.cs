using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoppe : MonoBehaviour
{
    public int fruitTypes =5;
    public GameObject[] orderMenus;
    public Inventory Player;
    public Transform[] Masks;
    int[][] orders;
    
    void Start()
    {
        
        orders = new int[3][];
        for(int i = 0; i<orders.Length; i++)
        {
            orders[i] = new int[fruitTypes];
            newOrder(i);
        }

        
        
    }

    // Update is called once per frame
    void Update()
    {
        ShowOrders();//good
        CheckOrders();//nope
        
    }

    void CheckOrders()
    {
        Collider[] hitinfo0 = Physics.OverlapBox(new Vector3(-6.7f, 23, -8.9f), new Vector3(1, 1, 1));
        int hit = -1;
        for(int i =0; i < hitinfo0.Length; i++)
        {
            if (hitinfo0[i].CompareTag("Player"))
            {
                hit = 0;
                break;
            }
        }

        if (hit == -1)
        {
            Collider[] hitinfo1 = Physics.OverlapBox(new Vector3(-4.3f, 24, -6.4f), new Vector3(1, 1, 1));

            for (int i = 0; i < hitinfo1.Length; i++)
            {
                if (hitinfo1[i].CompareTag("Player"))
                {
                    hit = 1;
                    break;
                }
            }
        }

        if (hit == -1)
        {
            Collider[] hitinfo2 = Physics.OverlapBox(new Vector3(-2,25,-4), new Vector3(1, 1, 1));

            for (int i = 0; i < hitinfo2.Length; i++)
            {
                if (hitinfo2[i].CompareTag("Player"))
                {
                    hit = 2;
                    break;
                }
            }
        }

        for(int i =0; i< orders.Length; i++)
        {
            orderMenus[i].transform.localScale = new Vector3(1, 1, 1);
            orderMenus[i].transform.localPosition = new Vector3(0, 0, i * .35f);
        }
        if (hit != -1)
        {
            orderMenus[hit].transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            orderMenus[hit].transform.localPosition = new Vector3(0, 0, .2f + hit*.35f);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (hit != -1)
            {
                //Make Order;
                bool hasEnough = true;
                for(int i = 0; i < orders[hit].Length; i++)
                {
                    
                    if(Player.inventory[i] < orders[hit][i])
                    {
                        hasEnough = false;
                    }

                }
                if (hasEnough)
                {
                    for (int i = 0; i < orders[hit].Length; i++)
                    {

                        Player.inventory[i] -= orders[hit][i];

                    }

                    Player.score += 20;
                    newOrder(hit);
                }
            }
        }
        
    }

    void ShowOrders()
    {
        for (int i = 0; i < orders.Length; i++)
        {
            for (int j = 0; j < orders[i].Length; j++)
            {
                Masks[i*orders[i].Length + j].localScale = new Vector3((float)orders[i][j] *.2f, 1, 1);
            }
        }

        
    }
    void newOrder(int i)
    {
        for (int j = 0; j < orders[i].Length; j++)
        {
            float r = Random.value;
            if (r < .25)
            {
                orders[i][j] = 0;
            }
            else if (r < .5)
            {
                orders[i][j] = 1;
            }
            else if(r<.75)
            {
                orders[i][j] = 3;
            }
            else
            {
                orders[i][j] = 5;
            }
         
        }

    }

}
