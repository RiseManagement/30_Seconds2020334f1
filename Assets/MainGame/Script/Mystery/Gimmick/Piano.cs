using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Piano : MonoBehaviour
{
    [Header("アップライトピアノ")]
    public Sprite[] keybord_sprite = new Sprite[8]; //0番目は何も押してない状態、1から7番目各鍵盤を押した状態
    public GameObject[] keybord_obj = new GameObject[pianomaxCount];　//ピアノオブジェクト

    public int pianoplaycount;      //ピアノ鍵盤を押した数
    static int pianomaxCount = 6;   //ピアノ鍵盤を押せる最大数
    public bool successFlg;　       //ギミックフラグ

    [Header("アップライトピアノの音")]
    AudioSource audioSource;
    public AudioClip[] audioClip = new AudioClip[7];

    CameraManager cameraManager; //カメラ管理スクリプト
   
    // Start is called before the first frame update
    void Start()
    {
        successFlg = false;
        pianoplaycount = 0;
        cameraManager = GameObject.Find("Main Camera").GetComponent<CameraManager>();
        audioSource = this.transform.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraManager.Focusflg)
        {
            PianoPlay();
        }
        else
        {
            PianoGimmickReset();
            this.gameObject.SetActive(false);
            ItemDataBase.Entity.GetData(int.Parse(this.gameObject.transform.parent.name)).InteractFlag = 0;

            Debug.Log("フォーカスリセット");
        }
    }

    /// <summary>
    /// ピアノ弾く
    /// </summary>
    void PianoPlay()
    {
        if (Input.GetMouseButtonDown(0) && pianoplaycount < pianomaxCount)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);

            string[] arr = hit.collider.gameObject.name.Split('_');

            if (arr.Length < 2) return;

            switch (arr[1])
            {
                case "do":
                    hit.collider.transform.parent.GetComponent<SpriteRenderer>().sprite = keybord_sprite[1];
                    audioSource.PlayOneShot(audioClip[0]);//音再生
                    break;
                case "re":
                    hit.collider.transform.parent.GetComponent<SpriteRenderer>().sprite = keybord_sprite[2];
                    audioSource.PlayOneShot(audioClip[1]);//音再生
                    break;
                case "mi":
                    hit.collider.transform.parent.GetComponent<SpriteRenderer>().sprite = keybord_sprite[3];
                    audioSource.PlayOneShot(audioClip[2]);//音再生

                    break;
                case "fua":
                    hit.collider.transform.parent.GetComponent<SpriteRenderer>().sprite = keybord_sprite[4];
                    audioSource.PlayOneShot(audioClip[3]);//音再生

                    break;
                case "so":
                    hit.collider.transform.parent.GetComponent<SpriteRenderer>().sprite = keybord_sprite[5];
                    audioSource.PlayOneShot(audioClip[4]);//音再生

                    break;
                case "ra":
                    hit.collider.transform.parent.GetComponent<SpriteRenderer>().sprite = keybord_sprite[6];
                    audioSource.PlayOneShot(audioClip[5]);//音再生

                    break;
                case "si":
                    hit.collider.transform.parent.GetComponent<SpriteRenderer>().sprite = keybord_sprite[7];
                    audioSource.PlayOneShot(audioClip[6]);//音再生

                    break;
                default:
                    break;
            }
            keybord_obj[pianoplaycount] = hit.collider.gameObject;
            //Debug.Log(hit.collider.gameObject);
            pianoplaycount++;
        }

        PianoGimmickSuccessCheack();
        PianoGimmickFailureCheack();
    }

    /// <summary>
    /// ピアノギミッククリアチェック
    /// </summary>
    void PianoGimmickSuccessCheack()
    {
        for(int i = 0; i < pianomaxCount; i++)
        {
            if (keybord_obj[i] == null) return;
        }

        //ギミッククリア条件
        if(keybord_obj[0].gameObject.name == "keyboard_do" &&
           keybord_obj[1].gameObject.name == "keyboard_do" && 
           keybord_obj[2].gameObject.name == "keyboard_mi" && 
           keybord_obj[3].gameObject.name == "keyboard_mi" && 
           keybord_obj[4].gameObject.name == "keyboard_so" && 
           keybord_obj[5].gameObject.name == "keyboard_do")
        {
            successFlg = true;
            Debug.Log("ピアノギミック成功");
            StartCoroutine(FocusCancel());

            //Debug.Log("確認：" + this.gameObject.transform.parent.name);

            ItemDataBase.Entity.GetData(int.Parse(this.gameObject.transform.parent.name)).ClearCheck = 2;
            this.gameObject.transform.parent.GetComponent<Gimmick>().GimmmickFlag = true;
        }
    }

    void PianoGimmickFailureCheack()
    {
        if(pianoplaycount >= pianomaxCount && !successFlg)
        {
            StartCoroutine(FocusCancel());
            
            Debug.Log("ピアノギミック失敗");
        }
    }

    /// <summary>
    /// ピアノギミックリセット
    /// </summary>
    public void PianoGimmickReset()
    {
        for (int i = 0; i < pianomaxCount; i++)
        {
            keybord_obj[i] = null;
        }
        this.transform.GetComponent<SpriteRenderer>().sprite = keybord_sprite[0];
        pianoplaycount = 0;
    }

    /// <summary>
    /// フォーカスキャンセル
    /// </summary>
    /// <returns></returns>
    IEnumerator FocusCancel()
    {
        yield return new WaitForSeconds(1.5f);

        cameraManager.FocusCancel();
    }
}
