using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_wall2 : MonoBehaviour
{
    Transform myTransform; //transform情報を格納する変数
    Vector3 position_start;      //初期位置
    Vector3 position_now;   //現在地
    float speed = 0.02f; //移動速度
    bool movingLeft = true; //移動方向が左かどうか

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
        if (movingLeft)
        {
            position_now.x -= speed;
            if (position_now.x - position_start.x <= -3.5f) // 初期位置から+3.5f進んだら
            {
                movingLeft = false;
            }
        }
        else
        {
            position_now.x += speed;
            if (position_now.x - position_start.x >= 0f) // 初期位置に戻ったら
            {
                movingLeft = true;
            }
        }
        myTransform.position = position_now; //物体の位置を更新
    }
}
