using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class time : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TextTime;
    [SerializeField] private TextMeshProUGUI TextGoal; 
    private float elapsedTime;
    private bool f_Goal = false; //�S�[���ɓ��B�������ǂ����̃t���O

    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = 0.0f; //���Ԃ̏�����
    }

    // Update is called once per frame
    void Update()
    {
        if(f_Goal == false) //�S�[�����Ă��Ȃ��Ƃ�
        {
            elapsedTime += Time.deltaTime; //�o�ߎ��Ԃ����Z
            TextTime.text = string.Format("Time {0:f2}sec", elapsedTime); //�o�ߎ��Ԃ�\��
        }
        
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.name == "Goal")
        {
            f_Goal = true; //�S�[���t���O�𗧂Ă�
            TextGoal.text = "Goal!"; //�S�[���̃e�L�X�g��\��
            Debug.Log("f_Goal = " + f_Goal); //�f�o�b�O�p
        }
    }
}
