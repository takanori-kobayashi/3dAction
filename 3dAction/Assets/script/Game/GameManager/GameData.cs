using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲーム進行状態
/// </summary>
public class GameData : MonoBehaviour
{
    /// <summary>
    /// ステージの最大タイマー
    /// </summary>
    private const int MAX_TIME = 400;

    /// <summary>
    /// ゲーム終了時のインターバルの最大値
    /// </summary>
    private const int MAX_END_INTERVAL = 60;

    /// <summary>
    /// ゲーム中のタイマー
    /// </summary>
    public int Timer { get; private set; }

    /// <summary>
    /// ゲーム終了時のインターバル
    /// </summary>
    private int GameEndInterval;

    /// <summary>
    /// スコア
    /// </summary>
    public int Score { get; private set; }

    /// <summary>
    /// ゲームクリア時のタイムボーナスのフラグ
    /// </summary>
    private bool m_TimeBonusFlg;

    /// <summary>
    /// Playerのゲームオブジェクト
    /// </summary>
    GameObject PlayerObj;

    /// <summary>
    /// Playerのコンポーネント
    /// </summary>
    Player player;

    /// <summary>
    /// GameStateのゲームオブジェクト
    /// </summary>
    GameObject GameStateObj;

    /// <summary>
    /// GameStateのコンポーネント
    /// </summary>
    GameState gamestate;


    // Start is called before the first frame update
    void Start()
    {
        GameEndInterval = MAX_END_INTERVAL;
        Timer = MAX_TIME;

        PlayerObj = GameObject.Find("Player");
        player = PlayerObj.GetComponent<Player>();

        GameStateObj = GameObject.Find("GameState");
        gamestate = GameStateObj.GetComponent<GameState>();

        m_TimeBonusFlg = false;

        //レイヤーの設定--------------------------------
        //敵との衝突を無視(レイヤー)
        int layer1 = LayerMask.NameToLayer("EnemyBullet");
        int layer2 = LayerMask.NameToLayer("enemy");
        Physics.IgnoreLayerCollision(layer1, layer2);

        //プレイヤーとの衝突を無視(レイヤー)
        layer1 = LayerMask.NameToLayer("Player");
        layer2 = LayerMask.NameToLayer("enemy03");
        Physics.IgnoreLayerCollision(layer1, layer2);

        //プレイヤーとの衝突を無視(レイヤー)
        layer1 = LayerMask.NameToLayer("Player");
        layer2 = LayerMask.NameToLayer("enemy");
        Physics.IgnoreLayerCollision(layer1, layer2);

        //プレイヤーとの衝突を無視(レイヤー)
        layer1 = LayerMask.NameToLayer("enemy");
        layer2 = LayerMask.NameToLayer("enemy");
        Physics.IgnoreLayerCollision(layer1, layer2);

        //プレイヤーとの衝突を無視(レイヤー)
        layer1 = LayerMask.NameToLayer("enemy");
        layer2 = LayerMask.NameToLayer("enemy03");
        Physics.IgnoreLayerCollision(layer1, layer2);

        //----------------------------------------------

        //スコア初期化
        Score = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (0 < Timer && GameEndInterval <= 0 &&
            gamestate.m_state != gamestate.STATE_GAMEOVER &&
            gamestate.m_state != gamestate.STATE_GAMECLEAR)
        {
            GameEndInterval = MAX_END_INTERVAL;
            Timer--;
        }

        else if (0 < GameEndInterval)
        {
            GameEndInterval--;
        }

        //クリア時タイムボーナス加算
        if(gamestate.m_state == gamestate.STATE_GAMECLEAR && m_TimeBonusFlg == false)
        {
            m_TimeBonusFlg = true;
            Score += (Timer * 20);
        }


    }


    /// <summary>
    /// スコア加算
    /// </summary>
    /// <param name="AddScore"></param>
    public void AddScore(int AddScore)
    {
        Score += AddScore;
    }
}
