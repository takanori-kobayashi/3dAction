using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの弾発射処理
/// </summary>
public class Shooting : MonoBehaviour
{
    /// <summary>
    /// bulletのゲームオブジェクト
    /// </summary>
    public GameObject bullet;

    /// <summary>
    /// 弾丸発射点 
    /// </summary>
    public Transform muzzle;

    /// <summary>
    /// 弾丸の速度 
    /// </summary>
    public float speed = 1000;

    /// <summary>
    /// fire1キーが押下状態
    /// </summary>
    private float m_fire1 = 0.0f;

    /// <summary>
    /// 弾を打った状態
    /// </summary>
    private bool m_fire1_flg = false;

    /// <summary>
    /// Rigidbodyのコンポーネント
    /// </summary>
    private Rigidbody rBody;

    /// <summary>
    /// Playerのゲームオブジェクト
    /// </summary>
    GameObject refObj;

    /// <summary>
    /// Playerのコンポーネント
    /// </summary>
    Player player;

    /// <summary>
    /// サウンドファイル
    /// </summary>
    public AudioClip sound1;

    /// <summary>
    /// AudioSourceのコンポーネント
    /// </summary>
    AudioSource audioSource;

    // Use this for initialization
    void Start()
    {

        speed = 500;

        audioSource = GetComponent<AudioSource>();

        rBody = this.GetComponent<Rigidbody>();
        rBody.useGravity = false; //最初にrigidBodyの重力を使わなくする

        //プレイヤーのオブジェクト取得
        refObj = GameObject.Find("Player");
        player = refObj.GetComponent<Player>();
    }

    /// <summary>
    /// m_fire1_flgの値を返す
    /// </summary>
    /// <returns></returns>
    public bool GetFireFlg()
    {
        return m_fire1_flg;
    }


    // Update is called once per frame
    void Update()
    {
        if(player.m_dead != true)
        {
            m_fire1 = Input.GetAxisRaw("Fire1");
            
        }
    }

    void FixedUpdate()
    {
        if (1.0f == m_fire1 && false == m_fire1_flg)
        {
            m_fire1_flg = true;

            if(GameObject.FindGameObjectsWithTag("bullet").Length < 3)
            { 
                // 弾丸の複製
                GameObject bullets = Instantiate(bullet) as GameObject;

                Vector3 force;

                force = (this.gameObject.transform.forward + new Vector3(0.0f, 0.5f, 0.0f)) * speed;
                //force = localGravity * speed;
                
                // Rigidbodyに力を加えて発射
                bullets.GetComponent<Rigidbody>().AddForce(force);
                bullets.GetComponent<Rigidbody>().useGravity = true;

                // 弾丸の位置を調整(Playerの座標+指定y座標)
                bullets.transform.position = muzzle.position + new Vector3(0.0f,0.5f,0.0f);

                //サウンドの再生
                //audioSource.PlayOneShot(sound1);
                if(sound1 != null)
                {
                    AudioSource.PlayClipAtPoint(sound1, gameObject.transform.position);
                }
            }
        }

        else if(0.0f == m_fire1)
        {
            m_fire1_flg = false;
        }


    }


}
