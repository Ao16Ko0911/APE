using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60; // FPS‚ğ60‚Éİ’è
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("up")) //ª‚È‚çz•ûŒü‚É0.1fi‚Ş
        {
            transform.position -= transform.forward * 0.1f;
        }
        if (Input.GetKey("down")) //«‚È‚çz•ûŒü‚É-0.1fi‚Ş
        {
            transform.position += transform.forward * 0.1f;
        }
        if (Input.GetKey("right")) //¨‚È‚çy•ûŒü‚É3.0f‰ñ“]
        {
            transform.Rotate(0f,3.0f,0f);
        }
        if (Input.GetKey("left")) //©‚È‚çy•ûŒü‚É-3.0f‰ñ“]
        {
            transform.Rotate(0f, -3.0f, 0f);
        }
    }
}
