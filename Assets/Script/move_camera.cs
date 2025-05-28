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
    public Quaternion hRotation;//プレイヤーとカメラの角度の同期

    public float mx; //マウスの横移動
    public float my; //マウスの縦移動

    bool justWarped = false;//ワープフラグ

    private void Start()
    {
        hRotation = Quaternion.identity;
        transform.position = player.transform.position + player.transform.forward * -2 + player.transform.up;
        transform.LookAt(player.transform);
        //最初のプレイヤーの位置の取得
        pastPos = player.transform.position;
        //最初の重力方向の取得
        warp wp = player.GetComponent<warp>();
        pastG = wp.gravityDirection / 9.81f;
    }
    void Update()
    {

        //------カメラの移動------

        //プレイヤーの現在地の取得
        currentPos = player.transform.position;

        //現在の重力方向の取得
        warp wp = player.GetComponent<warp>();
        currentG = wp.gravityDirection / 9.81f;
        
        if (justWarped)
        {
            hRotation = Quaternion.identity;
            //Maze1から移動
            if (pastG == Vector3.down)
            {
                if (currentG == Vector3.right) //Maze2に移動
                {
                    transform.position = player.transform.position + Vector3.down * 2 + Vector3.left;
                    transform.LookAt(player.transform);
                    Debug.Log("1to2");
                }
                else if (currentG == Vector3.left) //Maze4に移動
                {
                    transform.position = player.transform.position + Vector3.down * 2 + Vector3.right;
                    transform.LookAt(player.transform);
                    Debug.Log("1to4");
                }
                else if (currentG == Vector3.forward) //Maze5に移動
                {
                    transform.position = player.transform.position + Vector3.down * 2 + Vector3.back;
                    transform.LookAt(player.transform);
                    Debug.Log("1to5");
                }
                else if (currentG == Vector3.back) //Maze6に移動
                {
                    transform.position = player.transform.position + Vector3.down * 2 + Vector3.forward;
                    transform.LookAt(player.transform);
                    Debug.Log("1to6");
                }

            }
            //Maze2から移動
            if (pastG == Vector3.right)
            {
                if (currentG == Vector3.down) //Maze1に移動
                {
                    transform.position = player.transform.position + Vector3.right * 2 + Vector3.up;
                    transform.LookAt(player.transform);
                    hRotation *= Quaternion.Euler(0, 90, 0);
                    Debug.Log("2to1");
                }
                else if (currentG == Vector3.up) //Maze3に移動
                {
                    transform.position = player.transform.position + Vector3.right * 2 + Vector3.down;
                    transform.LookAt(player.transform);
                    transform.Rotate(0, 0, 180);
                    hRotation *= Quaternion.Euler(0, -90, 0);
                    Debug.Log("2to3");
                }
                else if (currentG == Vector3.forward) //Maze5に移動
                {
                    transform.position = player.transform.position + Vector3.right * 2 + Vector3.back;
                    transform.LookAt(player.transform);
                    transform.Rotate(0, 0, 90);
                    hRotation *= Quaternion.Euler(0, 0, 90);
                    Debug.Log("2to5");
                }
                else if (currentG == Vector3.back) //Maze6に移動
                {
                    transform.position = player.transform.position + Vector3.right * 2 + Vector3.forward;
                    transform.LookAt(player.transform);
                    transform.Rotate(0, 0, -90);
                    hRotation *= Quaternion.Euler(0, 0, 90);
                    Debug.Log("2to6");
                }
            }
            //Maze3から移動
            if (pastG == Vector3.up)
            {
                if (currentG == Vector3.right) //Maze2に移動
                {
                    transform.position = player.transform.position + Vector3.up * 2 + Vector3.left;
                    transform.LookAt(player.transform);
                    transform.Rotate(0, 0, 180);
                    hRotation *= Quaternion.Euler(180, 0, 0);
                    Debug.Log("3to2");
                }
                else if (currentG == Vector3.left) //Maze4に移動
                {
                    transform.position = player.transform.position + Vector3.up * 2 + Vector3.right;
                    transform.LookAt(player.transform);
                    transform.Rotate(0, 0, 180);
                    hRotation *= Quaternion.Euler(180, 0, 0);
                    Debug.Log("3to4");
                }
                else if (currentG == Vector3.forward) //Maze5に移動
                {
                    transform.position = player.transform.position + Vector3.up * 2 + Vector3.back;
                    transform.LookAt(player.transform);
                    transform.Rotate(0, 0, 180);
                    hRotation *= Quaternion.Euler(0, 0, 180);
                    Debug.Log("3to5");
                }
                else if (currentG == Vector3.back) //Maze6に移動
                {
                    transform.position = player.transform.position + Vector3.up * 2 + Vector3.forward;
                    transform.LookAt(player.transform);
                    transform.Rotate(0, 0, 180);
                    hRotation *= Quaternion.Euler(0, 0, 180);
                    Debug.Log("3to6");
                }
            }
            //Maze4から移動
            if (pastG == Vector3.left)
            {
                if (currentG == Vector3.down) //Maze1に移動
                {
                    transform.position = player.transform.position + Vector3.left * 2 + Vector3.up;
                    transform.LookAt(player.transform);
                    hRotation *= Quaternion.Euler(0, -90, 0);
                    Debug.Log("4to1");
                }
                else if (currentG == Vector3.up) //Maze3に移動
                {
                    transform.position = player.transform.position + Vector3.left * 2 + Vector3.down;
                    transform.LookAt(player.transform);
                    transform.Rotate(0, 0, 180);
                    hRotation *= Quaternion.Euler(0, 90, 0);
                    Debug.Log("4to3");
                }
                else if (currentG == Vector3.forward) //Maze5に移動
                {
                    transform.position = player.transform.position + Vector3.left * 2 + Vector3.back;
                    transform.LookAt(player.transform);
                    transform.Rotate(0, 0, -90);
                    hRotation *= Quaternion.Euler(0, 0, -90);
                    Debug.Log("4to5");
                }
                else if (currentG == Vector3.back) //Maze6に移動
                {
                    transform.position = player.transform.position + Vector3.left * 2 + Vector3.forward;
                    transform.LookAt(player.transform);
                    transform.Rotate(0, 0, 90);
                    hRotation *= Quaternion.Euler(0, 0, -90);
                    Debug.Log("4to6");
                }
            }
            //Maze5から移動
            if (pastG == Vector3.forward)
            {
                if (currentG == Vector3.down) //Maze1に移動
                {
                    transform.position = player.transform.position + Vector3.forward * 2 + Vector3.up;
                    transform.LookAt(player.transform);
                    Debug.Log("5to1");
                }
                else if (currentG == Vector3.right) //Maze2に移動
                {
                    transform.position = player.transform.position + Vector3.forward * 2 + Vector3.left;
                    transform.LookAt(player.transform);
                    transform.Rotate(0, 0, -90);
                    hRotation *= Quaternion.Euler(-90, 0, 0);
                    Debug.Log("5to2");
                }
                else if (currentG == Vector3.up) //Maze3に移動
                {
                    transform.position = player.transform.position + Vector3.forward * 2 + Vector3.down;
                    transform.LookAt(player.transform);
                    transform.Rotate(0, 0, 180);
                    hRotation *= Quaternion.Euler(0, 180, 0);
                    Debug.Log("5to3");
                }
                else if (currentG == Vector3.left) //Maze4に移動
                {
                    transform.position = player.transform.position + Vector3.forward * 2 + Vector3.right;
                    transform.LookAt(player.transform);
                    transform.Rotate(0, 0, 90);
                    hRotation *= Quaternion.Euler(-90, 0, 0);
                    Debug.Log("5to4");
                }
            }
            //Maze6から移動
            if (pastG == Vector3.back)
            {
                if (currentG == Vector3.down) //Maze1に移動
                {
                    transform.position = player.transform.position + Vector3.back * 2 + Vector3.up;
                    transform.LookAt(player.transform);
                    hRotation *= Quaternion.Euler(0, 180, 0);
                    Debug.Log("6to1");
                }
                else if (currentG == Vector3.right) //Maze2に移動
                {
                    transform.position = player.transform.position + Vector3.back * 2 + Vector3.left;
                    transform.LookAt(player.transform);
                    transform.Rotate(0, 0, 90);
                    hRotation *= Quaternion.Euler(90, 0, 0);
                    Debug.Log("6to2");
                }
                else if (currentG == Vector3.up) //Maze3に移動
                {
                    transform.position = player.transform.position + Vector3.back * 2 + Vector3.down;
                    transform.LookAt(player.transform);
                    transform.Rotate(0, 0, 180);
                    hRotation *= Quaternion.Euler(0, 0, 0);
                    Debug.Log("6to3");
                }
                else if (currentG == Vector3.left) //Maze4に移動
                {
                    transform.position = player.transform.position + Vector3.back * 2 + Vector3.right;
                    transform.LookAt(player.transform);
                    transform.Rotate(0, 0, -90);
                    hRotation *= Quaternion.Euler(90, 0, 0);
                    Debug.Log("6to4");
                }
            }
            justWarped = false;
            pastG = currentG;
        }
        else
        {
            {
                diff = currentPos - pastPos;
            }
        }

        transform.position = Vector3.Lerp(transform.position, transform.position + diff, 1.0f);//カメラをプレイヤーの移動差分だけうごかす


        pastPos = currentPos;
        diff = Vector3.zero;


        //------カメラの回転------

        // マウスの移動量を取得
        mx = Input.GetAxis("Mouse X") * 2.0f;
        my = Input.GetAxis("Mouse Y") * 1.5f;

        
        // X方向に一定量移動していれば横回転
        if (Mathf.Abs(mx) > 0.01f)
        {
            // 回転軸はプレイヤーに対して上向きの軸
            transform.RotateAround(player.transform.position, -currentG, mx);
        }

        // Y方向に一定量移動していれば縦回転
        if (Mathf.Abs(my) > 0.01f)
        {
            // 回転軸はカメラ自身のX軸
            transform.RotateAround(player.transform.position, -transform.right, my);

        }


        //デバッグ用
        //Debug.Log(transform.position);
        //Debug.Log(currentG == Vector3.right);
        //Debug.Log("forward = " + player.transform.forward);
        //Debug.Log("up" + player.transform.up);
    }

    //ワープフラグの更新
    public void SetJustWarped()
    {
        justWarped = true;
    }
}
