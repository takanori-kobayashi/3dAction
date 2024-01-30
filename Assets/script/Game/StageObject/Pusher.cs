using UnityEngine;
using System.Collections;

/// <summary>
/// 押す床
/// </summary>
public class Pusher : MonoBehaviour
{
    /// <summary>
    /// 最初の位置
    /// </summary>
    private Vector3 origin;

    //private new Rigidbody rigidbody;

    void Start()
    {
        origin = GetComponent<Rigidbody>().position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 offset = new Vector3(0, 0, Mathf.Sin(Time.time));
        GetComponent<Rigidbody>().MovePosition(origin + offset);
    }
}