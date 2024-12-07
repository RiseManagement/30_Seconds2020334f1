using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    private AudioSource audioSource;
    private AudioSource bgmSource;

    public AudioClip mainMenuBGM; // スタートシーンとタイトルシーンのBGM
    public AudioClip gameBGM;     // ゲームシーンのBGM

    void Awake()
    {
        // シングルトンパターンを実装
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            // BGM用のAudioSourceを追加
            bgmSource = gameObject.AddComponent<AudioSource>();
            bgmSource.loop = true;

            // シーンがロードされたときにBGMを切り替える
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing on SoundManager");
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ChangeBGM(scene.name);
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    public void PlayBGM(AudioClip bgm)
    {
        if (bgm != null && bgmSource != null)
        {
            bgmSource.clip = bgm;
            bgmSource.Play();
        }
    }

    public void StopBGM()
    {
        if (bgmSource != null && bgmSource.isPlaying)
        {
            bgmSource.Stop();
        }
    }

    public void ChangeBGM(string sceneName)
    {
        if (sceneName == "Start" || sceneName == "title")
        {
            if (bgmSource.clip != mainMenuBGM) // BGMが変更されていない場合のみ再生
            {
                PlayBGM(mainMenuBGM);
            }
        }
        else if (sceneName == "maingamefirst" || sceneName == "maingamelast")
        {
            if (bgmSource.clip != gameBGM) // BGMが変更されていない場合のみ再生
            {
                PlayBGM(gameBGM);
            }
        }
    }
}
