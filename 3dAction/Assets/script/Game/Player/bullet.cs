using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの弾の処理
/// </summary>
public class bullet : MonoBehaviour
{
    /// <summary>
    /// エフェクト赤
    /// </summary>
    const byte RED_EFFECT = 0;

    /// <summary>
    /// エフェクト青
    /// </summary>
    const byte BLUE_EFFECT = 1;

    /// <summary>
    /// エフェクト赤のゲームオブジェクト
    /// </summary>
    public GameObject effect1;

    /// <summary>
    /// エフェクト青のゲームオブジェクト
    /// </summary>
    public GameObject effect2;

    /// <summary>
    /// エフェクトの位置
    /// </summary>
    public Transform tExp;

    /// <summary>
    /// 接触したtagのテーブル用のクラス
    /// </summary>
    private class HitTag
    {
        public string ObjectName;
        public int EffectType;

        public HitTag(string Object, int Effect)
        {
            ObjectName = Object;
            EffectType = Effect;
        }
    };

    /// <summary>
    /// 接触したtagのテーブル
    /// </summary>
    readonly HitTag[] HIT_TAG =
    {
        new HitTag("Obstacle",BLUE_EFFECT), //障害物
        new HitTag("MoveStage",BLUE_EFFECT),//移動する床
        new HitTag("Enemy",RED_EFFECT),//敵
        new HitTag("Wall",BLUE_EFFECT),//壁
        new HitTag("FloorEnemy",RED_EFFECT),//設置敵
        new HitTag("trap",BLUE_EFFECT),//罠系
    };


    // Start is called before the first frame update
    void Start()
    {
        Object.Destroy(this.gameObject, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(5, 5, 5));
    }

    /// <summary>
    /// 接触時の処理
    /// </summary>
    /// <param name="t"></param>
    void OnTriggerEnter(Collider t)
    {
        // 接触対象を検索
        for(int i=0; i<HIT_TAG.Length; i++) {
            if (t.gameObject.tag == HIT_TAG[i].ObjectName)
            {
                //---------------------
                //ヒットエフェクト
                //---------------------

                //ヒットエフェクトの複製
                GameObject Oexp;

                Oexp = null;

                //エフェクト赤
                if (HIT_TAG[i].EffectType == RED_EFFECT)
                {
                    if (effect1 != null) { 
                        Oexp = Instantiate(effect1) as GameObject;
                    }
                }
                //エフェクト青
                else if (HIT_TAG[i].EffectType == BLUE_EFFECT)
                {
                    if (effect2 != null)
                    {
                        Oexp = Instantiate(effect2) as GameObject;
                    }
                }

                if (Oexp != null)
                {
                    // 爆発エフェクトの位置を調整
                    Oexp.transform.position = tExp.position;
                    Object.Destroy(Oexp, 0.5f); //0.5秒後に削除                                            
                    Object.Destroy(this.gameObject);//弾の削除
                }

                else {
                    Object.Destroy(this.gameObject);
                }
                break;
            }
        }
    }
}
