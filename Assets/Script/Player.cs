using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float moveSpeed = 0.1f;  // ← クラス内で定義（Startの外）
    private bool hasJump = false; // ギミック取得済みか
    private float jumpPower = 5.0f;
    private Rigidbody rb;
    private bool isGrounded = false; // ← 接地フラグ追加

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60; // FPSを60に設定
        rb = GetComponent<Rigidbody>();
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

        //★ジャンプギミック
        if (hasJump && isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }

    //★ジャンプギミック
    public void Jump()
    {
        if (!hasJump)
        {
            hasJump = true;
            GetComponent<Renderer>().material.color = Color.red;

            // 回転をすべて固定（転倒防止）
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            // ジャンプ時間制限コルーチン開始（例：10秒）
            StartCoroutine(JumpTimeLimit(10f));

        }
    }

    // ★ジャンプができる時間を制限
    private IEnumerator JumpTimeLimit(float duration)
    {
        yield return new WaitForSeconds(duration);

        hasJump = false;
        GetComponent<Renderer>().material.color = Color.white; // 元の色に戻すなど
    }

    // ★ 接地判定（地面との接触を確認）
    void OnCollisionStay(Collision collision)
    {
        // 地面と接触しているときだけtrue（Layerやタグで絞ってもOK）
        isGrounded = true;
    }

    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

    //★タイム増減
    public void AddTime(float timeDelta)
    {
        GetComponent<Renderer>().material.color = Color.blue;
        time_attack ta = FindObjectOfType<time_attack>();
        if (ta != null)
        {

            ta.AddTimeFromItem(timeDelta);
        }
    }

}
