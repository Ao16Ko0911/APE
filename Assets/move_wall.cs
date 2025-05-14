using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_wall : MonoBehaviour
{
    Transform myTransform; //transform�����i�[����ϐ�
    Vector3 position_start;      //���̂̈ʒu���i�[����ϐ�
    Vector3 position_now; //���̂̈ʒu���i�[����ϐ�

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
        position_now.x += 0.02f;
        if(position_start.x - position_now.x < -3.5)
        {
            position_now = position_start; //���̂̈ʒu�������ʒu�ɖ߂�
        }
        myTransform.position = position_now; 
    }
}
