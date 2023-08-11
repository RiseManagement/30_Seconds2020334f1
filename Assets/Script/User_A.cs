using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User_A : User
{
    private void Awake()
    {
        username = "Player_A";
    }

    // Start is called before the first frame update
    void Start()
    {
        GetItem();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
