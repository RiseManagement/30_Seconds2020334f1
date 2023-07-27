using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWindow : MonoBehaviour
{
    public GameObject game;

    public void ItemWindowOpen()
    {
        game.SetActive(true);
    }

}
