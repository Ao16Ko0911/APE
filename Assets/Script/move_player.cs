using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_player : MonoBehaviour
{
    [SerializeField] private Vector3 velocity;              // 移動方向
    [SerializeField] private float moveSpeed = 5.0f;        // 移動速度
    [SerializeField] private move_camera refCamera;  // カメラの水平回転を参照する用
    [SerializeField] private float applySpeed = 0.2f; //回転速度
    [SerializeField] private float buff;

    void Start() {
        buff = 1;
    }

    void Update()
    {
        // WASD入力から、XZ平面(水平な地面)を移動する方向(velocity)を得ます
        velocity = Vector3.zero;
        if (Input.GetKey(KeyCode.W) || Input.GetKey("up"))
            velocity.z -= 1 * buff;
        if (Input.GetKey(KeyCode.A) || Input.GetKey("left"))
            velocity.x += 1 * buff;
        if (Input.GetKey(KeyCode.S) || Input.GetKey("down"))
            velocity.z += 1 * buff;
        if (Input.GetKey(KeyCode.D) || Input.GetKey("right"))
            velocity.x -= 1 * buff;

        // 速度ベクトルの長さを1秒でmoveSpeedだけ進むように調整
        velocity = velocity.normalized * moveSpeed * Time.deltaTime;

        // いずれかの方向に移動している場合
        if (velocity.magnitude > 0)
        {
            // プレイヤーの回転(transform.rotation)の更新
            // 無回転状態のプレイヤーのZ+方向(後頭部)を、
            // カメラの水平回転(refCamera.hRotation)で回した移動の反対方向(-velocity)に回す回転に段々近づける
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                  Quaternion.LookRotation(refCamera.hRotation * -velocity),
                                                  applySpeed);

            // プレイヤーの位置(transform.position)の更新
            // カメラの水平回転(refCamera.hRotation)で回した移動方向(velocity)を足し込み
            transform.position += refCamera.hRotation * velocity;
        }
    }

}

