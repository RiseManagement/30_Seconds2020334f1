using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    public ItemDataBase itemData;
    private void Awake()
    {
        itemData = ItemDataBase.Entity;
    }

    // Start is called before the first frame update
    void Start()
    {
        var a = itemData.GetData(2);
        Debug.Log(a.Name);
        Debug.Log(a.OwnerFlag);
        a.OwnerFlag = 2;
        Debug.Log(a.OwnerFlag);
        Debug.Log(a.Image);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
