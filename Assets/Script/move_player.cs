using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_player : MonoBehaviour
{
    [SerializeField] private Vector3 velocity;              // �ړ�����
    [SerializeField] private float moveSpeed = 5.0f;        // �ړ����x
    [SerializeField] private move_camera refCamera;  // �J�����̐�����]���Q�Ƃ���p
    [SerializeField] private float applySpeed = 0.2f; //��]���x
    [SerializeField] private float buff;

    void Start() {
        buff = 1;
    }

    void Update()
    {
        // WASD���͂���AXZ����(�����Ȓn��)���ړ��������(velocity)�𓾂܂�
        velocity = Vector3.zero;
        if (Input.GetKey(KeyCode.W) || Input.GetKey("up"))
            velocity.z -= 1 * buff;
        if (Input.GetKey(KeyCode.A) || Input.GetKey("left"))
            velocity.x += 1 * buff;
        if (Input.GetKey(KeyCode.S) || Input.GetKey("down"))
            velocity.z += 1 * buff;
        if (Input.GetKey(KeyCode.D) || Input.GetKey("right"))
            velocity.x -= 1 * buff;

        // ���x�x�N�g���̒�����1�b��moveSpeed�����i�ނ悤�ɒ���
        velocity = velocity.normalized * moveSpeed * Time.deltaTime;

        // �����ꂩ�̕����Ɉړ����Ă���ꍇ
        if (velocity.magnitude > 0)
        {
            // �v���C���[�̉�](transform.rotation)�̍X�V
            // ����]��Ԃ̃v���C���[��Z+����(�㓪��)���A
            // �J�����̐�����](refCamera.hRotation)�ŉ񂵂��ړ��̔��Ε���(-velocity)�ɉ񂷉�]�ɒi�X�߂Â���
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                  Quaternion.LookRotation(refCamera.hRotation * -velocity),
                                                  applySpeed);

            // �v���C���[�̈ʒu(transform.position)�̍X�V
            // �J�����̐�����](refCamera.hRotation)�ŉ񂵂��ړ�����(velocity)�𑫂�����
            transform.position += refCamera.hRotation * velocity;
        }
    }

}

