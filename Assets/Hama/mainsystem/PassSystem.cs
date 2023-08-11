using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassSystem : MonoBehaviour
{
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
        Debug.Log(playerobj.name);

        if (playerobj.GetComponent<User_A>())
        {
            var itemid = playerobj.GetComponent<User_A>().itemID;
            var item = ItemDataBase.Entity.GetData(itemid.id);
            item.OwnerFlag = 1;
        }
        else if (playerobj.GetComponent<User_B>())
        {
            var itemid = playerobj.GetComponent<User_B>().itemID;
            var item = ItemDataBase.Entity.GetData(itemid.id);
            item.OwnerFlag = 2;
        }
        else
        {
            return;
        }
    }
}
