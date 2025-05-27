using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class move_player : MonoBehaviour
{

    [SerializeField] private Vector3 velocity;              // �ړ�����
    [SerializeField] private float moveSpeed = 5.0f;        // �ړ����x
    [SerializeField] private move_camera refCamera;  // �J�����̉�]���Q�Ƃ���p
    //[SerializeField] private float applySpeed = 0.2f; //��]���x
    [SerializeField] private float buff; //�����E�����o�t

    private Quaternion hRotation;
    private Vector3 pastG;
    private bool warp_flag;

    void Start() {
        hRotation = Quaternion.identity;
        //�ŏ��̏d�͕����̎擾
        warp wp = GetComponent<warp>();
        pastG = wp.gravityDirection / 9.81f;
    }

    void Update()
    {
        //���x�x�N�g���̏�����
        velocity = Vector3.zero;

        warp wp = GetComponent<warp>();
        Vector3 curG = wp.gravityDirection / 9.81f;
        //�d�͕����ƁC WASD���͂������L�[����A�����Ȓn�ʂ��ړ��������(velocity)�𓾂�
        if (curG == Vector3.down) //Maze1
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey("up"))
                velocity.z += 1 * buff;
            if (Input.GetKey(KeyCode.A) || Input.GetKey("left"))
                velocity.x -= 1 * buff;
            if (Input.GetKey(KeyCode.S) || Input.GetKey("down"))
                velocity.z -= 1 * buff;
            if (Input.GetKey(KeyCode.D) || Input.GetKey("right"))
                velocity.x += 1 * buff;

            refCamera.hRotation *= Quaternion.Euler(0, refCamera.mx, 0);
        }
        else if (curG == Vector3.right) //Maze2
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey("up"))
                velocity.y += 1 * buff;
            if (Input.GetKey(KeyCode.A) || Input.GetKey("left"))
                velocity.z += 1 * buff;
            if (Input.GetKey(KeyCode.S) || Input.GetKey("down"))
                velocity.y -= 1 * buff;
            if (Input.GetKey(KeyCode.D) || Input.GetKey("right"))
                velocity.z -= 1 * buff;

            refCamera.hRotation *= Quaternion.Euler(-refCamera.mx, 0, 0);
        }
        else if (curG == Vector3.up) //Maze3
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey("up"))
                velocity.z += 1 * buff;
            if (Input.GetKey(KeyCode.A) || Input.GetKey("left"))
                velocity.x += 1 * buff;
            if (Input.GetKey(KeyCode.S) || Input.GetKey("down"))
                velocity.z -= 1 * buff;
            if (Input.GetKey(KeyCode.D) || Input.GetKey("right"))
                velocity.x -= 1 * buff;

            refCamera.hRotation *= Quaternion.Euler(0, -refCamera.mx, 0);
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

            refCamera.hRotation *= Quaternion.Euler(refCamera.mx, 0, 0);
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

            refCamera.hRotation *= Quaternion.Euler(0, 0, -refCamera.mx);
        }
        else if (curG == Vector3.back) //Maze6
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey("up"))
                velocity.y += 1 * buff;
            if (Input.GetKey(KeyCode.A) || Input.GetKey("left"))
                velocity.x += 1 * buff;
            if (Input.GetKey(KeyCode.S) || Input.GetKey("down"))
                velocity.y -= 1 * buff;
            if (Input.GetKey(KeyCode.D) || Input.GetKey("right"))
                velocity.x -= 1 * buff;

            refCamera.hRotation *= Quaternion.Euler(0, 0, refCamera.mx);
        }

        // ���x�x�N�g���̒�����1�b��moveSpeed�����i�ނ悤�ɒ���
        velocity = velocity.normalized * moveSpeed * Time.deltaTime;

       
        
        // �����ꂩ�̕����Ɉړ����Ă���ꍇ
        if (velocity.magnitude > 0)
        {
            // �v���C���[�̉�](transform.rotation)�̍X�V
            // ����]��Ԃ̃v���C���[��Z+����(�㓪��)���A
            // �J�����̐�����](refCamera.hRotation)�ŉ񂵂��ړ��̔��Ε���(-velocity)�ɉ񂷉�]�ɒi�X�߂Â���
            //transform.rotation = Quaternion.Slerp(transform.rotation,
            //                                       Quaternion.LookRotation(hRotation * -velocity),
            //                                       applySpeed);

            transform.rotation = Quaternion.LookRotation(refCamera.hRotation * velocity);
        }

        // �v���C���[�̈ʒu(transform.position)�̍X�V
        // �J�����̐�����](refCamera.hRotation)�ŉ񂵂��ړ�����(velocity)�𑫂�����
        transform.position += refCamera.hRotation * velocity;

        pastG = curG;
        
        //�f�o�b�O�p
        //Debug.Log(velocity.magnitude);
        //Debug.Log(transform.up);
    }

}

