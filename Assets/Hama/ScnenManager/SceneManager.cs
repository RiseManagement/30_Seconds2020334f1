using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    static public SceneManager instance;

    static string scenename;
    static string oldscenename;
    public static string OldSceneName
    {
        get { return oldscenename; }
    }
    public enum SceneName
    {
        TITLE,
        INTERVAL,
        STORY,
        CLEAR,
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
        oldscenename = scenename;     
    }

    // Update is called once per frame
    void Update()
    {
        scenename = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        Debug.Log(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// シーン遷移
    /// </summary>
    /// <param name="scene">列挙型のシーン名</param>
    public static void SceneLaod(SceneName scene)
    {
        oldscenename = scenename;
        string sceneName = scene.ToString().ToLower();

        if (!UnityEngine.SceneManagement.SceneManager.GetSceneByName(sceneName).IsValid())
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
    }
}
