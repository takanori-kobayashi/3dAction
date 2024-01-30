using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ボス登場の床
/// </summary>
public class BossFloor : MonoBehaviour
{
    /// <summary>
    /// bossのゲームオブジェクト
    /// </summary>
    public GameObject BossObj;

    /// <summary>
    /// ボスの出現位置
    /// </summary>
    public Transform DefPos;

    /// <summary>
    /// ボス登場の床に触れているか
    /// </summary>
    private bool BossFloorFlg;

    /// <summary>
    /// ボスが登場したか
    /// </summary>
    private bool BossSetFlg;

    // Start is called before the first frame update
    void Start()
    {
        BossFloorFlg = false;
        BossSetFlg = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(BossFloorFlg == true)
        {
            if(BossSetFlg == false)
            {
                BossSetFlg = true;

                // bossの複製
                GameObject Boss = Instantiate(BossObj) as GameObject;
                Boss.transform.position = DefPos.position + new Vector3(0.0f, 20.0f, 10.0f);
            }

        }
    }

    void OnCollisionEnter(Collision hit)
    {
        //プレイヤーが床接触時にボス登場
        if(hit.gameObject.tag == "Player")
        {
            BossFloorFlg = true;
        }
    }
}
