using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class warp : MonoBehaviour
{
    public Vector3 gravityDirection = new Vector3(0.0f, -9.81f, 0.0f); //重力の向き
    private Rigidbody rb;
    public Transform maze1,maze2,maze3,maze4,maze5,maze6; 


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; //重力を無効にする
    }

    void FixedUpdate()
    {
        rb.AddForce(gravityDirection, ForceMode.Acceleration); //重力を加える
    }

    // Update is called once per frame
    void Update()
    {

        
    }
    void OnCollisionEnter(Collision other)
    {
        if ((other.gameObject.name == "Warp A1 to A4") ||(other.gameObject.name == "Warp C1 to C4")) //maze1からmaze2へワープ
        {
            gravityDirection = new Vector3(-9.81f, 0.0f, 0.0f); //重力の向きを元に戻す
            Vector3 localOffset = new Vector3(22.5f, 1.0f, 22.5f); //ワープ先のオフセット
            transform.rotation = maze4.rotation; //ワープ先の回転を取得
            transform.position = maze4.TransformPoint(localOffset); //ワープ先の座標          
        }
    }
}
