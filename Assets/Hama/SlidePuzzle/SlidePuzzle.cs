using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlidePuzzle : MonoBehaviour
{
    //判定
    public Vector3 saveThisObjPosition;
    public GameObject tapObj;
    public GameObject centerObj;
    public GameObject[] puzzleObj = new GameObject[9];

    // Start is called before the first frame update
    void Start()
    {
        centerObj = this.gameObject.transform.GetChild(0).gameObject;

        for(int i=0;i < puzzleObj.Length; i++){
            puzzleObj[i] = transform.GetChild(i).gameObject;
        }
        PuzzleReset();
    }

    // Update is called once per frame
    void Update()
    {
        int northNo = 0;
        int eastNo = 0;
        int westNo = 0;
        int southNo = 0;

        //
        if (Input.GetMouseButtonDown(0))
        {
            //上下左右にRayを飛ばす。
            RaycastHit2D hitUp = Physics2D.Raycast(centerObj.transform.position + Vector3.up, Vector2.up, 0.1f);
            RaycastHit2D hitDown = Physics2D.Raycast(centerObj.transform.position + Vector3.down, Vector2.down, 0.1f);
            RaycastHit2D hitRight = Physics2D.Raycast(centerObj.transform.position + Vector3.right, Vector2.right, 0.1f);
            RaycastHit2D hitLeft = Physics2D.Raycast(centerObj.transform.position + Vector3.left, Vector2.left, 0.1f);
            if (hitUp){
                if (hitUp.transform.gameObject.name.Contains("Puzzle"))
                    northNo = int.Parse(hitUp.transform.gameObject.name.Substring(7, 1));
            }
            if (hitDown){
                if (hitDown.transform.gameObject.name.Contains("Puzzle"))
                    southNo = int.Parse(hitDown.transform.gameObject.name.Substring(7, 1));
            }
            if (hitRight){
                if (hitRight.transform.gameObject.name.Contains("Puzzle"))
                    eastNo = int.Parse(hitRight.transform.gameObject.name.Substring(7, 1));
            }
            if (hitLeft){
                if (hitLeft.transform.gameObject.name.Contains("Puzzle"))
                    westNo = int.Parse(hitLeft.transform.gameObject.name.Substring(7, 1));
            }
            //Debug.Log(northNo + "," + southNo + "," + eastNo + "," + westNo);

            GetStageItemTapObjectInfo();

            var tapNo = int.Parse(tapObj.name.Substring(7,1));
            var centerNo = int.Parse(centerObj.name.Substring(7,1));

            //Debug.Log("タップNo：" + tapNo);
            //スライド式にするためにタップしたピースがどの位置かを判定する
            if (//上下左右1マスのみ判定
                (tapNo == northNo || 
                tapNo == southNo ||
                tapNo == eastNo||
                tapNo == westNo) 
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
                var savePuzzleObj = puzzleObj[tapNo-1];
                puzzleObj[tapNo-1] = puzzleObj[centerNo-1];
                puzzleObj[centerNo-1] = savePuzzleObj;
            }   
        }

        //ピース完成処理
        if(int.Parse(puzzleObj[0].gameObject.transform.gameObject.name.Substring(7, 1)) == 2 &&
            int.Parse(puzzleObj[1].gameObject.transform.gameObject.name.Substring(7, 1)) == 3 &&
            int.Parse(puzzleObj[2].gameObject.transform.gameObject.name.Substring(7, 1)) == 4 &&
            int.Parse(puzzleObj[3].gameObject.transform.gameObject.name.Substring(7, 1)) == 5 &&
            int.Parse(puzzleObj[4].gameObject.transform.gameObject.name.Substring(7, 1)) == 6 &&
            int.Parse(puzzleObj[5].gameObject.transform.gameObject.name.Substring(7, 1)) == 1 &&
            int.Parse(puzzleObj[6].gameObject.transform.gameObject.name.Substring(7, 1)) == 7 &&
            int.Parse(puzzleObj[7].gameObject.transform.gameObject.name.Substring(7, 1)) == 8 &&
            int.Parse(puzzleObj[8].gameObject.transform.gameObject.name.Substring(7, 1)) == 9
            )
        {
            Debug.Log("パズル完了");
        }
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
                tapObj = hit2d.transform.gameObject;
            Debug.Log(tapObj);
        }

    }

    /// <summary>
    /// パズルリセット
    /// </summary>
    void PuzzleReset()
    {
        List<int> puzzleNo = new List<int>();
        List<GameObject> puzzle = new List<GameObject>();
        int random = 0;
        int randompuzzlecount = puzzleObj.Length-1;

        foreach (GameObject i in puzzleObj){
            puzzle.Add(i);
        }

        Debug.Log(puzzle[0].name + puzzle[1].name + puzzle[2].name+puzzle[3].name+puzzle[4].name+puzzle[5].name+ puzzle[6].name+ puzzle[7].name+ puzzle[8].name);

        while (randompuzzlecount > 0)
        {
            random = Random.Range(1, 9);
            if (!puzzle[randompuzzlecount].gameObject.name.Contains(random.ToString()))
            {
                Debug.Log("ランダム数：" + random);
                puzzleNo.Add(random);
                puzzle.Remove(puzzle[randompuzzlecount]);
                randompuzzlecount--;
            }
        }

        for (int i = 0; i < puzzleObj.Length-1; i++)
        {
            //ランダム数値を取得し、数値のピース名を設定
            //交換の関数作成

            //オブジェクト入れ替え
            var savePuzzleObj = puzzleObj[i];
            puzzleObj[i] = puzzleObj[puzzleNo[i]];
            puzzleObj[puzzleNo[i]] = savePuzzleObj;

            //座標入れ替え
            saveThisObjPosition = puzzleObj[i].transform.position;
            puzzleObj[i].transform.position = puzzleObj[random].transform.position;
            puzzleObj[random].transform.position = saveThisObjPosition;
        }
    }
}
