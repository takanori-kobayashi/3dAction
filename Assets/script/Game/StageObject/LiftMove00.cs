using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 移動リフトの処理
/// </summary>
public class LiftMove00 : MonoBehaviour
{
    /// <summary>
    /// 接触したtagのテーブル
    /// </summary>
    readonly string[] HIT_TAG =
    {
        "Player", //プレイヤー
        "FloorEnemy",//設置敵(リフトに乗せたい敵)
    };

    /// <summary>
    /// x軸の移動許可
    /// </summary>
    public bool move_x = false;

    /// <summary>
    /// y軸の移動許可
    /// </summary>
    public bool move_y = false;

    /// <summary>
    /// z軸の移動許可
    /// </summary>
    public bool move_z = false;

    /// <summary>
    /// x軸の移動スピード
    /// </summary>
    public float speed_x = 1.0f;

    /// <summary>
    /// y軸の移動スピード
    /// </summary>
    public float speed_y = 1.0f;

    /// <summary>
    /// z軸の移動スピード
    /// </summary>
    public float speed_z = 1.0f;

    /// <summary>
    /// x軸の移動距離
    /// </summary>
    public float distance_x = 0.0f;

    /// <summary>
    /// y軸の移動距離
    /// </summary>
    public float distance_y = 0.0f;

    /// <summary>
    /// z軸の移動距離
    /// </summary>
    public float distance_z = 0.0f;

    /// <summary>
    /// x座標
    /// </summary>
    private float x = 0.0f;

    /// <summary>
    /// y座標
    /// </summary>
    private float y = 0.0f;

    /// <summary>
    /// z座標
    /// </summary>
    private float z = 0.0f;

    /// <summary>
    /// x軸の最初の移動方向
    /// </summary>
    public bool FirstDirection_x = false;

    /// <summary>
    /// y軸の最初の移動方向
    /// </summary>
    public bool FirstDirection_y = false;

    /// <summary>
    /// z軸の最初の移動方向
    /// </summary>
    public bool FirstDirection_z = false;

    /// <summary>
    /// リフトの最初の位置
    /// </summary>
    private Vector3 origin;


    /// <summary>
    /// カウント値
    /// </summary>
    private readonly float INTERVAL_COUNT = 0.017f;

    /// <summary>
    /// カウンター
    /// </summary>
    private float IntervalCount; //Time.timeだとリセット時リフトがずれるのでの代わりにカウントに

    // Start is called before the first frame update
    void Start()
    {
        origin = GetComponent<Rigidbody>().position;

        if (FirstDirection_x == true)
        {
            speed_x *= -1;
        }
        if (FirstDirection_y == true)
        {
            speed_y *= -1;
        }
        if (FirstDirection_z == true)
        {
            speed_z *= -1;
        }

        IntervalCount = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        IntervalCount += INTERVAL_COUNT;

        if (move_x == true)
        {
            x = Mathf.Sin(IntervalCount * speed_x);
        }
        if (move_y == true)
        {
            y = Mathf.Sin(IntervalCount * speed_y);
        }
        if (move_z == true)
        {
            z = Mathf.Sin(IntervalCount * speed_z);
        }


        Vector3 offset = new Vector3(x, y, z);
        //GetComponent<Rigidbody>().MovePosition(origin + offset);//こっちだと子要素にしたとき滑ってしまう

        offset.x *= distance_x;
        offset.y *= distance_y;
        offset.z *= distance_z;

        GetComponent<Rigidbody>().transform.position = origin + offset;
    }

    /// <summary>
    /// 接触時
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        //プレイヤー意外だとおかしくなる場合があるので
        for(int i=0; i<HIT_TAG.Length; i++)
        {
            if (collision.gameObject.tag == HIT_TAG[i])
            {
                collision.gameObject.transform.SetParent(this.transform);
            }
        }
    }

    /// <summary>
    /// 離れた時
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionExit(Collision collision)
    {
        collision.gameObject.transform.SetParent(null);
    }
}
