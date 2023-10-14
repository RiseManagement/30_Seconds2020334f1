using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private float x;
    private Camera mainCam;
    // Start is called before the first frame update
    void Start()
    {
        x = transform.position.x;
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LButton()   //LButtonを押したときの動作
    {
        x = transform.position.x;
        if (x<1)
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
        x = transform.position.x;
        if(x>59)
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

    public void ItemFocus(Vector2 vector2,int a)
    {
        FocusTransform(vector2);
        FocusSize(a);
    }

    //引数がアイテムクリック時のアイテム座標のフォーカス機能
    private void FocusTransform(Vector2 vector2)
    {
        Vector2 Focus_adjust = new Vector2(0, 0);    //フォーカスする位置の調整用Vector2
        transform.position = vector2+Focus_adjust ;
    }

    private void FocusSize(int a)
    {
        switch(a)
        {
            case 1://ズーム小
                mainCam.orthographicSize += 1;
                break;
            case 2: //ズーム中
                mainCam.orthographicSize += 2;
                break;
            case 3: //ズーム大
                mainCam.orthographicSize += 3;
                break;
            default:
                mainCam.orthographicSize = 5;
                break;
        }   
    }

}
