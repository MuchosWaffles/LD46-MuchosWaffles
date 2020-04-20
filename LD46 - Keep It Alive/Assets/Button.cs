using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Button : MonoBehaviour
{

    public string Next;
    public void next()
    {
        SceneManager.LoadScene(Next);
    }
}
