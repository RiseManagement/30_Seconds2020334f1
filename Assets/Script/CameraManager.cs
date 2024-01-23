using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour
{
    Vector3 cameraPos;
    private Camera mainCam;
    public bool Focusflg;
    [SerializeField] GameObject FocusCancelButton;
    [SerializeField] GameObject LButtonActive;
    [SerializeField] GameObject RButtonActive;
    // Start is called before the first frame update
    void Start()
    {
        Focusflg = false;
        mainCam = Camera.main;
        FocusCancelButton.SetActive(false) ;
        LButtonActive.SetActive(true);
        RButtonActive.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        CameraPosUpdate();
    }

    public void LButton()   //LButtonを押したときの動作
    {
        cameraPos.x = transform.position.x;
        if (cameraPos.x < 1)
        {
            transform.Translate(60, 0, 0, Space.World);
            Debug.Log("LButton_if");
        }
        else
        {
            transform.Translate(-20, 0, 0, Space.World);
            Debug.Log("LButton_else");
        }
        //transform.Rotate(0, 90, 0, Space.World);
    }
    public void RButton()   //RButtonを押したときの動作
    {
        cameraPos.x = transform.position.x;
        if(cameraPos.x > 59)
        {
            transform.Translate(-60, 0, 0, Space.World);
        }
        else
        {
            transform.Translate(20, 0, 0, Space.World);
        }
        
        Debug.Log("RButton");
        //transform.Rotate(0,-90,0,Space.World);
    }

    /// <summary>
    /// フォーカスではない時座標更新
    /// </summary>
    void CameraPosUpdate()
    {
        if (!Focusflg)
        {
            cameraPos.x = transform.position.x;
            cameraPos.y = transform.position.y;
        }
    }

    /// <summary>
    /// アイテムフォーカス
    /// </summary>
    /// <param name="vector2">アイテム座標</param>
    /// <param name="size">フォーカスサイズ</param>
    public void ItemFocus(Vector2 vector2,int size)//フォーカス機能＋フォーカスボタン表示
    {
        Focusflg = true;
        FocusTransform(vector2);
        SetFocusSize(size);
        FocusCancelButton.SetActive(true);
        LButtonActive.SetActive(false);
        RButtonActive.SetActive(false);
    }

    /// <summary>
    /// フォーカス座標
    /// </summary>
    /// <param name="vector2">アイテム座標</param>
    private void FocusTransform(Vector2 vector2)
    {
        Vector2 Focus_adjust = new Vector2(0, 0);    //フォーカスする位置の調整用Vector2
        transform.position = vector2+Focus_adjust ;
    }

    /// <summary>
    /// フォーカスズーム設定
    /// </summary>
    /// <param name="size">フォーカスサイズ</param>
    private void SetFocusSize(int size)
    {
        switch(size)
        {
            case 1://ズーム小
                mainCam.orthographicSize = 4;
                break;
            case 2: //ズーム中
                mainCam.orthographicSize = 3;
                break;
            case 3: //ズーム大
                mainCam.orthographicSize = 2;
                break;
            default:
                mainCam.orthographicSize = 5;
                break;
        }   
    }

    /// <summary>
    /// フォーカスキャンセル
    /// </summary>
    public void FocusCancel()
    {
        mainCam.orthographicSize = 5;
        mainCam.transform.position = cameraPos;
        Focusflg = false;
        FocusCancelButton.SetActive(false);
        LButtonActive.SetActive(true);
        RButtonActive.SetActive(true);
    }
}
