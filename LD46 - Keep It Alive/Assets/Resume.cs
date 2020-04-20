using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resume : MonoBehaviour
{
    public GameObject pausemenu;
    // Start is called before the first frame update
    public void resume()
    {
        Time.timeScale = 1;
        pausemenu.SetActive(false);
    }
}
