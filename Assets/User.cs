using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class User : MonoBehaviour
{
    string name;
    string Scenename;
    // Start is called before the first frame update
    void Start()
    {
        Scenename=SceneManager.GetActiveScene().name;
        if(Scenename=="maingame_A")
        {
            Debug.Log("name=PlayerA");
        }
        else if(Scenename=="maingame_B")
        {
            Debug.Log("name=PlayerB");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
