using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Inventory : MonoBehaviour
{
    public Material[] types;//Array of materials
    public Image[] icons; //Inventory Slots
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
        CheckScroll();
       
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
