using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ボスの動作
/// </summary>
public class boss : MonoBehaviour
{
    /// <summary>
    /// ボスのY軸の高さ
    /// </summary>
    readonly float BOSS_POSITION_Y = 4.6f;

    /// <summary>
    /// 登場時のスピード
    /// </summary>
    readonly float BOSS_ENTER_SPEED = 20.0f;

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
    /// Playerのゲームコンポーネント
    /// </summary>
    Player player;

    /// <summary>
    /// ボスの移動方向
    /// </summary>
    private Vector3 destination;

    /// <summary>
    /// ボスの向き
    /// </summary>
    private Vector3 loatate;

    /// <summary>
    /// 移動速度
    /// </summary>
    [SerializeField]
    private float walkSpeed = 1.0f;

    /// <summary>
    /// ボスのアニメーション
    /// </summary>
    private Animator animator; 

    /// <summary>
    /// ボスの弾発射間隔
    /// </summary>
    private readonly int SHOT_INTERVAL = 60*5;

    /// <summary>
    /// ボスの弾発射間隔のカウント
    /// </summary>
    private int m_ShotIntervalCount;

    /// <summary>
    /// ボスの弾発射状態
    /// </summary>
    public bool BossShot { get; private set; }

    /// <summary>
    /// ボスの登場時の状態フラグ
    /// </summary>
    private bool m_BossEnterFlg;


    // Start is called before the first frame update
    void Start()
    {
        //プレイヤーのオブジェクト取得
        refObj = GameObject.Find("Player");
        player = refObj.GetComponent<Player>();

        m_ShotIntervalCount = SHOT_INTERVAL;
        BossShot = false;
        m_BossEnterFlg = true;

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //ボス出現時
        if(m_BossEnterFlg == true)
        {
            BossEnter();
        }
        //通常時
        else
        { 
            BossNromal();
        }
       
        //ボスのアニメーション
        Animation();
    }


    /// <summary>
    /// ボスの登場時の動作
    /// </summary>
    void BossEnter()
    {

        destination = new Vector3(this.transform.position.x, BOSS_POSITION_Y, this.transform.position.z);
        loatate = new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z);
        direction = (destination - transform.position).normalized;
        velocity = Vector3.zero;
        velocity = direction * BOSS_ENTER_SPEED;

        GetComponent<Rigidbody>().MovePosition(transform.position + velocity * Time.deltaTime);
        transform.LookAt(loatate);

        if(this.transform.position.y <= BOSS_POSITION_Y)
        {
            m_BossEnterFlg = false;
        }
    }

    /// <summary>
    /// ボスの通常時の動作
    /// </summary>
    void BossNromal()
    {
        //ゲームオーバー時は動きを止める
        if(player.m_dead == true)
        {
            BossShot = false;
            return;
        }

        destination = new Vector3(player.transform.position.x, 4.6f, player.transform.position.z);
        loatate = new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z);
        direction = (destination - transform.position).normalized;
        velocity = Vector3.zero;
        velocity = direction * walkSpeed;

        GetComponent<Rigidbody>().MovePosition(transform.position + velocity * Time.deltaTime);

        //プレイヤーの向きに設定
        transform.LookAt(loatate);

        //ボスの弾発射動作
        if (BossShot == false)
        {
            if (0 < m_ShotIntervalCount)
            {
                m_ShotIntervalCount--;
            }
            else
            {
                BossShot = true;
                m_ShotIntervalCount = SHOT_INTERVAL;
            }
        }
        else
        {
            if (0 < m_ShotIntervalCount)
            {
                m_ShotIntervalCount--;
            }
            else
            {
                BossShot = false;
                m_ShotIntervalCount = SHOT_INTERVAL;
            }

        }
    }

    /// <summary>
    /// ボスのアニメーション
    /// </summary>
    void Animation()
    {
        animator.speed = 1.0f;

        animator.SetBool("Fire", BossShot);
    }

}
