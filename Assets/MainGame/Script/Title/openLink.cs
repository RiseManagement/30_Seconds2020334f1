using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openLink : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        Debug.Log("clicked");
        var uri = new System.Uri("https://dubd8946.wixsite.com/risegameteam");
        Application.OpenURL(uri.AbsoluteUri);
    }
}
