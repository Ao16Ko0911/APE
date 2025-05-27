using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class warp : MonoBehaviour
{
    public Vector3 gravityDirection = new Vector3(0.0f, -9.81f, 0.0f); //�d�͂̌���
    private Rigidbody rb;
    public Transform mainCamera; //���C���J������Transform���擾

    //�e���H��transform
    public Transform maze1, maze2, maze3, maze4, maze5, maze6;
    public Transform Player;

    //���[�v���\����
    [System.Serializable]
    public class WarpInfo
    {
        public string triggerName; //���[�v�̖��O
        public Transform targetMaze; //���[�v��̖��H��Transform
        public Vector3 localOffset; //���[�v��̃I�t�Z�b�g
    }

   
    public List<WarpInfo> warpInfos = new List<WarpInfo>(); //���[�v���̃��X�g

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; //�d�͂𖳌��ɂ���
    }

    void FixedUpdate()
    {
        rb.AddForce(gravityDirection, ForceMode.Acceleration); // �d�͂�������

        // �v���C���[���d�͂̋t�����i������j�ɉ�]������
        Quaternion Player = Quaternion.FromToRotation(transform.up, -1 * gravityDirection.normalized) * transform.rotation;
    
    }

    void OnCollisionEnter(Collision other)
    {
        foreach (var warpInfo in warpInfos)
        {
            if (other.gameObject.name == warpInfo.triggerName)
            {
                gravityDirection = warpInfo.targetMaze.up * -9.81f; //���[�v��̏d�͂��擾
                transform.rotation = warpInfo.targetMaze.rotation; //���[�v��̉�]���擾
                mainCamera.rotation = warpInfo.targetMaze.rotation; //�J�����̉�]���擾
                transform.position = warpInfo.targetMaze.TransformPoint(warpInfo.localOffset); //���[�v��̍��W
                move_camera cam = mainCamera.GetComponent<move_camera>();
                cam.GetComponent<move_camera>().SetJustWarped(); // �v���C���[�ɕt���Ă���J�����X�N���v�g�ɒʒm
                break;
            }
        }
    }

    //��Item�M�~�b�N���[�v�̒ǉ�
    public void WarpToRandomPoint()
    {
        if (warpInfos.Count == 0) return;

        int index = Random.Range(0, warpInfos.Count);
        WarpInfo selectedWarp = warpInfos[index];

        // �v���C���[�̐F�����ɂ���1�b��ɖ߂�
        StartCoroutine(ChangeColorTemporarily(Color.yellow, 1f));

        gravityDirection = selectedWarp.targetMaze.up * -9.81f;
        transform.rotation = selectedWarp.targetMaze.rotation;
        mainCamera.rotation = selectedWarp.targetMaze.rotation;
        transform.position = selectedWarp.targetMaze.TransformPoint(selectedWarp.localOffset);
    }

    //���F�������ԕύX
    private IEnumerator ChangeColorTemporarily(Color tempColor, float duration)
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        if (renderers.Length == 0) yield break;

        Color originalColor = renderers[0].material.color;

        foreach (var r in renderers)
            r.material.color = tempColor;

        yield return new WaitForSeconds(duration);

        foreach (var r in renderers)
            r.material.color = originalColor;
    }

    public void RemoveRandomWarpPoints(int count)
    {
        if (warpInfos.Count == 0) return;


        StartCoroutine(ChangeColorTemporarily(Color.black, 0.5f));

        // �V���b�t�����ďォ��count���
        List<WarpInfo> tempList = new List<WarpInfo>(warpInfos);
        int removeCount = Mathf.Min(count, tempList.Count);

        for (int i = 0; i < removeCount; i++)
        {
            int index = Random.Range(0, tempList.Count);
            WarpInfo selected = tempList[index];
            tempList.RemoveAt(index); // �����I�΂�Ȃ��悤�ɂ���

            // triggerName�ƈ�v����GameObject��T���č폜
            GameObject target = GameObject.Find(selected.triggerName);
            if (target != null)
            {
                Destroy(target);
            }

            warpInfos.Remove(selected);
        }
    }

}
