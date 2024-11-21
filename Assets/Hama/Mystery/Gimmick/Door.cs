using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
   public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        //アニメーション再生
        animator = gameObject.GetComponent<Animator>();
        //AかBでクリアフラグセット
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// アニメーション再生
    /// </summary>
    public void animeStart()
    {
        animator.SetBool("open", true);
    }

    /// <summary>
    /// アニメーション終了
    /// </summary>
    void endAnime()
    {
        Debug.Log("アニメ終了");
        
        //プロセス変更
        MainGameProgress.gameStaus = MainGameProgress.GameStaus.Interval;
    }

}
