using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User_B : User
{
    private void Awake()
    {
        username = "Player_B";
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
        
    }
}
