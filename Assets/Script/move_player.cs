using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60; // FPS��60�ɐݒ�
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("up")) //���Ȃ�z������0.1f�i��
        {
            transform.position -= transform.forward * 0.1f;
        }
        if (Input.GetKey("down")) //���Ȃ�z������-0.1f�i��
        {
            transform.position += transform.forward * 0.1f;
        }
        if (Input.GetKey("right")) //���Ȃ�y������3.0f��]
        {
            transform.Rotate(0f,3.0f,0f);
        }
        if (Input.GetKey("left")) //���Ȃ�y������-3.0f��]
        {
            transform.Rotate(0f, -3.0f, 0f);
        }
    }
}
