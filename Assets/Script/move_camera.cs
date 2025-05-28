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
    public Quaternion hRotation;//�v���C���[�ƃJ�����̊p�x�̓���

    public float mx; //�}�E�X�̉��ړ�
    public float my; //�}�E�X�̏c�ړ�

    bool justWarped = false;//���[�v�t���O

    private void Start()
    {
        hRotation = Quaternion.identity;
        transform.position = player.transform.position + player.transform.forward * -2 + player.transform.up;
        transform.LookAt(player.transform);
        //�ŏ��̃v���C���[�̈ʒu�̎擾
        pastPos = player.transform.position;
        //�ŏ��̏d�͕����̎擾
        warp wp = player.GetComponent<warp>();
        pastG = wp.gravityDirection / 9.81f;
    }
    void Update()
    {

        //------�J�����̈ړ�------

        //�v���C���[�̌��ݒn�̎擾
        currentPos = player.transform.position;

        //���݂̏d�͕����̎擾
        warp wp = player.GetComponent<warp>();
        currentG = wp.gravityDirection / 9.81f;
        
        if (justWarped)
        {
            hRotation = Quaternion.identity;
            //Maze1����ړ�
            if (pastG == Vector3.down)
            {
                if (currentG == Vector3.right) //Maze2�Ɉړ�
                {
                    transform.position = player.transform.position + Vector3.down * 2 + Vector3.left;
                    transform.LookAt(player.transform);
                    Debug.Log("1to2");
                }
                else if (currentG == Vector3.left) //Maze4�Ɉړ�
                {
                    transform.position = player.transform.position + Vector3.down * 2 + Vector3.right;
                    transform.LookAt(player.transform);
                    Debug.Log("1to4");
                }
                else if (currentG == Vector3.forward) //Maze5�Ɉړ�
                {
                    transform.position = player.transform.position + Vector3.down * 2 + Vector3.back;
                    transform.LookAt(player.transform);
                    Debug.Log("1to5");
                }
                else if (currentG == Vector3.back) //Maze6�Ɉړ�
                {
                    transform.position = player.transform.position + Vector3.down * 2 + Vector3.forward;
                    transform.LookAt(player.transform);
                    Debug.Log("1to6");
                }

            }
            //Maze2����ړ�
            if (pastG == Vector3.right)
            {
                if (currentG == Vector3.down) //Maze1�Ɉړ�
                {
                    transform.position = player.transform.position + Vector3.right * 2 + Vector3.up;
                    transform.LookAt(player.transform);
                    hRotation *= Quaternion.Euler(0, 90, 0);
                    Debug.Log("2to1");
                }
                else if (currentG == Vector3.up) //Maze3�Ɉړ�
                {
                    transform.position = player.transform.position + Vector3.right * 2 + Vector3.down;
                    transform.LookAt(player.transform);
                    transform.Rotate(0, 0, 180);
                    hRotation *= Quaternion.Euler(0, -90, 0);
                    Debug.Log("2to3");
                }
                else if (currentG == Vector3.forward) //Maze5�Ɉړ�
                {
                    transform.position = player.transform.position + Vector3.right * 2 + Vector3.back;
                    transform.LookAt(player.transform);
                    transform.Rotate(0, 0, 90);
                    hRotation *= Quaternion.Euler(0, 0, 90);
                    Debug.Log("2to5");
                }
                else if (currentG == Vector3.back) //Maze6�Ɉړ�
                {
                    transform.position = player.transform.position + Vector3.right * 2 + Vector3.forward;
                    transform.LookAt(player.transform);
                    transform.Rotate(0, 0, -90);
                    hRotation *= Quaternion.Euler(0, 0, 90);
                    Debug.Log("2to6");
                }
            }
            //Maze3����ړ�
            if (pastG == Vector3.up)
            {
                if (currentG == Vector3.right) //Maze2�Ɉړ�
                {
                    transform.position = player.transform.position + Vector3.up * 2 + Vector3.left;
                    transform.LookAt(player.transform);
                    transform.Rotate(0, 0, 180);
                    hRotation *= Quaternion.Euler(180, 0, 0);
                    Debug.Log("3to2");
                }
                else if (currentG == Vector3.left) //Maze4�Ɉړ�
                {
                    transform.position = player.transform.position + Vector3.up * 2 + Vector3.right;
                    transform.LookAt(player.transform);
                    transform.Rotate(0, 0, 180);
                    hRotation *= Quaternion.Euler(180, 0, 0);
                    Debug.Log("3to4");
                }
                else if (currentG == Vector3.forward) //Maze5�Ɉړ�
                {
                    transform.position = player.transform.position + Vector3.up * 2 + Vector3.back;
                    transform.LookAt(player.transform);
                    transform.Rotate(0, 0, 180);
                    hRotation *= Quaternion.Euler(0, 0, 180);
                    Debug.Log("3to5");
                }
                else if (currentG == Vector3.back) //Maze6�Ɉړ�
                {
                    transform.position = player.transform.position + Vector3.up * 2 + Vector3.forward;
                    transform.LookAt(player.transform);
                    transform.Rotate(0, 0, 180);
                    hRotation *= Quaternion.Euler(0, 0, 180);
                    Debug.Log("3to6");
                }
            }
            //Maze4����ړ�
            if (pastG == Vector3.left)
            {
                if (currentG == Vector3.down) //Maze1�Ɉړ�
                {
                    transform.position = player.transform.position + Vector3.left * 2 + Vector3.up;
                    transform.LookAt(player.transform);
                    hRotation *= Quaternion.Euler(0, -90, 0);
                    Debug.Log("4to1");
                }
                else if (currentG == Vector3.up) //Maze3�Ɉړ�
                {
                    transform.position = player.transform.position + Vector3.left * 2 + Vector3.down;
                    transform.LookAt(player.transform);
                    transform.Rotate(0, 0, 180);
                    hRotation *= Quaternion.Euler(0, 90, 0);
                    Debug.Log("4to3");
                }
                else if (currentG == Vector3.forward) //Maze5�Ɉړ�
                {
                    transform.position = player.transform.position + Vector3.left * 2 + Vector3.back;
                    transform.LookAt(player.transform);
                    transform.Rotate(0, 0, -90);
                    hRotation *= Quaternion.Euler(0, 0, -90);
                    Debug.Log("4to5");
                }
                else if (currentG == Vector3.back) //Maze6�Ɉړ�
                {
                    transform.position = player.transform.position + Vector3.left * 2 + Vector3.forward;
                    transform.LookAt(player.transform);
                    transform.Rotate(0, 0, 90);
                    hRotation *= Quaternion.Euler(0, 0, -90);
                    Debug.Log("4to6");
                }
            }
            //Maze5����ړ�
            if (pastG == Vector3.forward)
            {
                if (currentG == Vector3.down) //Maze1�Ɉړ�
                {
                    transform.position = player.transform.position + Vector3.forward * 2 + Vector3.up;
                    transform.LookAt(player.transform);
                    Debug.Log("5to1");
                }
                else if (currentG == Vector3.right) //Maze2�Ɉړ�
                {
                    transform.position = player.transform.position + Vector3.forward * 2 + Vector3.left;
                    transform.LookAt(player.transform);
                    transform.Rotate(0, 0, -90);
                    hRotation *= Quaternion.Euler(-90, 0, 0);
                    Debug.Log("5to2");
                }
                else if (currentG == Vector3.up) //Maze3�Ɉړ�
                {
                    transform.position = player.transform.position + Vector3.forward * 2 + Vector3.down;
                    transform.LookAt(player.transform);
                    transform.Rotate(0, 0, 180);
                    hRotation *= Quaternion.Euler(0, 180, 0);
                    Debug.Log("5to3");
                }
                else if (currentG == Vector3.left) //Maze4�Ɉړ�
                {
                    transform.position = player.transform.position + Vector3.forward * 2 + Vector3.right;
                    transform.LookAt(player.transform);
                    transform.Rotate(0, 0, 90);
                    hRotation *= Quaternion.Euler(-90, 0, 0);
                    Debug.Log("5to4");
                }
            }
            //Maze6����ړ�
            if (pastG == Vector3.back)
            {
                if (currentG == Vector3.down) //Maze1�Ɉړ�
                {
                    transform.position = player.transform.position + Vector3.back * 2 + Vector3.up;
                    transform.LookAt(player.transform);
                    hRotation *= Quaternion.Euler(0, 180, 0);
                    Debug.Log("6to1");
                }
                else if (currentG == Vector3.right) //Maze2�Ɉړ�
                {
                    transform.position = player.transform.position + Vector3.back * 2 + Vector3.left;
                    transform.LookAt(player.transform);
                    transform.Rotate(0, 0, 90);
                    hRotation *= Quaternion.Euler(90, 0, 0);
                    Debug.Log("6to2");
                }
                else if (currentG == Vector3.up) //Maze3�Ɉړ�
                {
                    transform.position = player.transform.position + Vector3.back * 2 + Vector3.down;
                    transform.LookAt(player.transform);
                    transform.Rotate(0, 0, 180);
                    hRotation *= Quaternion.Euler(0, 0, 0);
                    Debug.Log("6to3");
                }
                else if (currentG == Vector3.left) //Maze4�Ɉړ�
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

        transform.position = Vector3.Lerp(transform.position, transform.position + diff, 1.0f);//�J�������v���C���[�̈ړ�����������������


        pastPos = currentPos;
        diff = Vector3.zero;


        //------�J�����̉�]------

        // �}�E�X�̈ړ��ʂ��擾
        mx = Input.GetAxis("Mouse X") * 2.0f;
        my = Input.GetAxis("Mouse Y") * 1.5f;

        
        // X�����Ɉ��ʈړ����Ă���Ή���]
        if (Mathf.Abs(mx) > 0.01f)
        {
            // ��]���̓v���C���[�ɑ΂��ď�����̎�
            transform.RotateAround(player.transform.position, -currentG, mx);
        }

        // Y�����Ɉ��ʈړ����Ă���Ώc��]
        if (Mathf.Abs(my) > 0.01f)
        {
            // ��]���̓J�������g��X��
            transform.RotateAround(player.transform.position, -transform.right, my);

        }


        //�f�o�b�O�p
        //Debug.Log(transform.position);
        //Debug.Log(currentG == Vector3.right);
        //Debug.Log("forward = " + player.transform.forward);
        //Debug.Log("up" + player.transform.up);
    }

    //���[�v�t���O�̍X�V
    public void SetJustWarped()
    {
        justWarped = true;
    }
}
