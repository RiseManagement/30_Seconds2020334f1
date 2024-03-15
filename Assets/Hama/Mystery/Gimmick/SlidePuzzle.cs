using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlidePuzzle : MonoBehaviour
{
    //座標保存
    public Vector3 saveThisObjPosition;

    //ピース交換対象オブジェクト
    public GameObject tapObj;
    public GameObject centerObj;

    //パズル数
    const int puzzle_MAX_COUNT = 9;
    public GameObject[] puzzleObj = new GameObject[puzzle_MAX_COUNT];
    public GameObject[] answerpuzzleObj = new GameObject[puzzle_MAX_COUNT];

    //フラグ
    public bool successFlag;
    public bool tapObjFlag;
    public bool clearFlag;

    CameraManager cameraManager;

    // Start is called before the first frame update
    void Start()
    {
        successFlag = false;
        clearFlag = false;
        centerObj = this.gameObject.transform.GetChild(0).gameObject;
        cameraManager = GameObject.Find("Main Camera").GetComponent<CameraManager>();

        for(int i=0;i < puzzleObj.Length; i++){
            puzzleObj[i] = transform.GetChild(i).gameObject;
            answerpuzzleObj[i] = transform.GetChild(i).gameObject;
        }
        PuzzleReset();
        if (!PuzzleClearCkeck())
        {
            //PuzzleReset();
            Debug.Log("やり直し");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraManager.Focusflg)
        {
            SlidePuzzlePlay();
        }
        else
        {
            this.gameObject.SetActive(false);
            Debug.Log("フォーカスリセット");
        }
    }

    void SlidePuzzlePlay()
    {
        int northNo = 0;
        int eastNo = 0;
        int westNo = 0;
        int southNo = 0;

        //上下左右にRayを飛ばす。
        RaycastHit2D hitUp = Physics2D.Raycast(centerObj.transform.position + Vector3.up, Vector2.up, 0.1f);
        RaycastHit2D hitDown = Physics2D.Raycast(centerObj.transform.position + Vector3.down, Vector2.down, 0.1f);
        RaycastHit2D hitRight = Physics2D.Raycast(centerObj.transform.position + Vector3.right, Vector2.right, 0.1f);
        RaycastHit2D hitLeft = Physics2D.Raycast(centerObj.transform.position + Vector3.left, Vector2.left, 0.1f);
        if (hitUp)
        {
            if (hitUp.transform.gameObject.name.Contains("Puzzle"))
                northNo = int.Parse(hitUp.transform.gameObject.name.Substring(7, 1));
        }
        if (hitDown)
        {
            if (hitDown.transform.gameObject.name.Contains("Puzzle"))
                southNo = int.Parse(hitDown.transform.gameObject.name.Substring(7, 1));
        }
        if (hitRight)
        {
            if (hitRight.transform.gameObject.name.Contains("Puzzle"))
                eastNo = int.Parse(hitRight.transform.gameObject.name.Substring(7, 1));
        }
        if (hitLeft)
        {
            if (hitLeft.transform.gameObject.name.Contains("Puzzle"))
                westNo = int.Parse(hitLeft.transform.gameObject.name.Substring(7, 1));
        }
        //Debug.Log(northNo + "," + southNo + "," + eastNo + "," + westNo);


        //空白のピースの位置を取得する
        var centerNo = 0;
        for(int i = 0; i < puzzleObj.Length; i++)
        {
            if (puzzleObj[i].gameObject.name.Contains(centerObj.name))
                centerNo = i;
        }

        if (Input.GetMouseButtonDown(0))
        {
            GetStageItemTapObjectInfo();

            if (!tapObjFlag) return;

            var tapNo = 0;
            for (int i = 0; i < puzzleObj.Length; i++)
            {
                if (puzzleObj[i].gameObject.name.Contains(tapObj.name))
                    tapNo = i;
            }
            //Debug.Log("センターNo：" + centerNo);
            //Debug.Log("タップNo：" + tapNo);

            //スライド式にするためにタップしたピースがどの位置かを判定する
            if (//上下左右1マスのみ判定
                (int.Parse(tapObj.name.Substring(7, 1)) == northNo ||
                int.Parse(tapObj.name.Substring(7, 1)) == southNo ||
                int.Parse(tapObj.name.Substring(7, 1)) == eastNo ||
                int.Parse(tapObj.name.Substring(7, 1)) == westNo)
                &&
                //位置判定
                (tapObj.transform.position.x < centerObj.transform.position.x ||
               tapObj.transform.position.x > centerObj.transform.position.x ||
               tapObj.transform.position.y < centerObj.transform.position.y ||
               tapObj.transform.position.y > centerObj.transform.position.y)
               )
            {
                //座標入れ替え
                saveThisObjPosition = tapObj.transform.position;
                tapObj.transform.position = centerObj.transform.position;
                centerObj.transform.position = saveThisObjPosition;

                //オブジェクト入れ替え
                var savePuzzleObj = puzzleObj[tapNo];
                puzzleObj[tapNo] = puzzleObj[centerNo];
                puzzleObj[centerNo] = savePuzzleObj;

                //Debug.Log("交換");
            }
        }

        //ピース完成処理
        if (int.Parse(puzzleObj[0].gameObject.transform.gameObject.name.Substring(7, 1)) == 0 &&
            int.Parse(puzzleObj[1].gameObject.transform.gameObject.name.Substring(7, 1)) == 1 &&
            int.Parse(puzzleObj[2].gameObject.transform.gameObject.name.Substring(7, 1)) == 2 &&
            int.Parse(puzzleObj[3].gameObject.transform.gameObject.name.Substring(7, 1)) == 3 &&
            int.Parse(puzzleObj[4].gameObject.transform.gameObject.name.Substring(7, 1)) == 4 &&
            int.Parse(puzzleObj[5].gameObject.transform.gameObject.name.Substring(7, 1)) == 5 &&
            int.Parse(puzzleObj[6].gameObject.transform.gameObject.name.Substring(7, 1)) == 6 &&
            int.Parse(puzzleObj[7].gameObject.transform.gameObject.name.Substring(7, 1)) == 7 &&
            int.Parse(puzzleObj[8].gameObject.transform.gameObject.name.Substring(7, 1)) == 8
            && !clearFlag)
        {
            Debug.Log("パズル完了");
            successFlag = true;
            clearFlag = true;
            StartCoroutine(FocusCancel());
            ItemDataBase.Entity.GetData(int.Parse(this.gameObject.transform.parent.name)).InteractFlag = 1;
            gameObject.transform.parent.name = (int.Parse(gameObject.transform.parent.name) + 1).ToString();
        }
    }

    /// <summary>
    /// クリア可能か確認
    /// </summary>
    bool PuzzleClearCkeck()
    {
        bool isClear = false;
        int movecount = 0;
        List<GameObject> puzzle = new List<GameObject>();

        //一時的ピースリストに追加
        foreach (GameObject i in puzzleObj)
        {
            puzzle.Add(i);
        }

        for (int i = 0; i < puzzleObj.Length-1; i++)
        {
            bool match = false;
            int index = 0;
            //正解の位置確認
            while(!match)
            {
                //位置が正解の位置と同じ場合一度も移動してないならループ抜ける移動してるなら現在の位置と
                if(puzzle[index] == answerpuzzleObj[i])
                {
                    if(index > 0)
                    {
                        //交換
                        var w = puzzle[i];
                        puzzle[i] = puzzle[index];
                        puzzle[index] = w;
                    }
                    match = true;
                }
                else
                {
                    index++;
                    //Debug.Log("移動数：" + movecount);
                    movecount++;
                }
            }
        }

        //クリア可能か確認
        if(movecount % 2 == 0)
        {
            isClear = true;
        }

        return isClear;
    }

    /// <summary>
    /// タップしたオブジェクトの情報取得
    /// </summary>
    void GetStageItemTapObjectInfo()
    {
        tapObj = null;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit2d = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);

        if (hit2d)
        {
            if (hit2d.transform.gameObject.name.Contains("Puzzle"))
            {
                tapObj = hit2d.transform.gameObject;
                tapObjFlag = true;
            }
            //Debug.Log(tapObj);
        }
        else
        {
            tapObjFlag = false;
        }

    }

    /// <summary>
    /// パズルリセット
    /// </summary>
    void PuzzleReset()
    {
        //ピース番号リスト
        List<int> puzzleNo = new List<int>();

        //一時的ピースリスト
        List<GameObject> puzzle = new List<GameObject>();
        int random = 0;
        int randompuzzlecount = puzzleObj.Length-1;

        //一時的ピースリストに追加
        foreach (GameObject i in puzzleObj){
            puzzle.Add(i);
        }

        //Debug.Log(puzzleNo[0] + puzzleNo[1] + puzzle[2].name+puzzle[3].name+puzzle[4].name+puzzle[5].name+ puzzle[6].name+ puzzle[7].name+ puzzle[8].name);

        //ピース分をランダムにセット
        while (randompuzzlecount > 0)
        {
            random = Random.Range(0, 8);
            if (!puzzle[randompuzzlecount].gameObject.name.Contains(random.ToString()))
            {
                //Debug.Log("ランダム数：" + random);
                puzzleNo.Add(random);
                puzzle.Remove(puzzle[randompuzzlecount]);
                randompuzzlecount--;
            }
        }

        for (int i = 0; i < puzzleObj.Length-1; i++)
        {
            //オブジェクト入れ替え
            var savePuzzleObj = puzzleObj[i];
            puzzleObj[i] = puzzleObj[puzzleNo[i]];
            puzzleObj[puzzleNo[i]] = savePuzzleObj;

            //座標入れ替え
            saveThisObjPosition = puzzleObj[i].transform.position;
            puzzleObj[i].transform.position = puzzleObj[puzzleNo[i]].transform.position;
            puzzleObj[puzzleNo[i]].transform.position = saveThisObjPosition;
        }
    }

    /// <summary>
    /// コールチンのフォーカス解除
    /// </summary>
    /// <returns></returns>
    IEnumerator FocusCancel()
    {
        yield return new WaitForSeconds(1);

        cameraManager.FocusCancel();
    }
}
