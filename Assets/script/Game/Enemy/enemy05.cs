using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// enemy05の動作
/// </summary>
public class enemy05 : MonoBehaviour
{
    /// <summary>
    /// CharacterControllerのコンポーネント
    /// </summary>
    private CharacterController enemyController;

    /// <summary>
    /// 目的地点
    /// </summary>
    private Vector3 destination;

    /// <summary>
    /// 回転
    /// </summary>
    private Vector3 loatate;

    /// <summary>
    /// 歩くスピード
    /// </summary>
    [SerializeField]
    private float walkSpeed = 1.0f;


    /// <summary>
    /// 速度
    /// </summary>
    private Vector3 velocity;

    /// <summary>
    /// 移動方向
    /// </summary>
    private Vector3 direction;

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
        
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        //プレイヤーとの距離計算
        distance = (transform.position - player.transform.position).magnitude;        
        if (10.0f < distance) return;

        destination = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        loatate = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        direction = (destination - transform.position).normalized;
        velocity = Vector3.zero;
        velocity = direction * walkSpeed;


        GetComponent<Rigidbody>().MovePosition(transform.position + velocity * Time.deltaTime);

        transform.LookAt(loatate);

    }
}
