using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Inventory : MonoBehaviour
{
    public GameObject seedPreFab; //Seed Prefab to be instantiated
    public Transform placeLocation;//Where to place;
    public GameObject light;
    public Material[] types;//Array of materials
    public Image[] icons; //Inventory Slots
    public Text[] texts; //Icon Text

    
    int selected = 0;//selected rn

    public int[] inventory;//inventory is a list of ints, how many of each obj i have rn. 1 red 2 whites 0 pinks 5 yellows, etc... matches up with Material length and index

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < icons.Length; i++)
        {
            icons[i].material = types[i];

        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckScroll();//Good
        UpdateText(); //Good
        if(Input.GetKeyDown(KeyCode.E))CheckPlacement(); //Nope
    }

    void CheckPlacement()
    {
        //Check if trying to place within bounds of another object
        //Make triggers within bounds of other trees.
        //If not, place a seed, which should have a set type, maybe randomized features? 
        bool canPlace = true;
        Collider[] hits = Physics.OverlapSphere(placeLocation.position, 1);
        if (hits != null)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].CompareTag("Plant"))
                {
                    canPlace = false;
                }
            }
        }

        if (canPlace)
        {
            PlantSeed();
        }
        else
        {


        }
    }

    void PlantSeed()
    {
        if (inventory[selected] > 0)
        {
            GameObject child = GameObject.Instantiate(seedPreFab, placeLocation.position, placeLocation.rotation);
            Seed seedyboi = child.GetComponent<Seed>();
            seedyboi.planet = gameObject.GetComponent<CharController>().planet.transform;
            seedyboi.DirLight = light;
            seedyboi.type = selected;
            inventory[selected] -= 1;
        }
        
    }
    void UpdateText()
    {
        for(int i = 0; i < texts.Length; i++)
        {
            texts[i].text = inventory[i].ToString();
        }
    }
    void CheckScroll()
    {
        

        if (selected + Mathf.RoundToInt(Input.GetAxis("Mouse ScrollWheel") * 7) > -1 && selected + Mathf.RoundToInt(Input.GetAxis("Mouse ScrollWheel") * 7) < icons.Length)
        {
            selected += Mathf.RoundToInt(Input.GetAxis("Mouse ScrollWheel") * 7);
        }
        HighlightSeclected(selected);
    }

    void HighlightSeclected(int s)
    {
        icons[s].rectTransform.localScale = new Vector3(1.5f, 1.5f, 1);
        for(int i =0; i < icons.Length; i++)
        {
            if (i != s) icons[i].rectTransform.localScale = new Vector3(1, 1, 1);
        }
    }
}
