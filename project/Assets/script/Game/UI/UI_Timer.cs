using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  ////ここを追加////

/// <summary>
/// ゲーム中のタイマー表示処理
/// </summary>
public class UI_Timer : MonoBehaviour
{
    public int Timer;
    private int interval;

    GameObject GameObj;
    GameData gamedata;
    // Start is called before the first frame update
    void Start()
    {
        interval = 60;
        Timer = 400;

        GameObj = GameObject.Find("GameData");
        gamedata = GameObj.GetComponent<GameData>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.GetComponent<Text>().text = "TIME:"+ gamedata.Timer;

        if(0 < Timer && interval <= 0)
        {
            interval = 60;
            Timer--;
        }

        else if(0 < interval)
        {
            interval--;
        }
    }
}
