using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// enemy04の動作
/// </summary>
public class enemy04 : MonoBehaviour
{
    /// <summary>
    /// 弾発射の最大インターバル
    /// </summary>
    private const int MAX_INTERVAL = 100;

    /// <summary>
    /// 弾発射のインターバル
    /// </summary>
    private int m_interval;

    /// <summary>
    /// bulletのゲームオブジェクト
    /// </summary>
    public GameObject bullet;

    /// <summary>
    /// 弾丸発射点 
    /// </summary>
    public Transform muzzle;

    /// <summary>
    /// プレイヤーとの距離
    /// </summary>
    private float distance;

    /// <summary>
    /// Playerのゲームオブジェクト
    /// </summary>
    GameObject refObj;

    /// <summary>
    /// Playerのコンポーネント
    /// </summary>
    Player player;


    // Start is called before the first frame update
    void Start()
    {
        m_interval = 0;

        //プレイヤーのオブジェクト取得
        refObj = GameObject.Find("Player");
        player = refObj.GetComponent<Player>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //プレイヤーとの距離計算
        distance = (transform.position - player.transform.position).magnitude;

        if (35.0f < distance)
        {
            return;
        }

        if (0 < m_interval)
        {
            m_interval--;
        }
        else
        {
            // 弾丸の複製
            GameObject bullets = Instantiate(bullet) as GameObject;
            // 弾丸の位置を調整
            bullets.transform.position = muzzle.position;
            //インターバルセット
            m_interval = MAX_INTERVAL;

            Vector3 force;

            //force = (this.gameObject.transform.forward + new Vector3(0.0f, 0.5f, 0.0f)) * 10;
            force = this.gameObject.transform.forward * 1000;

            // Rigidbodyに力を加えて発射
            bullets.GetComponent<Rigidbody>().AddForce(force);

            // 弾丸の位置を調整(Playerの座標+指定y座標)
            bullets.transform.position = muzzle.position + new Vector3(0.0f, 2.0f, 0.0f);
        }

    }
}
