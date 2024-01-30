using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ヒットポイントの表示処理
/// </summary>
public class UI_HP : MonoBehaviour
{
    public GameObject canvas;//キャンバス

    public GameObject HPicon;//
    private GameObject[] prefab = new GameObject[5];

    Player player;
    GameObject refObj;

    int Disp_w;//画面サイズ横
    int Disp_h;//画面サイズ縦

    // Start is called before the first frame update
    void Start()
    {
       
        for(int i=0; i< prefab.Length; i++)
        {
            prefab[i] = (GameObject)Instantiate(HPicon);
            prefab[i].transform.SetParent(canvas.transform, false);
        }


        //// 色を変えてみる!
        //image_component[0].color = Color.red; //赤色を代入

        //image_component[0].transform.SetParent(canvas.transform, false);
        prefab[0].transform.position = new Vector3(30.0f, 30.0f, 0);
        prefab[1].transform.position = new Vector3(30.0f + 60.0f, 30.0f, 0);
        prefab[2].transform.position = new Vector3(30.0f + (60.0f * 2), 30.0f, 0);
        prefab[3].transform.position = new Vector3(30.0f + (60.0f * 3), 30.0f, 0);
        prefab[4].transform.position = new Vector3(30.0f + (60.0f * 4), 30.0f, 0);

        //プレイヤーのオブジェクト取得
        refObj = GameObject.Find("Player");
        player = refObj.GetComponent<Player>();

        Disp_w = Screen.width;
        Disp_h = Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
        //サイズが変更されたら再設定
        if (Disp_w != Screen.width || Disp_h != Screen.height)
        {
            prefab[0].transform.position = new Vector3(30.0f, 30.0f, 0);
            prefab[1].transform.position = new Vector3(30.0f + 60.0f, 30.0f, 0);
            prefab[2].transform.position = new Vector3(30.0f + (60.0f * 2), 30.0f, 0);
            prefab[3].transform.position = new Vector3(30.0f + (60.0f * 3), 30.0f, 0);
            prefab[4].transform.position = new Vector3(30.0f + (60.0f * 4), 30.0f, 0);
        }

        //一度クリア
        for (byte i = 0; i < 5; i++)
        {
            prefab[i].gameObject.SetActive(false); //非表示
        }
        //ある分だけ表示
        for (byte i = 0; i< player.GetHP(); i++)
        {
            prefab[i].gameObject.SetActive(true); //表示
        }
    }
}
