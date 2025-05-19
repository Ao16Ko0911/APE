using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float moveSpeed = 0.1f;  // �� �N���X���Œ�`�iStart�̊O�j
    private bool hasJump = false; // �M�~�b�N�擾�ς݂�
    private float jumpPower = 5.0f;
    private Rigidbody rb;
    private bool isGrounded = false; // �� �ڒn�t���O�ǉ�

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60; // FPS��60�ɐݒ�
        rb = GetComponent<Rigidbody>();
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

        //���W�����v�M�~�b�N
        if (hasJump && isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }

    //���W�����v�M�~�b�N
    public void Jump()
    {
        if (!hasJump)
        {
            hasJump = true;
            GetComponent<Renderer>().material.color = Color.red;

            // ��]�����ׂČŒ�i�]�|�h�~�j
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            // �W�����v���Ԑ����R���[�`���J�n�i��F10�b�j
            StartCoroutine(JumpTimeLimit(10f));

        }
    }

    // ���W�����v���ł��鎞�Ԃ𐧌�
    private IEnumerator JumpTimeLimit(float duration)
    {
        yield return new WaitForSeconds(duration);

        hasJump = false;
        GetComponent<Renderer>().material.color = Color.white; // ���̐F�ɖ߂��Ȃ�
    }

    // �� �ڒn����i�n�ʂƂ̐ڐG���m�F�j
    void OnCollisionStay(Collision collision)
    {
        // �n�ʂƐڐG���Ă���Ƃ�����true�iLayer��^�O�ōi���Ă�OK�j
        isGrounded = true;
    }

    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

    //���^�C������
    public void AddTime(float timeDelta)
    {
        GetComponent<Renderer>().material.color = Color.blue;
        time_attack ta = FindObjectOfType<time_attack>();
        if (ta != null)
        {

            ta.AddTimeFromItem(timeDelta);
        }
    }

}
