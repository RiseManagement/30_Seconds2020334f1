using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User_A : User
{
    private void Awake()
    {
        username = NameInputcontroller.player_1name;
        /*if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }*/
    }

    
    // Start is called before the first frame update
    void Start()
    {
        GetItem(this.gameObject);
        SceneSet(this.gameObject);
        User.playerObj = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("ユーザー名：" + username);
    }
}
