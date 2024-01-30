using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidePuzzle : MonoBehaviour
{
    //判定
    bool isCol;
    public int x_MoveCount = 1;
    public int y_MoveCount = 1;
    public Vector2 thisObjPosition;
    public Vector2 saveThisObjPosition;

    // Start is called before the first frame update
    void Start()
    {
        thisObjPosition = this.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey == false)
        {
            return;
        }

        thisObjPosition = this.gameObject.transform.position;

        if (Input.GetKeyDown(KeyCode.LeftArrow) && x_MoveCount > -1)
        {
            isCol = true;
            saveThisObjPosition = this.gameObject.transform.position;
            thisObjPosition.x -= 1.05f;
            this.gameObject.transform.position = thisObjPosition;
            x_MoveCount -= 1;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && x_MoveCount < 1)
        {
            isCol = true;
            saveThisObjPosition = this.gameObject.transform.position;
            thisObjPosition.x += 1.05f;
            this.gameObject.transform.position = thisObjPosition;
            x_MoveCount += 1;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && y_MoveCount < 1)
        {
            isCol = true;
            saveThisObjPosition = this.gameObject.transform.position;
            thisObjPosition.y += 1.05f;
            this.gameObject.transform.position = thisObjPosition;
            y_MoveCount += 1;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && y_MoveCount > -1)
        {
            isCol = true;
            saveThisObjPosition = this.gameObject.transform.position;
            thisObjPosition.y -= 1.05f;
            this.gameObject.transform.position = thisObjPosition;
            y_MoveCount -= 1;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isCol) return;
        
        //衝突してほしいゲームオブジェクトでなければ抜ける
        if (collision.gameObject.tag != "Area")
        {
            return;
        }
        collision.transform.position = saveThisObjPosition;
        isCol = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isCol) return;

        //衝突してほしいゲームオブジェクトでなければ抜ける
        if (collision.gameObject.tag != "Area")
        {
            return;
        }
        collision.transform.position = saveThisObjPosition;
        isCol = false;
    }
}
