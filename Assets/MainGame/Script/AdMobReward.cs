using System;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdMobReward :MonoBehaviour
{

    private RewardedAd rewardedAd;//RewardedAd型の変数 rewardedAdを宣言 この中にリワード広告の情報が入る

    // ハンドラをデリゲートとして保持しておき、Destroy 時に解除する
    private Action onAdOpenedHandler;
    private Action onAdClosedHandler;
    private Action<AdError> onAdFailedHandler;

    private string adUnitId;

    // SDK初期化はアプリ起動中1回でよい (シーン再入時の二重初期化防止)
    private static bool sdkInitialized;

    // ロード失敗時の再試行間隔 (失敗したまま放置すると以後広告が一切出なくなるため)
    private const float RetryDelaySeconds = 30f;

    private void Start()
    {

        // TODO: リリース前に必ず自分のAdMob管理画面の本番広告ユニットIDへ差し替えること
        //       (現在はGoogle公式のテスト用IDのため、本番では収益が発生しない)
#if UNITY_ANDROID
        adUnitId = "ca-app-pub-3940256099942544/5224354917";//ここにAndroidのリワード広告IDを入力
#elif UNITY_IPHONE
        adUnitId = "ca-app-pub-3940256099942544/1712485313";//ここにiOSのリワード広告IDを入力
#else
        adUnitId = "unexpected_platform";
#endif

        // 広告イベントをUnityメインスレッドで発火させる
        // (未設定だとコールバックが別スレッドで呼ばれ、UI操作やUnity API使用時にクラッシュし得る)
        MobileAds.RaiseAdEventsOnUnityMainThread = true;

        if (!sdkInitialized)
        {
            // SDK初期化完了を待ってからロード開始 (初期化前のロードは推奨されない)
            MobileAds.Initialize(initStatus =>
            {
                sdkInitialized = true;
                Debug.Log("AdMob SDK initialized");
                LoadRewardedAd();
            });
        }
        else
        {
            //リワード 読み込み開始
            Debug.Log("Rewarded ad load start");
            LoadRewardedAd();//リワード広告読み込み
        }
    }

    private void OnDestroy()
    {
        // 登録したハンドラを解除してから破棄する
        UnregisterEventHandlers();
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }
    }

    //リワード広告を表示する関数
    public void ShowAdMobReward()
    {
        //変数rewardedAdの中身が存在しており、広告の読み込みが完了していたら広告表示
        if (rewardedAd != null && rewardedAd.CanShowAd() == true)
        {
            //リワード広告 表示を実施　報酬の受け取りの関数GetRewardを引数に設定
            rewardedAd.Show(GetReward);
        }
        else
        {
            //リワード広告読み込み未完了
            Debug.Log("Rewarded ad not loaded");
        }
    }

    //報酬受け取り処理
    private void GetReward(Reward reward)
    {
        //報酬受け取り
        Debug.Log("GetReward");

        //ここに報酬の処理を書く
    }


    //リワード広告を読み込む関数 再読み込みにも使用
    public void LoadRewardedAd()
    {
        //広告の再読み込みのための処理
        //rewardedAdの中身が入っていた場合処理
        if (rewardedAd != null)
        {
            // 古い広告のハンドラを解除してから破棄（メモリリーク防止）
            UnregisterEventHandlers();
            //リワード広告は使い捨てなので一旦破棄
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        //リクエストを生成
        AdRequest request = new AdRequest();

        if (Application.systemLanguage == SystemLanguage.Japanese)
        {
            request.Keywords.Add("ゲーム");
            request.Keywords.Add("モバイルゲーム");
        }

        else
        {
            request.Keywords.Add("game");
            request.Keywords.Add("mobile games");
        }
        //広告をロード  その後、関数OnRewardedAdLoadedを呼び出す
        RewardedAd.Load(adUnitId, request, OnRewardedAdLoaded);
    }


    // 広告のロードを実施した後に呼び出される関数
    private void OnRewardedAdLoaded(RewardedAd ad, LoadAdError error)
    {
        //変数errorに情報が入っている　または、変数adに情報がはいっていなかったら実行
        if (error != null || ad == null)
        {
            //リワード 読み込み失敗
            Debug.LogError("Failed to load reward ad : " + error);//error:エラー内容

            // 一定時間後に再読み込みを試みる (放置すると以後このシーンでは広告が出なくなる)
            Invoke(nameof(LoadRewardedAd), RetryDelaySeconds);
            return;//この時点でこの関数の実行は終了
        }

        //リワード 読み込み完了
        Debug.Log("Reward ad loaded");

        //RewardedAd.Load(~略~)関数を実行することにより、RewardedAd型の変数adにRewardedAdのインスタンスを生成する。
        //生成したRewardedAd型のインスタンスを変数rewardedAdへ割り当て
        rewardedAd = ad;

        //広告の 表示・表示終了・表示失敗 の内容を登録
        RegisterEventHandlers(rewardedAd);
    }


    //広告の 表示・表示終了・表示失敗 の内容
    private void RegisterEventHandlers(RewardedAd ad)
    {
        // 解除できるようにハンドラを保持
        onAdOpenedHandler = () =>
        {
            //リワード広告 表示
            Debug.Log("Rewarded ad full screen content opened.");
        };
        onAdClosedHandler = () =>
        {
            //リワード広告 表示終了
            Debug.Log("Rewarded ad full screen content closed.");

            //リワード 再読み込み
            LoadRewardedAd();
        };
        onAdFailedHandler = (AdError error) =>
        {
            //エラー表示
            Debug.LogError("Rewarded ad failed to open full screen content with error : " + error);

            //リワード 再読み込み
            LoadRewardedAd();
        };

        ad.OnAdFullScreenContentOpened += onAdOpenedHandler;
        ad.OnAdFullScreenContentClosed += onAdClosedHandler;
        ad.OnAdFullScreenContentFailed += onAdFailedHandler;
    }

    private void UnregisterEventHandlers()
    {
        if (rewardedAd == null) return;
        if (onAdOpenedHandler != null) rewardedAd.OnAdFullScreenContentOpened -= onAdOpenedHandler;
        if (onAdClosedHandler != null) rewardedAd.OnAdFullScreenContentClosed -= onAdClosedHandler;
        if (onAdFailedHandler != null) rewardedAd.OnAdFullScreenContentFailed -= onAdFailedHandler;
        onAdOpenedHandler = null;
        onAdClosedHandler = null;
        onAdFailedHandler = null;
    }
}
