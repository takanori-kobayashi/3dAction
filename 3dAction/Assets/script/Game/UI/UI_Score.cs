using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  ////ここを追加////

/// <summary>
/// スコア表示処理
/// </summary>
public class UI_Score : MonoBehaviour
{
    GameObject GameObj;
    GameData gamedata;

    // Start is called before the first frame update
    void Start()
    {
        GameObj = GameObject.Find("GameData");
        gamedata = GameObj.GetComponent<GameData>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.GetComponent<Text>().text = "SCORE:" + gamedata.Score;
    }
}
