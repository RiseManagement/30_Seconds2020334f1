using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    GameObject Playername;
    public User user;
    // Start is called before the first frame update
    void Start()
    {
        string name;
        name = user.User_name;
        //Debug.Log(name);
        this.Playername = GameObject.Find("PlayerName");
        this.Playername.transform.GetChild(0).GetComponent<Text>().text = (name);
        //Debug.Log("成功");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
