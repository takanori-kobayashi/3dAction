using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームの状態
/// </summary>
public class GameState : MonoBehaviour
{
    /// <summary>
    /// ステージの状態(通常時)
    /// </summary>
    public readonly int STATE_PLAY = 0;

    /// <summary>
    /// ステージの状態(ポーズ状態)
    /// </summary>
    public readonly int STATE_PAUSE = 1;

    /// <summary>
    /// ステージの状態(ゲームオーバー状態)
    /// </summary>
    public readonly int STATE_GAMEOVER = 2;

    /// <summary>
    /// ステージの状態(ステージクリア状態)
    /// </summary>
    public readonly int STATE_GAMECLEAR = 3;


    /// <summary>
    /// ゲームの状態
    /// </summary>
    public int m_state { get; set; }

    /// <summary>
    /// キーが押されているかどうか判定
    /// </summary>
    private bool m_keypush;

    /// <summary>
    /// キー入力を受け付けるか判定
    /// </summary>
    private bool m_pushjudg;

    /// <summary>
    /// Playerのゲームオブジェクト
    /// </summary>
    Player player;

    /// <summary>
    /// Playerのコンポーネント
    /// </summary>
    GameObject refObj;

    // Start is called before the first frame update
    void Start()
    {
        m_state = STATE_PLAY;
        m_pushjudg = false;

        //プレイヤーのオブジェクト取得
        refObj = GameObject.Find("Player");
        player = refObj.GetComponent<Player>();

    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKey(KeyCode.Escape))
        {
            m_keypush = true;
        }
        else
        {
            m_keypush = false;
        }


        //ゲームプレイ中にESCキー(死んでいないとき)
        if (m_keypush == true && 
            m_state != STATE_GAMECLEAR &&
            m_state != STATE_GAMEOVER)
        {
            if (m_pushjudg == false)
            {
                if (m_state == STATE_PLAY)
                {
                    GamePause();
                }
                else if (m_state == STATE_PAUSE)
                {
                    GamePlay();
                }

                m_pushjudg = true;
            }
        }
        else
        {
            m_pushjudg = false;
        }

        if(player.m_dead == true)
        {
            m_state = STATE_GAMEOVER;
        }



    }

    private void FixedUpdate()
    {

    }


    /// <summary>
    /// アプリケーションの終了
    /// </summary>
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
    UnityEngine.Application.Quit();
#endif
    }

    /// <summary>
    /// ポーズ状態に切り替え
    /// </summary>
    public void GamePause()
    {
        m_state = STATE_PAUSE;
        Time.timeScale = 0f;
    }

    /// <summary>
    /// 通常状態に切り替え
    /// </summary>
    public void GamePlay()
    {
        m_state = STATE_PLAY;
        Time.timeScale = 1f;
    }
}
