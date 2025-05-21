using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class move_player : MonoBehaviour
{

    [SerializeField] private Vector3 velocity;              // 移動方向
    [SerializeField] private float moveSpeed = 5.0f;        // 移動速度
    [SerializeField] private move_camera refCamera;  // カメラの回転を参照する用
    [SerializeField] private float applySpeed = 0.2f; //回転速度
    [SerializeField] private float buff;

    private Quaternion hRotation;
    private Vector3 pastG;

    void Start() {
        hRotation = Quaternion.identity;
        //最初の重力方向の取得
        warp wp = GetComponent<warp>();
        pastG = wp.gravityDirection / -9.81f;
    }

    void Update()
    {
        //速度ベクトルの初期化
        velocity = Vector3.zero;

        warp wp = GetComponent<warp>();
        Vector3 curG = wp.gravityDirection / 9.81f;
        if(curG != pastG )
        {
            hRotation = Quaternion.identity;
        }
        if (curG == Vector3.down) //Maze1
        {
            //重力方向と， WASD入力か方向キーから、XZ平面(水平な地面)を移動する方向(velocity)を得る
            if (Input.GetKey(KeyCode.W) || Input.GetKey("up"))
                velocity.z -= 1 * buff;
            if (Input.GetKey(KeyCode.A) || Input.GetKey("left"))
                velocity.x += 1 * buff;
            if (Input.GetKey(KeyCode.S) || Input.GetKey("down"))
                velocity.z += 1 * buff;
            if (Input.GetKey(KeyCode.D) || Input.GetKey("right"))
                velocity.x -= 1 * buff;

            hRotation *= Quaternion.Euler(0, refCamera.mx, 0);
        }
        else if (curG == Vector3.left) //Maze4
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey("up"))
                velocity.y += 1 * buff;
            if (Input.GetKey(KeyCode.A) || Input.GetKey("left"))
                velocity.z -= 1 * buff;
            if (Input.GetKey(KeyCode.S) || Input.GetKey("down"))
                velocity.y -= 1 * buff;
            if (Input.GetKey(KeyCode.D) || Input.GetKey("right"))
                velocity.z += 1 * buff;

            hRotation *= Quaternion.Euler(refCamera.mx, 0, 0);
        }
        else if (curG == Vector3.forward) //Maze5
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey("up"))
                velocity.y += 1 * buff;
            if (Input.GetKey(KeyCode.A) || Input.GetKey("left"))
                velocity.x -= 1 * buff;
            if (Input.GetKey(KeyCode.S) || Input.GetKey("down"))
                velocity.y -= 1 * buff;
            if (Input.GetKey(KeyCode.D) || Input.GetKey("right"))
                velocity.x += 1 * buff;

            hRotation *= Quaternion.Euler(0, 0, -refCamera.mx);
        }
        
        // 速度ベクトルの長さを1秒でmoveSpeedだけ進むように調整
        velocity = velocity.normalized * moveSpeed * Time.deltaTime;

       
        
        // いずれかの方向に移動している場合
        if (velocity.magnitude > 0)
        {
            // プレイヤーの回転(transform.rotation)の更新
            // 無回転状態のプレイヤーのZ+方向(後頭部)を、
            // カメラの水平回転(refCamera.hRotation)で回した移動の反対方向(-velocity)に回す回転に段々近づける
            //transform.rotation = Quaternion.Slerp(transform.rotation,
            //                                       Quaternion.LookRotation(hRotation * -velocity),
            //                                       applySpeed);
            transform.rotation = Quaternion.LookRotation(hRotation * velocity);
        }
        
        // プレイヤーの位置(transform.position)の更新
        // カメラの水平回転(refCamera.hRotation)で回した移動方向(velocity)を足し込み
        transform.position += hRotation * velocity;

        pastG = curG;
    }

}

