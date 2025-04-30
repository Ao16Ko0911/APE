using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//壁に衝突した回数をカウントするスクリプト  

public class contact : MonoBehaviour
{
    int count; //衝突回数をカウントする変数
    [SerializeField] private TextMeshProUGUI TextCount; //衝突回数を表示するTextMeshProUGUIコンポーネント
    
    // Start is called before the first frame update
    void Start()
    {
        count = 0; //衝突回数の初期化
    }

    // Update is called once per frame
    void Update()
    {
        TextCount.text = string.Format("Contact {0} ", count); //衝突回数を表示   
    }

    void OnCollisionEnter(Collision other)
    {
        string namePrefix = other.gameObject.name.Substring(0, 4); //衝突したオブジェクトの名前の先頭4文字を取得
        if (namePrefix == "wall")
        {
            count++; //衝突回数をカウント
        }
    }
}
