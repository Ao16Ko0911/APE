using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_wall : MonoBehaviour
{
    Transform myTransform; //transform情報を格納する変数
    Vector3 position_start;      //物体の位置を格納する変数
    Vector3 position_now; //物体の位置を格納する変数

    // Start is called before the first frame update
    void Start()
    {
        myTransform = this.transform; //物体のtransform情報を取得
        position_start = myTransform.position; //物体の位置を取得
        position_now = position_start; //物体の位置を取得
    }

    // Update is called once per frame
    void Update()
    {
        position_now.x += 0.02f;
        if(position_start.x - position_now.x < -3.5)
        {
            position_now = position_start; //物体の位置を初期位置に戻す
        }
        myTransform.position = position_now; 
    }
}
