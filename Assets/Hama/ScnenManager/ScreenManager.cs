using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
    static SceneName scene;
    public enum SceneName
    {
        TITLE,
        INTERVAL,
        STORY,
        CLEAR,
        MAINGAME_A,
        MAINGAME_B,
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);       
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// シーン遷移
    /// </summary>
    /// <param name="scene">列挙型のシーン名</param>
    public static void SceneLaod(SceneName scene)
    {
        string sceneName = scene.ToString().ToLower();

        if (!SceneManager.GetSceneByName(sceneName).IsValid())
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
