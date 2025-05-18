using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class time_attack : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TextTime;
    [SerializeField] private TextMeshProUGUI TextGoal;
    [SerializeField] private TextMeshProUGUI TextBestTime;
    [SerializeField] private GameObject player;

    private float elapsedTime;
    private float bestTime;  // ← 追加
    private bool f_Goal = false; // ゴールに到達したかどうかのフラグ

    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = 0.0f;

        // 保存されたベストタイムを読み込む（なければ9999.0fを初期値とする）
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
        if (!f_Goal)
        {
            elapsedTime += Time.deltaTime;
            TextTime.text = string.Format("Time {0:F2} sec", elapsedTime);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Goal")
        {
            f_Goal = true;
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

            //プレイヤーを消す
            if (player != null)
            {
                Destroy(player);
            }
        }
    }
}
