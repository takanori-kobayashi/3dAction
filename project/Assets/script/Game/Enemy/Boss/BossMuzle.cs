using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ボスの弾発射処理
/// </summary>
public class BossMuzle : MonoBehaviour
{
    /// <summary>
    /// Bossのゲームオブジェクト
    /// </summary>
    GameObject BossObj;

    /// <summary>
    /// ボスのゲームコンポーネント
    /// </summary>
    boss BossClass;

    /// <summary>
    /// bulletのゲームオブジェクト
    /// </summary>
    public GameObject bullet;

    /// <summary>
    /// 弾発射のまでの最大時間
    /// </summary>
    private const int MAX_INTERVAL = 10;

    /// <summary>
    /// 弾発射までの時間
    /// </summary>
    private int m_interval = MAX_INTERVAL;

    /// <summary>
    /// 弾丸発射点 
    /// </summary>
    public Transform muzzle;

    // Start is called before the first frame update
    void Start()
    {
        //親オブジェクトの取得
        BossObj = transform.root.gameObject;//GameObject.Find("boss");
        BossClass = BossObj.GetComponent<boss>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //弾丸発射
        if (BossClass.BossShot == true)
        {
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
            }
        }        
    }
}
