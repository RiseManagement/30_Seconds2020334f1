using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ScenedemoManager : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("maingamefirst");
    }
}
