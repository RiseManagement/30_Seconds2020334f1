using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private float x;
    // Start is called before the first frame update
    void Start()
    {
        x = transform.position.x;
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

    //引数がアイテムクリック時のアイテム座標のフォーカス機能
    public void Focus(Vector2 vector2)
    {
        Vector2 Focus_adjust = new Vector2(0, 0);    //フォーカスする位置の調整用Vector2
        transform.position = vector2+Focus_adjust ;
        this.transform.localScale = new Vector2(2, 2);
    }
}
