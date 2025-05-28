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
    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time; // ゲーム開始時の時刻を記録
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

        if (player == null) return;
        if (!f_Goal)
        {
            elapsedTime = Time.time - startTime;
            TextTime.text = $"Time {elapsedTime:F2} sec";
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Goal")
        {
            f_Goal = true;
            TextTime.text = $"Time {elapsedTime:F2} sec"; // ゴール直後に表示更新
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



    public void AddTimeFromItem(float timeDelta)
    {
        startTime -= timeDelta; // 経過時間を調整
        elapsedTime = Time.time - startTime;
        if (elapsedTime < 0f) elapsedTime = 0f;
        TextTime.text = $"Time {elapsedTime:F2} sec";
    }


    //★ベストタイムをリセット
    public void ResetBestTime()
    {
        PlayerPrefs.DeleteKey("BestTime");
        bestTime = 9999.0f;
        TextBestTime.text = "Best: ---";
        Debug.Log("Best time reset!");
    }



}
