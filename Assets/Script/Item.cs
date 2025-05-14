using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // �I�u�W�F�N�g���� "Player" �̏ꍇ�̂ݏ�������
        if (other.gameObject.name == "Player")
        {
            // �v���C���[�� PlayerScript �������Ă���΃M�~�b�N��t�^
            Player ps = other.GetComponent<Player>();
            if (ps != null)
            {
                ps.EnableGimmick();
            }

            // �A�C�e��������
            Destroy(gameObject);
        }
    }
}
