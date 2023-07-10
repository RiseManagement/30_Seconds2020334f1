using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LButton()   //LButtonを押したときの動作
    {
        transform.Rotate(0, 90, 0, Space.World);
    }
    public void RButton()   //RButtonを押したときの動作
    {
        transform.Rotate(0,-90,0,Space.World);
    }
}
