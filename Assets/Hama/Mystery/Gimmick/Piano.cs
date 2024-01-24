using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Piano : MonoBehaviour
{
    [Header("アップライトピアノ")]
    public Sprite[] keybord_sprite = new Sprite[8];
    public GameObject[] keybord_obj = new GameObject[pianomaxCount];
    public int pianoplaycount;
    static int pianomaxCount = 6;
    public bool successFlg;

    CameraManager cameraManager;

    // Start is called before the first frame update
    void Start()
    {
        successFlg = false; 
        pianoplaycount = 0;
        cameraManager = GameObject.Find("Main Camera").GetComponent<CameraManager>();
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
                        break;
                    case "re":
                        hit.collider.transform.parent.GetComponent<SpriteRenderer>().sprite = keybord_sprite[2];
                        break;
                    case "mi":
                        hit.collider.transform.parent.GetComponent<SpriteRenderer>().sprite = keybord_sprite[3];
                        break;
                    case "fua":
                        hit.collider.transform.parent.GetComponent<SpriteRenderer>().sprite = keybord_sprite[4];
                        break;
                    case "so":
                        hit.collider.transform.parent.GetComponent<SpriteRenderer>().sprite = keybord_sprite[5];
                        break;
                    case "ra":
                        hit.collider.transform.parent.GetComponent<SpriteRenderer>().sprite = keybord_sprite[6];
                        break;
                    case "si":
                        hit.collider.transform.parent.GetComponent<SpriteRenderer>().sprite = keybord_sprite[7];
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

        //
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

    IEnumerator FocusCancel()
    {
        yield return new WaitForSeconds(1);

        cameraManager.FocusCancel();
    }
}
