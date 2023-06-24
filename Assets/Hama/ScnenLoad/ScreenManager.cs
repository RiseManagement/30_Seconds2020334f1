using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSceneA()
    {
        SceneManager.LoadScene("maingame_A1");
    }

    public void ChangeSceneB()
    {
        SceneManager.LoadScene("maingame_B1");
    }

    
}
