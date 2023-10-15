using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager :MonoBehaviour
{
    static public SceneManager instance;

    static string nowscenename;
    public static string NowSceneName
    {
        get
        {
            return nowscenename;
        }
    }
    static string oldscenename;
    public static string OldSceneName
    {
        get
        {
            return oldscenename;
        }
    }
    public enum SceneName
    {
        TITLE,
        INTERVAL,
        STORY,
        ENDING,
        MAINGAME_A,
        MAINGAME_B,
    }


    void Awake()
    {
        if (instance == null)
        {

            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        oldscenename = nowscenename;
    }

    // Update is called once per frame
    void Update()
    {
        nowscenename = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        //Debug.Log("現在のシーン:" + nowscenename);
        // Debug.Log("過去のシーン:" + oldscenename);
    }

    /// <summary>
    /// シーン遷移
    /// </summary>
    /// <param name="scene">列挙型のシーン名</param>
    public static void SceneLaod(SceneName scene)
    {
        oldscenename = nowscenename;
        string sceneName = scene.ToString().ToLower();

        if (!UnityEngine.SceneManagement.SceneManager.GetSceneByName(sceneName).IsValid())
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
    }
}
