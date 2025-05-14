using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_wall2 : MonoBehaviour
{
    Transform myTransform; //transform�����i�[����ϐ�
    Vector3 position_start;      //�����ʒu
    Vector3 position_now;   //���ݒn
    float speed = 0.02f; //�ړ����x
    bool movingLeft = true; //�ړ������������ǂ���

    // Start is called before the first frame update
    void Start()
    {
        myTransform = this.transform; //���̂�transform�����擾
        position_start = myTransform.position; //���̂̈ʒu���擾
        position_now = position_start; //���̂̈ʒu���擾   
    }

    // Update is called once per frame
    void Update()
    {
        if (movingLeft)
        {
            position_now.x -= speed;
            if (position_now.x - position_start.x <= -3.5f) // �����ʒu����+3.5f�i�񂾂�
            {
                movingLeft = false;
            }
        }
        else
        {
            position_now.x += speed;
            if (position_now.x - position_start.x >= 0f) // �����ʒu�ɖ߂�����
            {
                movingLeft = true;
            }
        }
        myTransform.position = position_now; //���̂̈ʒu���X�V
    }
}
