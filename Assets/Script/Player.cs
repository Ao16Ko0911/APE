/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    [SerializeField] private Vector3 velocity;              // 移動方向
    [SerializeField] private float moveSpeed = 5.0f;        // 移動速度
    [SerializeField] private move_camera refCamera;  // カメラの回転を参照する用
    [SerializeField] private float applySpeed = 0.2f; //回転速度
    [SerializeField] private float buff;

    //ギミック系
    private bool hasJump = false; // ギミック取得済みか
    private float jumpPower = 5.0f;
    private Rigidbody rb;
    private bool isGrounded = false; // ← 接地フラグ追加

    private Quaternion hRotation;
    private Vector3 pastG;

    void Start()
    {
        hRotation = Quaternion.identity;
        rb = GetComponent<Rigidbody>();
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
        if (curG != pastG)
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

        if (hasJump && isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }

    // ★ジャンプ付与（外部から呼ばれる）
    public void Jump()
    {
        if (!hasJump)
        {
            hasJump = true;
            GetComponent<Renderer>().material.color = Color.red;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            StartCoroutine(JumpTimeLimit(10f));
        }
    }

    private IEnumerator JumpTimeLimit(float duration)
    {
        yield return new WaitForSeconds(duration);
        hasJump = false;
        GetComponent<Renderer>().material.color = Color.white;
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

    // ★タイム増減（外部から呼ばれる）
    public void AddTime(float timeDelta)
    {
        GetComponent<Renderer>().material.color = Color.blue;
        time_attack ta = FindObjectOfType<time_attack>();
        if (ta != null)
        {
            ta.AddTimeFromItem(timeDelta);
        }
    }

    public void Speed(float multiplier, float duration)
    {
        StartCoroutine(SpeedBuff(multiplier, duration));
    }

    private IEnumerator SpeedBuff(float multiplier, float duration)
    {
        float originalSpeed = moveSpeed;
        moveSpeed *= multiplier;
        GetComponent<Renderer>().material.color = Color.gray; // 色でバフを表現
        yield return new WaitForSeconds(duration);
        moveSpeed = originalSpeed;
        GetComponent<Renderer>().material.color = Color.white;
    }

    public void Reduce(float factor, float duration)
    {
        StartCoroutine(SpeedDebuffRoutine(factor, duration));
    }

    private IEnumerator SpeedDebuffRoutine(float factor, float duration)
    {
        float originalSpeed = moveSpeed;
        moveSpeed *= factor; // 例: factor = 0.5f で半分の速度に
        GetComponent<Renderer>().material.color = Color.cyan; // 色を水色に
        yield return new WaitForSeconds(duration);
        moveSpeed = originalSpeed;
        GetComponent<Renderer>().material.color = Color.white; // 元の色に戻す
    }


}
*/