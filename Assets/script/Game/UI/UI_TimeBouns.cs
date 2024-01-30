using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ゲームクリア時のタイムボーナス表示処理
/// </summary>
public class UI_TimeBouns : MonoBehaviour
{
    //ゲームステートのクラス
    GameObject GameObj;
    GameState gamestate;

    GameObject GameDataObj;
    GameData gamedata;


    // Start is called before the first frame update
    void Start()
    {
        GameObj = GameObject.Find("GameState");
        gamestate = GameObj.GetComponent<GameState>();

        GameDataObj = GameObject.Find("GameData");
        gamedata = GameDataObj.GetComponent<GameData>();

        this.GetComponent<Text>().text = "";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //ステージクリア時に表示
        if(gamestate.m_state == gamestate.STATE_GAMECLEAR)
        {
            this.GetComponent<Text>().text = "TIME BONUS :\n" + gamedata.Timer +" x 20 = " + (gamedata.Timer*20);
        }
    }
}
