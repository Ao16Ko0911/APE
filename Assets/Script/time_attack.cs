using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class time_attack : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TextTime;
    [SerializeField] private TextMeshProUGUI TextGoal;
    [SerializeField] private TextMeshProUGUI TextBestTime;
    [SerializeField] private GameObject player;

    private float elapsedTime;
    private float bestTime;  // �� �ǉ�
    private bool f_Goal = false; // �S�[���ɓ��B�������ǂ����̃t���O
    

    

    // Start is called before the first frame update
    void Start()
    {

        elapsedTime = 0.0f;

        // �ۑ����ꂽ�x�X�g�^�C����ǂݍ��ށi�Ȃ����9999.0f�������l�Ƃ���j
        bestTime = PlayerPrefs.GetFloat("BestTime", 9999.0f);

        if (bestTime < 9999.0f)
        {
            TextBestTime.text = $"Best: {bestTime:F2} sec";
        }
        else
        {
            TextBestTime.text = "Best: ---";
        }
    }

    

    // Update is called once per frame
    void Update()
    {

        if (player == null) return;
        else if (!f_Goal)
        {
            elapsedTime += Time.deltaTime;
            TextTime.text = $"Time {elapsedTime:F2} sec";
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Goal")
        {
            f_Goal = true;
            TextTime.text = $"Time {elapsedTime:F2} sec"; // �S�[������ɕ\���X�V
            TextGoal.text = $"Goal!\nTime: {elapsedTime:F2} sec";

            if (elapsedTime < bestTime)
            {
                bestTime = elapsedTime;
                PlayerPrefs.SetFloat("BestTime", bestTime);
                PlayerPrefs.Save();
                TextBestTime.text = $"Best: {bestTime:F2} sec";
                Debug.Log("New Best Time Saved!");
            }

            Debug.Log("f_Goal = " + f_Goal);

            //�v���C���[������
            if (player != null)
            {
                Destroy(player);
            }
        }
    }



    public void AddTimeFromItem(float timeDelta)
    {
        elapsedTime += timeDelta;
        if (elapsedTime < 0f) elapsedTime = 0f;
        TextTime.text = $"Time {elapsedTime:F2} sec";

    }


    //���x�X�g�^�C�������Z�b�g
    public void ResetBestTime()
    {
        PlayerPrefs.DeleteKey("BestTime");
        bestTime = 9999.0f;
        TextBestTime.text = "Best: ---";
        Debug.Log("Best time reset!");
    }



}
