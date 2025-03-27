using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debug_Mode : MonoBehaviour
{
    public List<GameObject> debugObjects = new List<GameObject>();
    public bool debugmode = false;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < this.gameObject.transform.childCount; i++)
        {
            debugObjects.Add(this.gameObject.transform.GetChild(i).gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (debugmode)
        {
            for (int i = 1; i < debugObjects.Count; i++)
            {
               debugObjects[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 1; i < debugObjects.Count; i++)
            {
                debugObjects[i].SetActive(false);
            }
        }
    }

    /// <summary>
    /// デバッグモード
    /// </summary>
    public void DebugModeOn()
    {
        debugmode = true;
        debugObjects[0].SetActive(false);
        debugObjects[1].SetActive(true);
    }
    public void DebugModeOff()
    {
        debugmode = false;
        debugObjects[1].SetActive(false);
        debugObjects[0].SetActive(true);
    }
}
