using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// プレイヤーのダメージ時の点滅処理
/// </summary>
public class redFlash : MonoBehaviour
{
    /// <summary>
    /// Materialのコンポーネント
    /// </summary>
    private Material material;

    /// <summary>
    /// SkinnedMeshRendererのコンポーネント
    /// </summary>
    SkinnedMeshRenderer meshrender;

    /// <summary>
    /// Playerのゲームオブジェクト
    /// </summary>
    GameObject refObj;

    /// <summary>
    /// Playerのコンポーネント
    /// </summary>
    Player player;

    /// <summary>
    /// 通常状態の色
    /// </summary>
    private Color m_Normal;

    /// <summary>
    /// ダメージ時の色
    /// </summary>
    private Color m_Invincible;

    private void Start()
    {
        meshrender = GetComponent<SkinnedMeshRenderer>();

        refObj = GameObject.Find("Player");
        player = refObj.GetComponent<Player>();

        m_Normal = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        m_Invincible = new Color(1.0f, 0.0f, 0.0f, 1.0f);


    }

    void FixedUpdate()
    {
        float level = Mathf.Abs(Mathf.Sin(Time.time * 10));

        //死亡していたら非表示
        if(player.m_dead == true)
        {
           gameObject.SetActive(false);
        }

        if(0 < player.GetInvincibleTime())
        { 
            meshrender.material.color = new Color(level, level, level, level); ;
        }
        else
        {
            meshrender.material.color = m_Normal;
        }

    }
}