using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoseScript : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        
            if (other.gameObject.CompareTag("Plant") && !other.isTrigger)
            {
                if (gameObject.GetComponentInParent<CharController>().PlayerWater > 0)
                {
                    other.GetComponent<Seed>().increaseWater();
                }
            }
    }
}
