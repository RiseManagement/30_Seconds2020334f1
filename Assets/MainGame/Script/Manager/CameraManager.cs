using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour
{
    Vector3 cameraPos;
    private Camera mainCam;
    private SpriteRenderer[] stageWalls;
    private float defaultOrthoSize;
    public bool Focusflg; //true:フォーカス中、false：フォーカスではない
    // 現在カメラがフォーカスしている対象オブジェクト。
    // ギミック側が「自分にフォーカスしているときだけ動く」判定に使う。
    public Transform CurrentFocusTarget { get; private set; }
    [SerializeField] GameObject FocusCancelButton;
    [SerializeField] GameObject LButtonActive;
    [SerializeField] GameObject RButtonActive;
    // Start is called before the first frame update
    void Start()
    {
        Focusflg = false;
        mainCam = Camera.main;
        defaultOrthoSize = mainCam != null ? mainCam.orthographicSize : 5f;
        ResolveStageWalls();
        FocusCancelButton.SetActive(false);
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
            cameraPos.z = transform.position.z;
            //Debug.Log(cameraPos);
        }
    }

    /// <summary>
    /// アイテムフォーカス
    /// </summary>
    /// <param name="vector2">アイテム座標</param>
    /// <param name="size">フォーカスサイズ</param>
    public void ItemFocus(Vector2 vector2,int size)//フォーカス機能＋フォーカスボタン表示
    {
        ItemFocus(vector2, size, null);
    }

    /// <summary>
    /// アイテムフォーカス(対象オブジェクト付き)。呼び出し元が対象の Transform を渡すことで、
    /// 各ギミックが「自分自身にフォーカスしているか」を CurrentFocusTarget で識別できる。
    /// </summary>
    public void ItemFocus(Vector2 vector2, int size, Transform target)
    {
        Focusflg = true;
        CurrentFocusTarget = target;
        if (stageWalls == null || stageWalls.Length == 0)
        {
            ResolveStageWalls();
        }
        float desiredSize = GetFocusSize(size);
        mainCam.orthographicSize = ClampFocusSizeToBounds(vector2, desiredSize);
        FocusTransform(vector2);
        FocusCancelButton.SetActive(true);
        LButtonActive.SetActive(false);
        RButtonActive.SetActive(false);

        Debug.Log("アイテムフォーカス: " + (target != null ? target.name : "(no target)"));
    }

    /// <summary>
    /// フォーカス座標
    /// </summary>
    /// <param name="vector2">アイテム座標</param>
    private void FocusTransform(Vector2 vector2)
    {
        Vector2 Focus_adjust = new Vector2(0, 0);    //フォーカスする位置の調整用Vector2
        var target = new Vector3(vector2.x + Focus_adjust.x, vector2.y + Focus_adjust.y, transform.position.z);
        transform.position = ClampToStageBounds(target);
    }

    /// <summary>
    /// フォーカスズーム設定
    /// </summary>
    /// <param name="size">フォーカスサイズ</param>
    private void SetFocusSize(int size)
    {
        mainCam.orthographicSize = GetFocusSize(size);
    }

    private float GetFocusSize(int size)
    {
        switch(size)
        {
            case 1://ズーム小
                return 4;
            case 2: //ズーム中
                return 3;
            case 3: //ズーム大
                return 2;
            default:
                return defaultOrthoSize;
        }
    }

    /// <summary>
    /// フォーカスキャンセル
    /// </summary>
    public void FocusCancel()
    {
        mainCam.transform.position = cameraPos;
        SetFocusSize(0);
        Focusflg = false;
        CurrentFocusTarget = null;
        FocusCancelButton.SetActive(false);
        LButtonActive.SetActive(true);
        RButtonActive.SetActive(true);

        Debug.Log("フォーカスキャンセル");
    }

    private void ResolveStageWalls()
    {
        var all = FindObjectsOfType<SpriteRenderer>();
        var list = new List<SpriteRenderer>();
        foreach (var sr in all)
        {
            if (sr != null && sr.name.StartsWith("wall_"))
            {
                list.Add(sr);
            }
        }
        stageWalls = list.ToArray();
    }

    private Vector3 ClampToStageBounds(Vector3 target)
    {
        if (mainCam == null)
        {
            return target;
        }

        if (stageWalls == null || stageWalls.Length == 0)
        {
            ResolveStageWalls();
        }

        if (stageWalls == null || stageWalls.Length == 0)
        {
            return target;
        }

        var wall = GetWallForCurrentView();
        if (wall == null)
        {
            return target;
        }

        var bounds = wall.bounds;
        float halfHeight = mainCam.orthographicSize;
        float halfWidth = halfHeight * mainCam.aspect;

        float minX = bounds.min.x + halfWidth;
        float maxX = bounds.max.x - halfWidth;
        float minY = bounds.min.y + halfHeight;
        float maxY = bounds.max.y - halfHeight;

        float x = Mathf.Clamp(target.x, minX, maxX);
        float y = Mathf.Clamp(target.y, minY, maxY);
        return new Vector3(x, y, target.z);
    }

    private float ClampFocusSizeToBounds(Vector2 focusPoint, float desiredSize)
    {
        if (mainCam == null)
        {
            return desiredSize;
        }

        if (stageWalls == null || stageWalls.Length == 0)
        {
            ResolveStageWalls();
        }

        if (stageWalls == null || stageWalls.Length == 0)
        {
            return desiredSize;
        }

        var wall = GetWallForCurrentView();
        if (wall == null)
        {
            return desiredSize;
        }

        var bounds = wall.bounds;
        float maxHalfHeight = Mathf.Min(focusPoint.y - bounds.min.y, bounds.max.y - focusPoint.y);
        float maxHalfWidth = Mathf.Min(focusPoint.x - bounds.min.x, bounds.max.x - focusPoint.x) / mainCam.aspect;
        float maxAllowed = Mathf.Min(maxHalfHeight, maxHalfWidth);

        if (maxAllowed <= 0f)
        {
            return Mathf.Max(0.1f, desiredSize);
        }

        return Mathf.Min(desiredSize, maxAllowed);
    }

    private SpriteRenderer GetWallForCurrentView()
    {
        SpriteRenderer containing = null;
        Vector3 viewCenter = cameraPos;
        foreach (var sr in stageWalls)
        {
            if (sr == null) continue;
            if (sr.bounds.Contains(viewCenter))
            {
                containing = sr;
                break;
            }
        }

        if (containing != null)
        {
            return containing;
        }

        // フォールバック: どの壁にも含まれない場合は最も近い wall を使用
        SpriteRenderer nearest = null;
        float best = float.MaxValue;
        foreach (var sr in stageWalls)
        {
            if (sr == null) continue;
            float d = (sr.bounds.center - viewCenter).sqrMagnitude;
            if (d < best)
            {
                best = d;
                nearest = sr;
            }
        }
        return nearest;
    }
}
