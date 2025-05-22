using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openLink : MonoBehaviour
{
    public AudioClip sound1;
    public AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
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
        audio.PlayOneShot(sound1);
    }
}
