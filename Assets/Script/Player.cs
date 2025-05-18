using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float moveSpeed = 0.1f;  // �� �N���X���Œ�`�iStart�̊O�j
    private bool hasGimmick = false; // �M�~�b�N�擾�ς݂�

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60; // FPS��60�ɐݒ�
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("up")) //���Ȃ�z�����ɑO�i
        {
            transform.position -= transform.forward * moveSpeed;
        }
        if (Input.GetKey("down")) //���Ȃ�z�����Ɍ��
        {
            transform.position += transform.forward * moveSpeed;
        }
        if (Input.GetKey("right")) //���Ȃ�E��]
        {
            transform.Rotate(0f, 3.0f, 0f);
        }
        if (Input.GetKey("left")) //���Ȃ獶��]
        {
            transform.Rotate(0f, -3.0f, 0f);
        }
    }

    //���M�~�b�N��L���ɂ���֐�
    public void EnableGimmick()
    {
        if (!hasGimmick)
        {
            hasGimmick = true;
            

            // �F��ԂɕύX
            GetComponent<Renderer>().material.color = Color.red;
            
        }
    }

    //��
}
