using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWindowClose : MonoBehaviour
{
    public GameObject game;



    public void OnClick()
    {

        game.SetActive(false);


    }

}
