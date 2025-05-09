using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60; // FPSを60に設定
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("up")) //↑ならz方向に0.1f進む
        {
            transform.position -= transform.forward * 0.1f;
        }
        if (Input.GetKey("down")) //↓ならz方向に-0.1f進む
        {
            transform.position += transform.forward * 0.1f;
        }
        if (Input.GetKey("right")) //→ならy方向に3.0f回転
        {
            transform.Rotate(0f,3.0f,0f);
        }
        if (Input.GetKey("left")) //←ならy方向に-3.0f回転
        {
            transform.Rotate(0f, -3.0f, 0f);
        }
    }
}
