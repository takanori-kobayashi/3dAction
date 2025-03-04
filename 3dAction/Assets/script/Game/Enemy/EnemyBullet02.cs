﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵の弾2(直線)
/// </summary>
public class EnemyBullet02 : MonoBehaviour
{
    /// <summary>
    /// ヒット判定
    /// </summary>
    private bool m_hit;

    /// <summary>
    /// 存在時間
    /// </summary>
    private const float LIVING_TIME = 3.0f;

    /// <summary>
    /// 弾の位置
    /// </summary>
    private Vector3 bullet_pos;

    // Start is called before the first frame update
    void Start()
    {
        Object.Destroy(this.gameObject, LIVING_TIME);

        bullet_pos = GetComponent<Transform>().position;

    }


    /// <summary>
    /// トリガーの場合
    /// </summary>
    /// <param name="hit"></param>
    void OnTriggerStay(Collider hit)
    {
        // 接触対象のタグ
        if (hit.gameObject.tag == "Player" ||
            hit.gameObject.tag == "Obstacle")
        {
            m_hit = true;
        }
    }


    void FixedUpdate()
    {
        Vector3 diff = transform.position - bullet_pos;

        transform.rotation = Quaternion.LookRotation(diff);

        //対象のタグのオブジェクトに接触
        if (m_hit == true)
        {
            //弾の削除
            Object.Destroy(this.gameObject);
        }

        bullet_pos = transform.position;
    }
}
