using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  ////ここを追加////

/// <summary>
/// ポーズ時の表示処理
/// </summary>
public class UI_PAUSE : MonoBehaviour
{

    GameObject GameObj;
    GameState gamestate;

    Color PauseOnClor;
    Color PauseOffClor;

    // Start is called before the first frame update
    void Start()
    {
        GameObj = GameObject.Find("GameState");
        gamestate = GameObj.GetComponent<GameState>();

        PauseOnClor = new Color(0.0f, 0.0f, 0.0f, 0.5f);
        PauseOffClor = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        this.gameObject.GetComponent<Image>().color = PauseOffClor;
    }

    // Update is called once per frame
    void Update()
    {

        if (gamestate.m_state == gamestate.STATE_PAUSE)
        {
            this.gameObject.GetComponent<Image>().color = PauseOnClor;
        }
        else
        {
            this.gameObject.GetComponent<Image>().color = PauseOffClor;
        }
    }
}
