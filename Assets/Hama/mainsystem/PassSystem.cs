using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassSystem : MonoBehaviour
{
    static public int passitemid;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void ItemPass(GameObject playerobj)
    {
        //Debug.Log("パス");
        //Debug.Log(playerobj.name);
        //Debug.Log(passitemid);

        if (playerobj.GetComponent<User_A>())
        {
            var item = ItemDataBase.Entity.GetData(passitemid);
            item.OwnerFlag = 2;
        }
        else if (playerobj.GetComponent<User_B>())
        {
            var item = ItemDataBase.Entity.GetData(passitemid);
            item.OwnerFlag = 1;
        }
        else
        {
            return;
        }
    }
}
