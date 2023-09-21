using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemFocus : MonoBehaviour
{ 
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //引数はアイテムクリック時のアイテム座標
    void Focus(Vector2 vector2)
    {
        transform.position = vector2;
        this.transform.localScale=new Vector2(2,2);
    }
    
}
