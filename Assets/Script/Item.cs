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

                if(rand < 0)
                {
                    ps.Jump(); // 0–29 → 30%
                }
                else if (rand < 0)
                {
                    
                    ps.AddTime(5.0f); // 30–54 → 25%
                }
                else if (rand < 0)
                {
                    ps.AddTime(-3.0f); // 55– → 25%
                }
                else if (rand < 0)// 80–99 → ランダムワープ
                {
                    warp w = ps.GetComponent<warp>();
                    if (w != null)
                    {
                        w.WarpToRandomPoint();
                    }
                }
                else
                {
                    // 90–99 → ワープ地点をランダムで複数削除
                    warp w = ps.GetComponent<warp>();
                    if (w != null)
                    {
                        w.RemoveRandomWarpPoints(3); // 例: 3個削除
                    }
                }



            }

            // アイテムを消す
            Destroy(gameObject);
        }
    }
}

