using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カメラの動き
/// </summary>
public class CameraSample : MonoBehaviour
{

    /// <summary>
    /// Player用のゲームオブジェクト
    /// </summary>
    private GameObject player;

    /// <summary>
    /// 相対距離取得用
    /// </summary>
    private Vector3 offset;      

    // Use this for initialization
    void Start()
    {

        //プレイヤーの情報を取得
        this.player = GameObject.Find("Player");

        // MainCamera(自分自身)とplayerとの相対距離を求める
        offset = transform.position - player.transform.position;

    }

    // Update is called once per frame
    void Update()
    {

        //新しいトランスフォームの値を代入する
        transform.position = player.transform.position + offset;

    }
}