using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TapHandler : MonoBehaviour
{
    public AudioClip genericTapSound; // 汎用タップSE
    public AudioClip startTapSound;   // スタート画面専用タップSE
    private Blinker blinker;

    void Start()
    {
        blinker = FindObjectOfType<Blinker>(); // Blinkerコンポーネントを取得
        SoundManager.instance.ChangeBGM(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name); // 現在のシーンに基づいてBGMを再生
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 左クリックまたはタップ
        {
            StartCoroutine(HandleTap());
        }
    }

    IEnumerator HandleTap()
    {
        blinker.StopBlinking(); // Blinkerの点滅を停止
        blinker.StartCoroutine(blinker.FadeOut()); // Blinkerのフェードアウトを開始
        yield return PlayTapSound(); // タップ時のSEを再生
        yield return ChangeScene();
    }

    IEnumerator PlayTapSound()
    {
        string currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        AudioClip clipToPlay = null;

        if (currentSceneName == "Start") // スタート画面のシーン名を指定
        {
            clipToPlay = startTapSound;
        }
        else
        {
            clipToPlay = genericTapSound;
        }

        if (clipToPlay != null)
        {
            SoundManager.instance.PlaySound(clipToPlay);
            yield return new WaitForSeconds(clipToPlay.length); // サウンドの再生が終わるのを待つ
        }
    }

    IEnumerator ChangeScene()
    {
        // 現在のシーン名に基づいて次のシーンを決定
        string nextScene = "";
        string currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        if (currentSceneName == "Start")
        {
            nextScene = "title";
        }
        //else if (currentSceneName == "title")
        //{
        //    nextScene = "maingamefirst";
        //}
        //else if (currentSceneName == "maingamefirst")
        //{
        //    nextScene = "maingamelast";
        //}
        //else if (currentSceneName == "maingamelast")
        //{
        //    nextScene = "maingamefirst";
        //}

        // 非同期でシーンをロード
        AsyncOperation asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(nextScene);

        // シーンのロードが完了するまで待つ
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // BGMの切り替えはSoundManagerのOnSceneLoadedで行われるため、ここでは不要
    }
}
