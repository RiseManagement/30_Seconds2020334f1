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

    // WaitForSeconds を使い回すためのキャッシュ（GC抑制）
    static readonly WaitForSeconds _waitOne = new WaitForSeconds(1f);

    // Start is called before the first frame update
    void Start()
    {
        successFlag = false;
        clearFlag = false;
        centerObj = this.gameObject.transform.GetChild(0).gameObject;

        var mainCameraObj = GameObject.Find("Main Camera");
        if (mainCameraObj != null)
        {
            cameraManager = mainCameraObj.GetComponent<CameraManager>();
        }
        if (cameraManager == null)
        {
            Debug.LogWarning("[SlidePuzzle] CameraManager が取得できませんでした。");
        }

        for(int i=0;i < puzzleObj.Length; i++){
            puzzleObj[i] = transform.GetChild(i).gameObject;
            answerpuzzleObj[i] = transform.GetChild(i).gameObject;
        }
        PuzzleReset();

        if (!PuzzleClearCkeck())
        {
            PuzzleReset();
            Debug.Log("やり直し");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraManager == null) return;

        // パズルにフォーカスしているときのみ動作させる。
        // SlidePuzzle スクリプトは "4_SildePuzzle" に付いており、その親が ID "4" (パズル本体) なので、
        // CameraManager.CurrentFocusTarget が自分の親と一致する場合だけ操作可能にする。
        // (机(ID2)など他オブジェクトにフォーカスしている間は誤作動しないようにするため)
        bool focusedOnThis = cameraManager.Focusflg
                             && cameraManager.CurrentFocusTarget != null
                             && cameraManager.CurrentFocusTarget == transform.parent;

        if (focusedOnThis)
        {
            for (int i = 0; i < puzzleObj.Length; i++)
                puzzleObj[i].SetActive(true);
            SlidePuzzlePlay();
        }
        else
        {
            for (int i = 0; i < puzzleObj.Length; i++)
                puzzleObj[i].SetActive(false);
        }
    }

    void SlidePuzzlePlay()
    {
        //空白のピースの配列インデックス取得
        var centerNo = -1;
        for (int i = 0; i < puzzleObj.Length; i++)
        {
            if (puzzleObj[i] == centerObj)
            {
                centerNo = i;
                break;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            GetStageItemTapObjectInfo();

            if (!tapObjFlag) return;
            Debug.Log("パズル移動開始");

            var tapNo = -1;
            for (int i = 0; i < puzzleObj.Length; i++)
            {
                if (puzzleObj[i] == tapObj)
                {
                    tapNo = i;
                    break;
                }
            }
            if (tapNo < 0 || centerNo < 0) return;

            Debug.Log("センターNo：" + centerNo);
            Debug.Log("タップNo：" + tapNo);

            // 親 "4" が大きな BoxCollider2D(3x3) を持っているため、
            // Physics2D.Raycast での上下左右1マス判定は "4" のコライダに邪魔されて機能しない。
            // そのため中央ピースとタップしたピースの localPosition 差が
            // 上下左右 1 マス(=1 ユニット)以内かだけで隣接判定を行う。
            Vector3 diff = tapObj.transform.localPosition - centerObj.transform.localPosition;
            float absX = Mathf.Abs(diff.x);
            float absY = Mathf.Abs(diff.y);
            const float tolerance = 0.3f;
            bool isAdjacent =
                (absX < tolerance && Mathf.Abs(absY - 1f) < tolerance) ||
                (absY < tolerance && Mathf.Abs(absX - 1f) < tolerance);

            if (isAdjacent)
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

            //使用済に更新
            ItemDataBase.Entity.GetData(int.Parse(this.gameObject.transform.parent.name)).InteractFlag = 1;
            //アイテム名前変更
            gameObject.transform.parent.name = (int.Parse(gameObject.transform.parent.name) + 1).ToString();

            //クリア判定
            ItemDataBase.Entity.GetData(int.Parse(this.gameObject.transform.parent.name)).ClearCheck = 2;
            
            //机は中身情報に変更
            gameObject.transform.root.Find("2").gameObject.name = "3";
            ItemDataBase.Entity.GetData(3).InteractFlag = 1;
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
        tapObjFlag = false;

        // 2D シーンはオルソカメラ + z=0 平面に貼り付くスプライトなので、
        // マウスのスクリーン座標をワールド座標に変換して OverlapPointAll で判定する。
        if (Camera.main == null) return;
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mouse2D = new Vector2(mouseWorld.x, mouseWorld.y);

        // 親 "4" が大きな BoxCollider2D(3x3) を持っていて、OverlapPoint だと先にそちらが
        // ヒットしてしまう。OverlapPointAll で全重なりを取得し、その中から "Puzzle_" 名の
        // コライダのみを採用する。
        Collider2D[] hits = Physics2D.OverlapPointAll(mouse2D);
        foreach (var hit in hits)
        {
            if (hit == null) continue;
            if (hit.gameObject.name.StartsWith("Puzzle_"))
            {
                tapObj = hit.gameObject;
                tapObjFlag = true;
                Debug.Log("パズルタップ可能:" + tapObj);
                return;
            }
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
            // Random.Range(int,int) は上限排他なので puzzleObj.Length(=9) を指定し、
            // 最後のピース番号(8)もシャッフル対象に含める。
            random = Random.Range(0, puzzleObj.Length);
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
        yield return _waitOne;

        if (cameraManager != null)
        {
            cameraManager.FocusCancel();
        }
    }
}
