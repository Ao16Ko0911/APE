using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_wall : MonoBehaviour
{
    Transform myTransform; //transformî•ñ‚ğŠi”[‚·‚é•Ï”
    Vector3 position_start;      //•¨‘Ì‚ÌˆÊ’u‚ğŠi”[‚·‚é•Ï”
    Vector3 position_now; //•¨‘Ì‚ÌˆÊ’u‚ğŠi”[‚·‚é•Ï”

    // Start is called before the first frame update
    void Start()
    {
        myTransform = this.transform; //•¨‘Ì‚Ìtransformî•ñ‚ğæ“¾
        position_start = myTransform.position; //•¨‘Ì‚ÌˆÊ’u‚ğæ“¾
        position_now = position_start; //•¨‘Ì‚ÌˆÊ’u‚ğæ“¾
    }

    // Update is called once per frame
    void Update()
    {
        position_now.x += 0.02f;
        if(position_start.x - position_now.x < -3.5)
        {
            position_now = position_start; //•¨‘Ì‚ÌˆÊ’u‚ğ‰ŠúˆÊ’u‚É–ß‚·
        }
        myTransform.position = position_now; 
    }
}
