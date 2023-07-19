using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeopItem : MonoBehaviour
{
    public TItem item;


    public void Pickup()
    {
        //Debug.Log("OK");
        Inventry.instance.Add(item);
        Destroy(gameObject);
    }



}
