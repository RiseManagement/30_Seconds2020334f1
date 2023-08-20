using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class User : MonoBehaviour
{
    protected string username;
    public string User_name{ get { return username; } }

    //アイテムウィンドウ情報
    [SerializeField] List<ItemWinowSlot> itemWinowSlot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected void GetItem(GameObject playerobj)
    {
        int count = 0; // 番目を表示するためのもの
        var parent = GameObject.Find("InventryPalent");

        //Debug.Log(parent);

        foreach (Transform child in parent.transform)
        {
            itemWinowSlot.Add(itemWinowSlot[count]);
            itemWinowSlot[count] = child.GetComponent<ItemWinowSlot>(); // 順番に子オブジェクトを取得
            count++;
        }

        GameObject.Find("Scroll View").SetActive(false); 

        int id = 0;

        for (int i = 0; i < itemWinowSlot.Count - 1; i++)
        {
            if (playerobj.GetComponent<User_A>())
            {
               id = itemWinowSlot[i].GetItemData(id, 1);
            }
            if (playerobj.GetComponent<User_B>())
            {
               id = itemWinowSlot[i].GetItemData(id, 2);
            }
            //Debug.Log("id:"+id);
        }
    }

    protected void SceneSet(GameObject playerobj)
    {
        DontDestroyOnLoad(playerobj);

        if (SceneManager.NowSceneName == SceneManager.SceneName.INTERVAL.ToString().ToLower())
        {
            UnityEngine.SceneManagement.SceneManager.MoveGameObjectToScene(playerobj,
                UnityEngine.SceneManagement.SceneManager.GetActiveScene());
            Debug.Log("通過2");
        }
    }
}
