﻿using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
    static public SceneTransitions instance;

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
        MAINGAMEFIRST,
        MAINGAMELAST,
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
        if (MainGameProgress.gameStaus != MainGameProgress.GameStaus.IntervalStart ||
            MainGameProgress.gameStaus != MainGameProgress.GameStaus.IntervalEnd)
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
        string sceneName = scene.ToString();

        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
