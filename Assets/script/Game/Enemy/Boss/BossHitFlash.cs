using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ボスが弾に当たった時の色処理
/// </summary>
public class BossHitFlash : MonoBehaviour
{
    SkinnedMeshRenderer meshrender;

    /// <summary>
    /// Bossのゲームオブジェクト
    /// </summary>
    GameObject BulletHitObj;

    /// <summary>
    /// Bossのコンポーネント
    /// </summary>
    bullet_hit BulletHitClass;

    /// <summary>
    /// ボスの通常状態の色
    /// </summary>
    private Color m_Normal;

    /// <summary>
    /// ボスのダメージ時の色
    /// </summary>
    private Color m_Hit;

    /// <summary>
    /// ボスのダメージ時の時間
    /// </summary>
    private int m_FlashTime = 0;

    /// <summary>
    /// ボスのダメージ時の最大時間
    /// </summary>
    readonly int FLASH_TIME = 10;

    /// <summary>
    /// ボスの前のライフ
    /// </summary>
    private int m_OldLife;

    // Start is called before the first frame update
    void Start()
    {
        meshrender = GetComponent<SkinnedMeshRenderer>();

        //親オブジェクトの取得
        BulletHitObj = transform.root.gameObject;//GameObject.Find("boss");
        BulletHitClass = BulletHitObj.GetComponent<bullet_hit>();

        m_Normal = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        m_Hit = new Color(1.0f, 0.0f, 0.0f, 1.0f);

        m_OldLife = BulletHitClass.life;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float level = Mathf.Abs(Mathf.Sin(Time.time * 10));

        if(BulletHitClass.life  < m_OldLife)
        {
            m_FlashTime = FLASH_TIME;
        }

        if(m_FlashTime <= 0)
        {
            meshrender.material.color = m_Normal;

        }
        else
        {
            if (0 < m_FlashTime)
            {
                m_FlashTime--;
            }
            meshrender.material.color = new Color(level, 0.0f, level, level);

        }

        m_OldLife = BulletHitClass.life;
    }
}
