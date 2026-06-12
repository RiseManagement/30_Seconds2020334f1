using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class Piano : MonoBehaviour
{
    [Header("アップライトピアノ")]
    [FormerlySerializedAs("keybord_sprite")]
    public Sprite[] keyboardSprite = new Sprite[8];
    [FormerlySerializedAs("keybord_obj")]
    public GameObject[] keyboardObj = new GameObject[pianomaxCount];
    public int pianoplaycount;
    static int pianomaxCount = 6;
    public bool successFlg;

    CameraManager cameraManager;

    // WaitForSeconds キャッシュ（GC抑制）
    static readonly WaitForSeconds _wait1_5 = new WaitForSeconds(1.5f);

    // Start is called before the first frame update
    void Start()
    {
        successFlg = false;
        pianoplaycount = 0;

        var mainCameraObj = GameObject.Find("Main Camera");
        if (mainCameraObj != null)
        {
            cameraManager = mainCameraObj.GetComponent<CameraManager>();
        }
        if (cameraManager == null)
        {
            Debug.LogWarning("[Piano] CameraManager が取得できませんでした。");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraManager == null) return;
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
            if (hit.collider == null)
            {
                return;
            }

                string[] arr = hit.collider.gameObject.name.Split('_');

                if (arr.Length < 2) return; 

                switch (arr[1])
                {
                    case "do":
                        hit.collider.transform.parent.GetComponent<SpriteRenderer>().sprite = keyboardSprite[1];
                        break;
                    case "re":
                        hit.collider.transform.parent.GetComponent<SpriteRenderer>().sprite = keyboardSprite[2];
                        break;
                    case "mi":
                        hit.collider.transform.parent.GetComponent<SpriteRenderer>().sprite = keyboardSprite[3];
                        break;
                    case "fua":
                        hit.collider.transform.parent.GetComponent<SpriteRenderer>().sprite = keyboardSprite[4];
                        break;
                    case "so":
                        hit.collider.transform.parent.GetComponent<SpriteRenderer>().sprite = keyboardSprite[5];
                        break;
                    case "ra":
                        hit.collider.transform.parent.GetComponent<SpriteRenderer>().sprite = keyboardSprite[6];
                        break;
                    case "si":
                        hit.collider.transform.parent.GetComponent<SpriteRenderer>().sprite = keyboardSprite[7];
                        break;
                    default:
                        break;
                }
                keyboardObj[pianoplaycount] = hit.collider.gameObject;
                //Debug.Log(hit.collider.gameObject);
                pianoplaycount++;
        }

        PianoGimmickSuccessCheck();
        PianoGimmickFailureCheck();
    }

    /// <summary>
    /// ピアノギミッククリアチェック
    /// </summary>
    void PianoGimmickSuccessCheck()
    {
        for(int i = 0; i < pianomaxCount; i++)
        {
            if (keyboardObj[i] == null) return;
        }

        //
        if(keyboardObj[0].gameObject.name == "keyboard_do" &&
           keyboardObj[1].gameObject.name == "keyboard_do" && 
           keyboardObj[2].gameObject.name == "keyboard_mi" && 
           keyboardObj[3].gameObject.name == "keyboard_mi" && 
           keyboardObj[4].gameObject.name == "keyboard_so" && 
           keyboardObj[5].gameObject.name == "keyboard_do")
        {
            successFlg = true;
            Debug.Log("ピアノギミック成功");
            StartCoroutine(FocusCancel());

            Debug.Log("確認：" + this.gameObject.transform.parent.name);

            ItemDataBase.Entity.GetData(int.Parse(this.gameObject.transform.parent.name)).ClearCheck = 2;
            this.gameObject.transform.parent.GetComponent<Gimmick>().GimmickFlag = true;
        }
    }

    void PianoGimmickFailureCheck()
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
            keyboardObj[i] = null;
        }
        this.transform.GetComponent<SpriteRenderer>().sprite = keyboardSprite[0];
        pianoplaycount = 0;
    }

    IEnumerator FocusCancel()
    {
        yield return _wait1_5;

        if (cameraManager != null)
        {
            cameraManager.FocusCancel();
        }
    }
}
