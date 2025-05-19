using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class warp : MonoBehaviour
{
    public Vector3 gravityDirection = new Vector3(0.0f, -9.81f, 0.0f); //重力の向き
    private Rigidbody rb;
    public Transform mainCamera; //メインカメラのTransformを取得

    //各迷路のtransform
    public Transform maze1, maze2, maze3, maze4, maze5, maze6;

    //ワープ情報構造体
    [System.Serializable]
    public class WarpInfo
    {
        public string triggerName; //ワープの名前
        public Transform targetMaze; //ワープ先の迷路のTransform
        public Vector3 localOffset; //ワープ先のオフセット
    }

    [HideInInspector]
    public List<WarpInfo> warpInfos = new List<WarpInfo>(); //ワープ情報のリスト

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; //重力を無効にする
    }

    void FixedUpdate()
    {
        rb.AddForce(gravityDirection, ForceMode.Acceleration); //重力を加える
    }

    void OnCollisionEnter(Collision other)
    {
        foreach (var warpInfo in warpInfos)
        {
            if (other.gameObject.name == warpInfo.triggerName)
            {
                gravityDirection = warpInfo.targetMaze.up * -9.81f; //ワープ先の重力を取得
                transform.rotation = warpInfo.targetMaze.rotation; //ワープ先の回転を取得
                mainCamera.rotation = warpInfo.targetMaze.rotation; //カメラの回転を取得
                transform.position = warpInfo.targetMaze.TransformPoint(warpInfo.localOffset); //ワープ先の座標
                break;
            }
        }


        //if (other.gameObject.name == "Warp A1 to A4") 
        //{
        //    gravityDirection = maze4.up * -9.81f; //ワープ先の重力を取得
        //    Vector3 localOffset = new Vector3(22.5f, 1.0f, 22.5f); //ワープ先のオフセット
        //    transform.rotation = maze4.rotation; //ワープ先の回転を取得
        //    mainCamera.rotation = maze4.rotation; //カメラの回転を取得
        //    transform.position = maze4.TransformPoint(localOffset); //ワープ先の座標          
        //}

        //if (other.gameObject.name == "Warp A1 to A5")
        //{
        //    gravityDirection = maze5.up * -9.81f; //ワープ先の重力を取得
        //    Vector3 localOffset = new Vector3(-22.5f, 1.0f, -22.5f); //ワープ先のオフセット
        //    transform.rotation = maze5.rotation; //ワープ先の回転を取得
        //    mainCamera.rotation = maze5.rotation; //カメラの回転を取得
        //    transform.position = maze5.TransformPoint(localOffset); //ワープ先の座標          
        //}

        //if (other.gameObject.name == "Warp B1 to B2")
        //{
        //    gravityDirection = maze2.up * -9.81f; //ワープ先の重力を取得
        //    Vector3 localOffset = new Vector3(-22.5f, 1.0f, 22.5f); //ワープ先のオフセット
        //    transform.rotation = maze2.rotation; //ワープ先の回転を取得
        //    mainCamera.rotation = maze2.rotation; //カメラの回転を取得
        //    transform.position = maze2.TransformPoint(localOffset); //ワープ先の座標          
        //}

        //if (other.gameObject.name == "Warp B1 to B5")
        //{
        //    gravityDirection = maze5.up * -9.81f; //ワープ先の重力を取得
        //    Vector3 localOffset = new Vector3(22.5f, 1.0f, -22.5f); //ワープ先のオフセット
        //    transform.rotation = maze5.rotation; //ワープ先の回転を取得
        //    mainCamera.rotation = maze5.rotation; //カメラの回転を取得
        //    transform.position = maze5.TransformPoint(localOffset); //ワープ先の座標          
        //}

        //if (other.gameObject.name == "Warp C1 to C4")
        //{
        //    gravityDirection = maze4.up * -9.81f; //ワープ先の重力を取得
        //    Vector3 localOffset = new Vector3(22.5f, 1.0f, -22.5f); //ワープ先のオフセット
        //    transform.rotation = maze4.rotation; //ワープ先の回転を取得
        //    mainCamera.rotation = maze4.rotation; //カメラの回転を取得
        //    transform.position = maze4.TransformPoint(localOffset); //ワープ先の座標          
        //}

        //if (other.gameObject.name == "Warp C1 to C6")
        //{
        //    gravityDirection = maze4.up * -9.81f; //ワープ先の重力を取得
        //    Vector3 localOffset = new Vector3(-22.5f, 1.0f, 22.5f); //ワープ先のオフセット
        //    transform.rotation = maze4.rotation; //ワープ先の回転を取得
        //    mainCamera.rotation = maze4.rotation; //カメラの回転を取得
        //    transform.position = maze4.TransformPoint(localOffset); //ワープ先の座標          
        //}

        //if (other.gameObject.name == "Warp D1 to D2")
        //{
        //    gravityDirection = maze2.up * -9.81f; //ワープ先の重力を取得
        //    Vector3 localOffset = new Vector3(-22.5f, 1.0f, -22.5f); //ワープ先のオフセット
        //    transform.rotation = maze2.rotation; //ワープ先の回転を取得
        //    mainCamera.rotation = maze2.rotation; //カメラの回転を取得
        //    transform.position = maze2.TransformPoint(localOffset); //ワープ先の座標          
        //}

        //if (other.gameObject.name == "Warp D1 to D5")
        //{
        //    gravityDirection = maze5.up * -9.81f; //ワープ先の重力を取得
        //    Vector3 localOffset = new Vector3(22.5f, 1.0f, 22.5f); //ワープ先のオフセット
        //    transform.rotation = maze5.rotation; //ワープ先の回転を取得
        //    mainCamera.rotation = maze5.rotation; //カメラの回転を取得
        //    transform.position = maze5.TransformPoint(localOffset); //ワープ先の座標          
        //}
    }

    //★Itemギミックワープの追加
    public void WarpToRandomPoint()
    {
        if (warpInfos.Count == 0) return;

        int index = Random.Range(0, warpInfos.Count);
        WarpInfo selectedWarp = warpInfos[index];

        // プレイヤーの色を黒にして1秒後に戻す
        StartCoroutine(ChangeColorTemporarily(Color.yellow, 1f));

        gravityDirection = selectedWarp.targetMaze.up * -9.81f;
        transform.rotation = selectedWarp.targetMaze.rotation;
        mainCamera.rotation = selectedWarp.targetMaze.rotation;
        transform.position = selectedWarp.targetMaze.TransformPoint(selectedWarp.localOffset);
    }

    //★色を一定期間変更
    private IEnumerator ChangeColorTemporarily(Color tempColor, float duration)
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        if (renderers.Length == 0) yield break;

        Color originalColor = renderers[0].material.color;

        foreach (var r in renderers)
            r.material.color = tempColor;

        yield return new WaitForSeconds(duration);

        foreach (var r in renderers)
            r.material.color = originalColor;
    }

    public void RemoveRandomWarpPoints(int count)
    {
        if (warpInfos.Count == 0) return;

        // シャッフルして上からcount個取る
        List<WarpInfo> tempList = new List<WarpInfo>(warpInfos);
        int removeCount = Mathf.Min(count, tempList.Count);

        for (int i = 0; i < removeCount; i++)
        {
            int index = Random.Range(0, tempList.Count);
            WarpInfo selected = tempList[index];
            tempList.RemoveAt(index); // もう選ばれないようにする

            // triggerNameと一致するGameObjectを探して削除
            GameObject target = GameObject.Find(selected.triggerName);
            if (target != null)
            {
                Destroy(target);
            }

            warpInfos.Remove(selected);
        }
    }

}
