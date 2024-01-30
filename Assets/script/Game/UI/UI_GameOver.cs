using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// ゲームオーバー時の常時処理
/// </summary>
public class UI_GameOver : MonoBehaviour
{
    GameObject refObj;
    Player player;

    public GameObject canvas;//キャンバス
    public GameObject GmaeOverImgObj;//
    private GameObject GmaeOverImgObjpre;

    //ゲームステートのクラス
    GameObject GameStateObj;
    GameState gamestate;

    private int m_interval;


    // Start is called before the first frame update
    void Start()
    {
        GmaeOverImgObjpre = (GameObject)Instantiate(GmaeOverImgObj);
        GmaeOverImgObjpre.transform.SetParent(canvas.transform, false);

        GmaeOverImgObjpre.transform.position = new Vector3(Screen.width/2.0f, Screen.height/2.0f, 0.0f);

        refObj = GameObject.Find("Player");
        player = refObj.GetComponent<Player>();

        GameStateObj = GameObject.Find("GameState");
        gamestate = GameStateObj.GetComponent<GameState>();

        m_interval = 0;
    }

    private void Update()
    {
        /*
        if(player.m_dead == true)
        {
            if(60 <= m_interval)
            {
                if(0 < Input.GetAxisRaw("Fire1"))
                {
                    SceneManager.LoadScene("Title");
                }
            }
        }
        */

        if (gamestate.m_state == gamestate.STATE_GAMEOVER)
        {
            if (60 <= m_interval)
            {
                if (0 < Input.GetAxisRaw("Fire1"))
                {
                    SceneManager.LoadScene("Title");
                }
            }
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if (0 == player.GetHP())
        if(gamestate.m_state == gamestate.STATE_GAMEOVER)
        {
            //表示位置再設定
            if(m_interval == 0)
            {
                GmaeOverImgObjpre.transform.position = new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, 0.0f);
            }

            GmaeOverImgObjpre.gameObject.SetActive(true); //表示

            if (m_interval < 60) {
                m_interval++;
            }
            else
            {
                this.GetComponent<Text>().text = "PUSH FIRE KEY";
            }
        }
        else
        {
            GmaeOverImgObjpre.gameObject.SetActive(false); //非表示
        }

    }
}
