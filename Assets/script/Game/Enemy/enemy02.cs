using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// enemy02の動作
/// </summary>
public class enemy02 : MonoBehaviour
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
    /// Playerのゲームオブジェクト
    /// </summary>
    GameObject refObj;

    /// <summary>
    /// Playerのコンポーネント
    /// </summary>
    Player player;

    /// <summary>
    /// bullet prefab 
    /// </summary>
    public GameObject bullet;

    /// <summary>
    /// インターバルの最大値
    /// </summary>
    private const int MAX_INTERVAL = 100;

    /// <summary>
    /// 弾発射のインターバル
    /// </summary>
    private int m_interval = MAX_INTERVAL;

    /// <summary>
    /// 弾丸発射点 
    /// </summary>
    public Transform muzzle;

    /// <summary>
    /// プレイヤーとの距離
    /// </summary>
    private float distance;

    // Use this for initialization
    void Start()
    {
        enemyController = GetComponent<CharacterController>();
        //animator = GetComponent<Animator>();
        destination = new Vector3(25f, 0f, 25f);
        velocity = Vector3.zero;
        
        //プレイヤーのオブジェクト取得
        refObj = GameObject.Find("Player");
        player = refObj.GetComponent<Player>();

        distance = 0.0f;
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        {
            //プレイヤーとの距離計算
            distance = (transform.position - player.transform.position).magnitude;
            if (35.0f < distance)
            {
                return;
            }

            //プレイヤーの座標取得(ジャンプはしない)
            destination = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);

            velocity = Vector3.zero;
            //animator.SetFloat("Speed", 2.0f);
            direction = (destination - transform.position).normalized;
            transform.LookAt(new Vector3(destination.x, destination.y, destination.z));
            velocity = direction * walkSpeed;
            //Debug.Log(destination.y);

            if(0 < m_interval)
            {
                m_interval--;
            }
            else
            {
                // 弾丸の複製
                GameObject bullets = Instantiate(bullet) as GameObject;
                // 弾丸の位置を調整
                bullets.transform.position = muzzle.position;
                //インターバルセット
                m_interval = MAX_INTERVAL;
            }
        }

    }
}
