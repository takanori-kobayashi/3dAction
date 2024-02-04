using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ポーズ時のテキストの表示処理
/// </summary>
public class UI_PauseText : MonoBehaviour
{
    GameObject GameObj;
    GameState gamestate;

    /// <summary>
    /// カーソルの位置(続ける)
    /// </summary>
    readonly int CURSOR_RESUME = 0;

    /// <summary>
    /// カーソルの位置(終了)
    /// </summary>
    readonly int CURSOR_EXIT = 1;

    /// <summary>
    /// カーソル
    /// </summary>
    int cursor;

    float inputVertical;
    float inputFire1;

    bool inputpush;
    // Start is called before the first frame update
    void Start()
    {
        GameObj = GameObject.Find("GameState");
        gamestate = GameObj.GetComponent<GameState>();

        cursor = CURSOR_RESUME;
        inputpush = false;

        inputVertical = 0.0f;
        inputFire1 = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //ポーズ中
        if (gamestate.m_state == gamestate.STATE_PAUSE)
        {

            if (cursor == CURSOR_RESUME)
            {
                this.GetComponent<Text>().text = "PAUSE MENU\n> RETURN TO GAME\n   EXIT";
            }
            else if (cursor == CURSOR_EXIT)
            {
                this.GetComponent<Text>().text = "PAUSE MENU\n   RETURN TO GAME\n> EXIT";
            }

            //入力を取得-----------------------------------
            inputVertical = Input.GetAxisRaw("Vertical");
            inputFire1 = Input.GetAxisRaw("Fire1");
            //カーソルの動き
            if (inputVertical != 0.0f )
            {

                if(inputpush == false)
                {
                    inputpush = true;
                    cursor++;

                   if (1 < cursor)
                    {
                        cursor = 0;
                    }
                }
            }
            else
            {
                inputpush = false;
            }
            //攻撃ボタン

            if (inputFire1 != 0.0)
            {
                if (cursor == CURSOR_RESUME)
                {
                    gamestate.GamePlay();
                }
                else if (cursor == CURSOR_EXIT)
                {
                    gamestate.Quit();
                }
            }
            //-----------------------------------------------

        }
        //通常時
        else
        {
            cursor = CURSOR_RESUME;
            this.GetComponent<Text>().text = "";
        }
    }
}
