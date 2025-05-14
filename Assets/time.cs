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
    private bool f_Goal = false; //ゴールに到達したかどうかのフラグ

    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = 0.0f; //時間の初期化
    }

    // Update is called once per frame
    void Update()
    {
        if(f_Goal == false) //ゴールしていないとき
        {
            elapsedTime += Time.deltaTime; //経過時間を加算
            TextTime.text = string.Format("Time {0:f2}sec", elapsedTime); //経過時間を表示
        }
        
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.name == "Goal")
        {
            f_Goal = true; //ゴールフラグを立てる
            TextGoal.text = "Goal!"; //ゴールのテキストを表示
            Debug.Log("f_Goal = " + f_Goal); //デバッグ用
        }
    }
}
