using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_camera : MonoBehaviour
{
    [SerializeField] GameObject player;

    Vector3 currentPos;//現在のカメラ位置
    Vector3 pastPos;//過去のカメラ位置

    Vector3 currentG;//現在の重力方向
    Vector3 pastG;//過去の重力方向

    Vector3 diff;//移動距離
    [SerializeField] public Quaternion hRotation;      // カメラの水平回転

    public float mx; //マウスの横移動
    public float my; //マウスの縦移動

    private void Start()
    {

        transform.LookAt(player.transform);
        //回転の初期化
        hRotation = Quaternion.identity;
        //最初のプレイヤーの位置の取得
        pastPos = player.transform.position;
        //最初の重力方向の取得
        warp wp = player.GetComponent<warp>();
        pastG = wp.gravityDirection / -9.81f;
    }
    void Update()
    {

        //------カメラの移動------

        //プレイヤーの現在地の取得
        currentPos = player.transform.position;

        //現在の重力方向の取得
        warp wp = player.GetComponent<warp>();
        currentG = wp.gravityDirection / -9.81f;

        //Debug.Log("forward = " + player.transform.forward);
        //Debug.Log("up" + player.transform.up);


        if (currentG != pastG)
        {
            Debug.Log(currentG);
            
            transform.position = player.transform.position + player.transform.forward * -2f + player.transform.up * 1.5f;
           
            transform.LookAt(player.transform);
        }
        else
        {
            diff = currentPos - pastPos;

            transform.position = Vector3.Lerp(transform.position, transform.position + diff, 1.0f);//カメラをプレイヤーの移動差分だけうごかす
        }

        pastPos = currentPos;
        pastG = currentG;


        //------カメラの回転------

        // マウスの移動量を取得
        mx = Input.GetAxis("Mouse X") * 2.0f;
        my = Input.GetAxis("Mouse Y") * 1.5f;

        hRotation *= Quaternion.Euler(0, mx, 0);

        if(currentG == Vector3.left || currentG == Vector3.right) {
            // X方向に一定量移動していれば横回転
            if (Mathf.Abs(mx) > 0.01f)
            {
                // 回転軸はプレイヤーに対して上向きの軸
                transform.RotateAround(player.transform.position, Vector3.down, mx);
            }

            // Y方向に一定量移動していれば縦回転
            if (Mathf.Abs(my) > 0.01f)
            {
                // 回転軸はカメラ自身のX軸
                transform.RotateAround(player.transform.position, transform.right, -my);
            }
        }
        // X方向に一定量移動していれば横回転
        if (Mathf.Abs(mx) > 0.01f)
        {
            // 回転軸はプレイヤーに対して上向きの軸
            transform.RotateAround(player.transform.position, currentG, mx);
        }

        // Y方向に一定量移動していれば縦回転
        if (Mathf.Abs(my) > 0.01f)
        {
            // 回転軸はカメラ自身のX軸
            transform.RotateAround(player.transform.position, transform.right, -my);
        }
        Debug.Log(transform.position);
        
    }
}
