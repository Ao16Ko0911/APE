using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo_Maze : MonoBehaviour
{
    Transform TF; //–À˜H‚ðŠi”[
    [SerializeField] private float speed;

    // Start is called before the first frame update
    void Start()
    {
        TF = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        TF.Rotate(0f, speed, 0f, Space.World);
    }
}
