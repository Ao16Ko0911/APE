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
            move_player ps = other.GetComponent<move_player>();

            if (ps != null)
            {
                int rand = Random.Range(0, 100);

                if (rand < 100)
                {

                    ps.AddTime(-5.0f); // 30–54 → 25%
                }

                if (rand < 10)
                {
                    ps.Jump(); // 0–29 → 30%
                }
                else if(rand < 20)
                {
                    ps.reverse(5.0f);
                }
                else if (rand < 30)
                {
                    
                    ps.AddTime(5.0f); // 30–54 → 25%
                }
                else if (rand < 50)
                {
                    ps.AddTime(-5.0f); // 55– → 25%
                }
                
                else if (rand < 70)
                {
                    // 60〜69の10%でスピードバフ付与
                    ps.Speed(4.0f, 5.0f); // 5秒間スピード2倍
                }

                else if (rand < 89)
                {
                    ps.Reduce(0.75f, 5.0f); // スピード半分、5秒間
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

