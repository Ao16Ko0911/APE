using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_camera : MonoBehaviour
{
    [SerializeField] GameObject player;

    Vector3 currentPos;//���݂̃J�����ʒu
    Vector3 pastPos;//�ߋ��̃J�����ʒu

    Vector3 currentG;//���݂̏d�͕���
    Vector3 pastG;//�ߋ��̏d�͕���

    Vector3 diff;//�ړ�����
    [SerializeField] public Quaternion hRotation;      // �J�����̐�����]

    public float mx; //�}�E�X�̉��ړ�
    public float my; //�}�E�X�̏c�ړ�

    private void Start()
    {

        transform.LookAt(player.transform);
        //��]�̏�����
        hRotation = Quaternion.identity;
        //�ŏ��̃v���C���[�̈ʒu�̎擾
        pastPos = player.transform.position;
        //�ŏ��̏d�͕����̎擾
        warp wp = player.GetComponent<warp>();
        pastG = wp.gravityDirection / -9.81f;
    }
    void Update()
    {

        //------�J�����̈ړ�------

        //�v���C���[�̌��ݒn�̎擾
        currentPos = player.transform.position;

        //���݂̏d�͕����̎擾
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

            transform.position = Vector3.Lerp(transform.position, transform.position + diff, 1.0f);//�J�������v���C���[�̈ړ�����������������
        }

        pastPos = currentPos;
        pastG = currentG;


        //------�J�����̉�]------

        // �}�E�X�̈ړ��ʂ��擾
        mx = Input.GetAxis("Mouse X") * 2.0f;
        my = Input.GetAxis("Mouse Y") * 1.5f;

        hRotation *= Quaternion.Euler(0, mx, 0);

        if(currentG == Vector3.left || currentG == Vector3.right) {
            // X�����Ɉ��ʈړ����Ă���Ή���]
            if (Mathf.Abs(mx) > 0.01f)
            {
                // ��]���̓v���C���[�ɑ΂��ď�����̎�
                transform.RotateAround(player.transform.position, Vector3.down, mx);
            }

            // Y�����Ɉ��ʈړ����Ă���Ώc��]
            if (Mathf.Abs(my) > 0.01f)
            {
                // ��]���̓J�������g��X��
                transform.RotateAround(player.transform.position, transform.right, -my);
            }
        }
        // X�����Ɉ��ʈړ����Ă���Ή���]
        if (Mathf.Abs(mx) > 0.01f)
        {
            // ��]���̓v���C���[�ɑ΂��ď�����̎�
            transform.RotateAround(player.transform.position, currentG, mx);
        }

        // Y�����Ɉ��ʈړ����Ă���Ώc��]
        if (Mathf.Abs(my) > 0.01f)
        {
            // ��]���̓J�������g��X��
            transform.RotateAround(player.transform.position, transform.right, -my);
        }
        Debug.Log(transform.position);
        
    }
}
