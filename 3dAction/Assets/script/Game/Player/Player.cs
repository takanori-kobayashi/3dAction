using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// プレイヤーの動作
/// </summary>
public class Player : MonoBehaviour
{


    /// <summary>
    /// 接触したtagのテーブル
    /// </summary>
    readonly string[] HIT_TAG =
    {
        "Enemy",//敵
        "FloorEnemy",//設置敵
        "EnemyBullet",//敵の弾
        "trap"//罠
    };

    /// <summary>
    /// プレイヤーの歩く速さ
    /// </summary>
    readonly float MOVE_SPEED = 5.0f;


    /// <summary>
    /// 水平方向の入力
    /// </summary>
    float m_inputHorizontal;

    /// <summary>
    /// 垂直方向の入力
    /// </summary>
    float m_inputVertical;

    /// <summary>
    /// ジャンプのキー入力
    /// </summary>
    private float m_JumpKey;

    /// <summary>
    /// Rigidbodyのコンポーネント
    /// </summary>
    Rigidbody m_rb;

    /// <summary>
    /// ジャンプ力
    /// </summary>
    public float m_JumpPower;

    /// <summary>
    /// ジャンプの状態
    /// </summary>
    public bool m_jumpFlg { get; set; }

    /// <summary>
    /// 重力の設定
    /// </summary>
    [SerializeField] private Vector3 m_localGravity;



    /// <summary>
    /// アニメーション
    /// </summary>
    private Animator m_animator;

    /// <summary>
    ///  アニメーションの状態
    /// </summary>
    readonly int ANIMESTATE_WALK = 0;
    readonly int ANIMESTATE_DEFALT = 1;
    readonly int ANIMESTATE_JUMP = 2;
    readonly int ANIMESTATE_FIRE = 3;


    //キャラクターの初期位置
    private const float DEFAULTPOINT_X = -0.67f;
    private const float DEFAULTPOINT_Y = 3.9f;
    private const float DEFAULTPOINT_Z = -0.395f;

    /// <summary>
    /// GameDataのゲームオブジェクト
    /// </summary>
    GameObject GameObj;

    /// <summary>
    /// GameDataのコンポーネント
    /// </summary>
    GameData gamedata;

    /// <summary>
    /// GameStateのゲームオブジェクト
    /// </summary>
    GameObject GameStateObj;

    /// <summary>
    /// GameStateのコンポーネント
    /// </summary>
    GameState gamestate;

    /// <summary>
    /// Playerのゲームオブジェクト
    /// </summary>
    GameObject refObj;

    /// <summary>
    /// Shootingのコンポーネント
    /// </summary>
    Shooting shooting;

    /// <summary>
    /// ヒットポイント最大値
    /// </summary>
    private const byte MAX_HP = 5;

    /// <summary>
    /// ヒットポイント
    /// </summary>
    private byte m_hitpoint;


    /// <summary>
    /// 最大無敵時間(Frm数)
    /// </summary>
    private readonly byte MAX_INVICIBLETIME = 60;

    /// <summary>
    /// 無敵時間
    /// </summary>
    private byte m_InvincibleTime;

    /// <summary>
    /// 死亡判定
    /// </summary>
    public bool m_dead { get; private set; }

    /// <summary>
    /// 死亡位置
    /// </summary>
    private Vector3 m_DeadPos;

    /// <summary>
    /// 爆発エフェクト
    /// </summary>
    public GameObject exp;

    /// <summary>
    /// 爆発時のSE
    /// </summary>
    public AudioClip SoundExplosion;

    /// <summary>
    /// ダメージ時のSE
    /// </summary>
    public AudioClip SoundHit;

    void Start()
    {

        //ジャンプ力設定-----
        m_JumpPower = 15.0f;
        m_jumpFlg = true;
        //-------------------

        //重力設定-----------
        m_localGravity.x = 0.0f;
        m_localGravity.y = -30.0f;
        m_localGravity.z = 0.0f;
        //-------------------

        //弾発射クラス-------
        refObj = GameObject.Find("Player");
        shooting = refObj.GetComponent<Shooting>();
        //-------------------
        
        //ゲームデータクラス--
        GameObj = GameObject.Find("GameData");
        gamedata = GameObj.GetComponent<GameData>();
        //--------------------

        //ゲームステートクラス--
        GameStateObj = GameObject.Find("GameState");
        gamestate = GameStateObj.GetComponent<GameState>();
        //--------------------

        m_InvincibleTime = 0;

        m_hitpoint = MAX_HP;


        m_animator = GetComponent<Animator>();

        m_rb = GetComponent<Rigidbody>();

        m_dead = false;

    }


    void Update()
    {
        m_inputHorizontal = Input.GetAxisRaw("Horizontal");
        m_inputVertical = Input.GetAxisRaw("Vertical");

        m_JumpKey = Input.GetAxisRaw("Jump");

        //Debug.Log("m_JumpKey:"+m_JumpKey);
    }

    void OnCollisionEnter(Collision col)
    {
        // Debug.Log("OnCollisionEnter:");
        Vector3 hitPos;
        foreach (ContactPoint point in col.contacts)
        {
            hitPos = point.point;
            //Debug.Log("OnCollisionEnter:"+hitPos);
        }
    }

    /// <summary>
    /// プレイヤーのアニメーション
    /// </summary>
    /// <param name="moveForward"></param>
    void Animation(Vector3 moveForward)
    {
        m_animator.speed = 1.0f;

        int state_tmp = m_animator.GetInteger("state");
        //通常
        if (moveForward != Vector3.zero)
        {
            state_tmp = ANIMESTATE_DEFALT;
        }
        //歩き
        else
        {
            state_tmp = ANIMESTATE_WALK;
        }
        //ジャンプ
        if (m_jumpFlg)
        {
            state_tmp = ANIMESTATE_JUMP;
        }

        //弾発射
        if (shooting.GetFireFlg() == true)
        {
            m_animator.speed = 10.0f; //アニメーションのスピード
            state_tmp = ANIMESTATE_FIRE;
        }


        m_animator.SetInteger("state", state_tmp);
    }

    /// <summary>
    /// ヒットポイントの取得
    /// </summary>
    /// <returns></returns>
    public byte GetHP()
    {
        return m_hitpoint;
    }

    /// <summary>
    /// 無敵時間の取得
    /// </summary>
    /// <returns></returns>
    public byte GetInvincibleTime()
    {
        return m_InvincibleTime;
    }

    void FixedUpdate()
    {
        //死亡時
        if(m_dead == true)
        {
            transform.position = m_DeadPos;

            return;
        }

        // カメラの方向から、X-Z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        // 方向キーの入力値とカメラの向きから、移動方向を決定
        Vector3 moveForward = cameraForward * m_inputVertical + Camera.main.transform.right * m_inputHorizontal;


        // 移動方向にスピードを掛ける。ジャンプや落下がある場合は、別途Y軸方向の速度ベクトルを足す。
        m_rb.velocity = moveForward * MOVE_SPEED + new Vector3(0, m_rb.velocity.y, 0);

        // キャラクターの向きを進行方向に
        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }

        //重力加速度の最大値
        if (m_rb.velocity.y <= -15.0f)
        {
            m_rb.velocity = new Vector3(m_rb.velocity.x, -15.0f, m_rb.velocity.z);
        }

        //ジャンプ
        if (m_JumpKey == 1)
        {

            if (!m_jumpFlg)
            {
                m_jumpFlg = true;
                m_rb.velocity = transform.up * m_JumpPower;
            }

        }

        //死亡したとき----------------
        //ゲームクリアの状態でないとき
        if(gamestate.m_state != gamestate.STATE_GAMECLEAR)
        { 
            //落下死
            if(transform.position.y < -15.0f)
            {
                //transform.position = new Vector3(DEFAULTPOINT_X, DEFAULTPOINT_Y, DEFAULTPOINT_Z);
                m_hitpoint = 0;
                m_dead = true;
                m_DeadPos = transform.position;
            }
            //HP0
            if( m_hitpoint <= 0)
            {
                //Object.Destroy(this.gameObject);
                m_dead = true;
                m_DeadPos = transform.position;
                DeadExplosion();//爆発エフェクト
            }
            //時間切れ(タイマー0)
            if(gamedata.Timer <= 0)
            {
                m_hitpoint = 0;
                m_dead = true;
                m_DeadPos = transform.position;
                DeadExplosion(); //爆発エフェクト
            }

            //死亡時のSE
            if(m_dead == true)
            { 
                if (SoundExplosion != null)
                {
                    AudioSource.PlayClipAtPoint(SoundExplosion, gameObject.transform.position);
                }
            }
        }

        //ゲームクリアの状態で落下したとき
        else if(gamestate.m_state == gamestate.STATE_GAMECLEAR)
        {
            //落下を止める
            if (transform.position.y < -100.0f)
            {
                transform.position = new Vector3(transform.position.x,-100.0f, transform.position.z);
            }
        }

        //-------------------------

        //--キャラクターアニメーション--
        Animation(moveForward);

        //無敵時間デクリメント
        if( 0 < m_InvincibleTime )
        {
            m_InvincibleTime--;
        }

        //重力
        m_rb.AddForce(m_localGravity, ForceMode.Acceleration);//Rigidbody に力を加える
    }

    /// <summary>
    /// 攻撃を受けた時の処理
    /// </summary>
    void Damage()
    {
        if (m_InvincibleTime == 0)
        {
            if (0 < m_hitpoint)
            {
                m_hitpoint--;

                if (m_hitpoint != 0 && SoundHit != null)
                {
                    AudioSource.PlayClipAtPoint(SoundHit, gameObject.transform.position);
                }
            }
            m_InvincibleTime = MAX_INVICIBLETIME;
        }
    }


    /// <summary>
    /// やられた時のエフェクト
    /// </summary>
    void DeadExplosion()
    {
        //---------------------
        //爆発エフェクト
        //---------------------
        if (exp != null)
        {
            // 爆発エフェクトの複製
            GameObject Oexp = Instantiate(exp) as GameObject;
            // 爆発エフェクトの位置を調整
            if (transform.position != null)
            {
                Oexp.transform.position = transform.position;
            }

            Object.Destroy(Oexp, 1.0f); //二秒後に削除
        }
    }



    /// <summary>
    /// トリガーとの接触時に呼ばれるコールバック
    /// </summary>
    /// <param name="hit"></param>
    void OnTriggerStay(Collider hit)
    {
        // 接触対象のタグを調べる
        for(int i=0; i<HIT_TAG.Length; i++)
        {
            if (hit.gameObject.tag == HIT_TAG[i])
            {
                Damage();
            }
        }
    }


    /// <summary>
    /// トリガー以外との接触時に呼ばれるコールバック
    /// </summary>
    /// <param name="hit"></param>
    void OnCollisionStay(Collision hit)
    {
        // 接触対象のタグを調べる
        for (int i = 0; i < HIT_TAG.Length; i++)
        {
            if (hit.gameObject.tag == HIT_TAG[i])
            {
                Damage();
            }
        }
    }


}
