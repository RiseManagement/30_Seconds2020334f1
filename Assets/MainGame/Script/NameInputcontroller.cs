using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NameInputcontroller : MonoBehaviour
{
    public static string player_1name;
    public static string player_2name;
    public GameObject player_1obj;
    public GameObject player_2obj;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetInputPlayerName()
    {
        player_1name=player_1obj.transform.GetChild(2).GetComponent<Text>().text;
        player_2name=player_2obj.transform.GetChild(2).GetComponent<Text>().text;
    }

    public void LoadSceneMainGame()
    {
        SetInputPlayerName();

        SceneTransitions.SceneLaod(SceneTransitions.SceneName.MAINGAMEFIRST);
        //UnityEngine.SceneManagement.SceneManager.LoadScene("maingamefirst");
    }

}
