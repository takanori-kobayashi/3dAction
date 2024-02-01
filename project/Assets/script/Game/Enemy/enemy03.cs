using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// enemy03の動作
/// </summary>
public class enemy03 : MonoBehaviour
{
    /// <summary>
    /// 目的地
    /// </summary>
    private Vector3 destination;

    /// <summary>
    /// 移動スピード
    /// </summary>
    [SerializeField]
    private float MoveSpeed = 100.0f;

    /// <summary>
    /// 最大移動スピード
    /// </summary>
    private const float MAX_SPEED = 15.0f;

    /// <summary>
    /// 移動するタイミングの最大値
    /// </summary>
    private const int MAX_TIMING = (int)(MAX_SPEED * 4);

    /// <summary>
    /// 移動するタイミング
    /// </summary>
    private int timing = 0;

    /// <summary>
    /// プレイヤーとの距離
    /// </summary>
    private float distance;

    /// <summary>
    /// Playerのゲームオブジェクト
    /// </summary>
    GameObject refObj;

    /// <summary>
    /// Playerのコンポーネント
    /// </summary>
    Player player;

    void Start()
    {

        //プレイヤーのオブジェクト取得
        refObj = GameObject.Find("Player");
        player = refObj.GetComponent<Player>();

        MoveSpeed = 10.0f;
        timing = 20;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //プレイヤーとの距離計算
        distance = (transform.position - player.transform.position).magnitude;
        if (20.0f < distance)
        {
            GetComponent<Rigidbody>().velocity = transform.forward.normalized * 0;
            return;
        }

        if (timing < 0)
        {
            //プレイヤーの座標取得(ジャンプはしない)
            destination = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);

            transform.LookAt(player.transform); //プレイヤーの向きに設定

            timing = MAX_TIMING;
            MoveSpeed = MAX_SPEED;
        }
        else
        {

            GetComponent<Rigidbody>().velocity = transform.forward.normalized * MoveSpeed; //速度ベクトル設定

            if (0 < MoveSpeed)
            {
                MoveSpeed -= 1.0f;
            }
            timing--;
        }
    }

}
