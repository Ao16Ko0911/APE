using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float moveSpeed = 0.1f;  // ← クラス内で定義（Startの外）
    private bool hasGimmick = false; // ギミック取得済みか

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60; // FPSを60に設定
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("up")) //↑ならz方向に前進
        {
            transform.position -= transform.forward * moveSpeed;
        }
        if (Input.GetKey("down")) //↓ならz方向に後退
        {
            transform.position += transform.forward * moveSpeed;
        }
        if (Input.GetKey("right")) //→なら右回転
        {
            transform.Rotate(0f, 3.0f, 0f);
        }
        if (Input.GetKey("left")) //←なら左回転
        {
            transform.Rotate(0f, -3.0f, 0f);
        }
    }

    //★ギミックを有効にする関数
    public void EnableGimmick()
    {
        if (!hasGimmick)
        {
            hasGimmick = true;
            

            // 色を赤に変更
            GetComponent<Renderer>().material.color = Color.red;
            
        }
    }

    //★
}
