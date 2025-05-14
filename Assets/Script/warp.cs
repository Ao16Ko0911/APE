using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class warp : MonoBehaviour
{
    public Vector3 gravityDirection = new Vector3(0.0f, -9.81f, 0.0f); //�d�͂̌���
    private Rigidbody rb;
    public Transform maze1,maze2,maze3,maze4,maze5,maze6; 


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; //�d�͂𖳌��ɂ���
    }

    void FixedUpdate()
    {
        rb.AddForce(gravityDirection, ForceMode.Acceleration); //�d�͂�������
    }

    // Update is called once per frame
    void Update()
    {

        
    }
    void OnCollisionEnter(Collision other)
    {
        if ((other.gameObject.name == "Warp A1 to A4") ||(other.gameObject.name == "Warp C1 to C4")) //maze1����maze2�փ��[�v
        {
            gravityDirection = new Vector3(-9.81f, 0.0f, 0.0f); //�d�͂̌��������ɖ߂�
            Vector3 localOffset = new Vector3(22.5f, 1.0f, 22.5f); //���[�v��̃I�t�Z�b�g
            transform.rotation = maze4.rotation; //���[�v��̉�]���擾
            transform.position = maze4.TransformPoint(localOffset); //���[�v��̍��W          
        }
    }
}
