using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_camera : MonoBehaviour
{
    [SerializeField] GameObject player;

    Vector3 currentPos;//現在のカメラ位置
    Vector3 pastPos;//過去のカメラ位置

    Vector3 diff;//移動距離
    [SerializeField] public Quaternion hRotation;      // カメラの水平回転

    private void Start()
    {
        //回転の初期化
        hRotation = Quaternion.identity;                // 水平回転(Y軸を軸とする回転)は、無回転
        //最初のプレイヤーの位置の取得
        pastPos = player.transform.position;
    }
    void Update()
    {
        //------カメラの移動------

        //プレイヤーの現在地の取得
        currentPos = player.transform.position;

        diff = currentPos - pastPos;

        transform.position = Vector3.Lerp(transform.position, transform.position + diff, 1.0f);//カメラをプレイヤーの移動差分だけうごかす

        pastPos = currentPos;


        //------カメラの回転------

        // マウスの移動量を取得
        float mx = Input.GetAxis("Mouse X") * 2.0f;
        float my = Input.GetAxis("Mouse Y") * 1.5f;

        hRotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * 2.0f, 0);

        // X方向に一定量移動していれば横回転
        if (Mathf.Abs(mx) > 0.01f)
        {
            // 回転軸はワールド座標のY軸
            transform.RotateAround(player.transform.position, Vector3.up, mx);
        }

        // Y方向に一定量移動していれば縦回転
        if (Mathf.Abs(my) > 0.01f)
        {
            // 回転軸はカメラ自身のX軸
            transform.RotateAround(player.transform.position, transform.right, -my);
        }
    }
}
