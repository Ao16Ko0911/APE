using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//�ǂɏՓ˂����񐔂��J�E���g����X�N���v�g  

public class contact : MonoBehaviour
{
    int count; //�Փˉ񐔂��J�E���g����ϐ�
    [SerializeField] private TextMeshProUGUI TextCount; //�Փˉ񐔂�\������TextMeshProUGUI�R���|�[�l���g
    
    // Start is called before the first frame update
    void Start()
    {
        count = 0; //�Փˉ񐔂̏�����
    }

    // Update is called once per frame
    void Update()
    {
        TextCount.text = string.Format("Contact {0} ", count); //�Փˉ񐔂�\��   
    }

    void OnCollisionEnter(Collision other)
    {
        string namePrefix = other.gameObject.name.Substring(0, 4); //�Փ˂����I�u�W�F�N�g�̖��O�̐擪4�������擾
        if (namePrefix == "wall")
        {
            count++; //�Փˉ񐔂��J�E���g
        }
    }
}
