using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 三人称視点カメラ(メインカメラ)
/// </summary>
public class TpsCamera : MonoBehaviour
{
    /// <summary>
    /// Player用のゲームオブジェクト
    /// </summary>
    GameObject targetObj;

    /// <summary>
    /// Playerのポジション
    /// </summary>
    Vector3 targetPos;

    /// <summary>
    /// Playerのコンポーネント
    /// </summary>
    Player player;

    /// <summary>
    /// GameState用のゲームオブジェクト
    /// </summary>
    GameObject GameStateObj;

    /// <summary>
    /// GameStateのコンポーネント
    /// </summary>
    GameState gamestate;

    /// <summary>
    /// カメラの回転スピード
    /// </summary>
    private readonly float ROTATE_SPEED = 1.0f;

    void Start()
    {
        Debug.Log("TpsCamera.cs");
        targetObj = GameObject.Find("Player");
        targetPos = targetObj.transform.position;
        player = targetObj.GetComponent<Player>();

        GameStateObj = GameObject.Find("GameState");
        gamestate = GameStateObj.GetComponent<GameState>();
    }

    /// <summary>
    /// カメラの位置リセット
    /// </summary>
    public void ResetCamera()
    {
        Debug.Log("ResetCamera TPS");

        targetObj = GameObject.Find("Player");
        targetPos = targetObj.transform.position;

        //-----------------------------
        Vector3 pos;
        Quaternion rote;
        Vector3 scale;

        pos.x = targetPos.x;
        pos.y = targetPos.y + 4.0f;
        pos.z = targetPos.z - 8.0f;

        rote.x = 20.0f;
        rote.y = 0.0f;
        rote.z = 0.0f;

        scale.x = 1.0f;
        scale.y = 1.0f;
        scale.z = 1.0f;

        transform.position = pos;
        transform.rotation = Quaternion.Euler(20.0f, 0.0f, 0.0f);
        transform.localScale = scale;

        //------------------------------
    }

    void Update()
    {
        //死亡時とクリア時カメラくるくる
        //if(player.m_dead == true)
        if(gamestate.m_state == gamestate.STATE_GAMEOVER ||
            gamestate.m_state == gamestate.STATE_GAMECLEAR)
        {
            transform.RotateAround(targetPos, Vector3.up,  Time.deltaTime * 10.0f);
            return;
        }


        // targetの移動量分、自分（カメラ）も移動する
        transform.position += targetObj.transform.position - targetPos;
        targetPos = targetObj.transform.position;

#if false
        // マウスの右クリックを押している間
        if (Input.GetMouseButton(1))
        {
            // マウスの移動量
            float mouseInputX = Input.GetAxis("Mouse X");
            //float mouseInputY = Input.GetAxis("Mouse Y");
            // targetの位置のY軸を中心に、回転（公転）する
            transform.RotateAround(targetPos, Vector3.up, mouseInputX * Time.deltaTime * 200f);
            // カメラの垂直移動（※角度制限なし、必要が無ければコメントアウト）
            //transform.RotateAround(targetPos, transform.right, mouseInputY * Time.deltaTime * 200f);
        }
#else
        // マウスの右クリックを押している間
//        if (Input.GetMouseButton(1))
//        {
            // マウスの移動量
            float mouseInputX_l = Input.GetAxis("CameraX_left");
            float mouseInputX_r = Input.GetAxis("CameraX_right");        

        if (mouseInputX_l >= 1)
        {         // targetの位置のY軸を中心に、回転（公転）する
            transform.RotateAround(targetPos, Vector3.up, -ROTATE_SPEED * Time.deltaTime * 200f);
        }
        if (mouseInputX_r >= 1)
        {         // targetの位置のY軸を中心に、回転（公転）する
            transform.RotateAround(targetPos, Vector3.up, ROTATE_SPEED * Time.deltaTime * 200f);
        }


        //Debug.Log("x" + mouseInputX_l);
        //float mouseInputY = Input.GetAxis("Mouse Y");

            // カメラの垂直移動（※角度制限なし、必要が無ければコメントアウト）
            //transform.RotateAround(targetPos, transform.right, mouseInputY * Time.deltaTime * 200f);
//        }
#endif
    }


}
