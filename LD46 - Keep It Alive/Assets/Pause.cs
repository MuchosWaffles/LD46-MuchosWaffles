using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public Canvas pauseMenu;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            pauseMenu.gameObject.SetActive( !pauseMenu.gameObject.activeSelf);
            if (pauseMenu.gameObject.activeSelf) Time.timeScale = 0; else Time.timeScale = 1;
        }
    }
}
