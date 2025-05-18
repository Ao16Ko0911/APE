using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // オブジェクト名が "Player" の場合のみ処理する
        if (other.gameObject.name == "Player")
        {
            // プレイヤーが PlayerScript を持っていればギミックを付与
            Player ps = other.GetComponent<Player>();
            if (ps != null)
            {
                int rand = Random.Range(0, 100);

                if (rand < 50)
                {
                    ps.EnableGimmick();
                }
                else if (rand < 80)
                {
                    ps.EnableGimmick();
                }
                else
                {
                    ps.EnableGimmick();
                }


                ps.EnableGimmick();
            }

            // アイテムを消す
            Destroy(gameObject);
        }
    }
}

