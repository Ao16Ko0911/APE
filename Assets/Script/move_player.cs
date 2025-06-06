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
    [SerializeField] public float buff; //�����E�����o�t

    //�M�~�b�N�n
    private bool hasJump = false; // �M�~�b�N�擾�ς݂�
    private float jumpPower = 5.0f;
    private Rigidbody rb;
    private bool isGrounded = false; // �� �ڒn�t���O�ǉ�


    private Color orgColor;
    private Quaternion hRotation;
    private Vector3 pastG;
    private bool warp_flag;

    void Start()
    {
        orgColor = GetComponent<Renderer>().material.color;
        hRotation = Quaternion.identity;
        rb = GetComponent<Rigidbody>();
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
                velocity.z -= 1 * buff;
            if (Input.GetKey(KeyCode.A) || Input.GetKey("left"))
                velocity.x += 1 * buff;
            if (Input.GetKey(KeyCode.S) || Input.GetKey("down"))
                velocity.z += 1 * buff;
            if (Input.GetKey(KeyCode.D) || Input.GetKey("right"))
                velocity.x -= 1 * buff;

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

        if (hasJump && isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }

    // ���W�����v�t�^�i�O������Ă΂��j
    public void Jump()
    {
        if (!hasJump)
        {
            hasJump = true;
            GetComponent<Renderer>().material.color = Color.red;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            StartCoroutine(JumpTimeLimit(10f));
        }
    }

    private IEnumerator JumpTimeLimit(float duration)
    {
        yield return new WaitForSeconds(duration);
        hasJump = false;
        GetComponent<Renderer>().material.color = orgColor;
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

    public void AddTime(float timeDelta, float duration)
    {
        StartCoroutine(AddTimeBuff(timeDelta, duration));
    }

    // ���^�C�������i�O������Ă΂��j
    public IEnumerator AddTimeBuff(float timeDelta, float duration)
    {
        if (timeDelta > 0)
        {
            GetComponent<Renderer>().material.color = Color.blue; // 増加時は黄色
        }
        else if (timeDelta < 0)
        {
            GetComponent<Renderer>().material.color = Color.yellow; // 減少時は青
        }
        time_attack ta = FindObjectOfType<time_attack>();
        if (ta != null)
        {
            ta.AddTimeFromItem(timeDelta);

            yield return new WaitForSeconds(duration);
            GetComponent<Renderer>().material.color = orgColor;
        }
        else
        {
            Debug.LogWarning("time_attack スクリプトが見つからない！");
        }


    }

    public void Speed(float multiplier, float duration)
    {
        StartCoroutine(SpeedBuff(multiplier, duration));
    }

    private IEnumerator SpeedBuff(float multiplier, float duration)
    {
        float originalSpeed = moveSpeed;
        moveSpeed *= multiplier;
        GetComponent<Renderer>().material.color = Color.gray; // �F�Ńo�t��\��
        yield return new WaitForSeconds(duration);
        moveSpeed = originalSpeed;
        GetComponent<Renderer>().material.color = orgColor;
    }

    public void Reduce(float factor, float duration)
    {
        StartCoroutine(SpeedDebuffRoutine(factor, duration));
    }

    private IEnumerator SpeedDebuffRoutine(float factor, float duration)
    {
        float originalSpeed = moveSpeed;
        moveSpeed *= factor; // ��: factor = 0.5f �Ŕ����̑��x��
        GetComponent<Renderer>().material.color = Color.cyan; // �F�𐅐F��
        yield return new WaitForSeconds(duration);
        moveSpeed = originalSpeed;
        GetComponent<Renderer>().material.color = orgColor; // ���̐F�ɖ߂�
    }

    public void reverse(float duration)
    {
        StartCoroutine(ReverseRoutine(duration));
    }

    public IEnumerator ReverseRoutine(float duration)
    {
        float originalbuff = buff;
        buff *= -1;
        GetComponent<Renderer>().material.color = Color.green;
        yield return new WaitForSeconds(duration);
        buff = originalbuff;
        GetComponent<Renderer>().material.color = orgColor;
    }

}

