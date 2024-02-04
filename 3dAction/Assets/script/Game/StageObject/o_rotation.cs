using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 回転リフト
/// </summary>
public class o_rotation : MonoBehaviour
{
    /// <summary>
    /// 弾の座標
    /// </summary>
    private float x, y, z;

    /// <summary>
    /// 弾の速度
    /// </summary>
    private float vx, vy, vz;

    /// <summary>
    /// 回転の中心の座標
    /// </summary>
    private float cx, cy, cz;

    /// <summary>
    /// 半径
    /// </summary>
    public float r = 10.0f;

    /// <summary>
    /// 角度(ラジアン)
    /// </summary>
    private float theta = 0.0f;

    /// <summary>
    /// 1回の移動で変化する角度(ラジアン)
    /// </summary>
    public float omega = 0.01f;

    /// <summary>
    /// 角度(ラジアン)
    /// </summary>
    private float z_theta = 0.0f;

    /// <summary>
    /// 1回の移動で変化する角度(ラジアン)
    /// </summary>
    private float z_omega = 0.1f;

    /// <summary>
    /// 最初の位置
    /// </summary>
    private Vector3 origin;

    /// <summary>
    /// z軸の回転向き
    /// </summary>
    public bool z_type = false;

    //int z_pi = 0;

    // Start is called before the first frame update
    void Start()
    {
        origin = GetComponent<Rigidbody>().position;
        x = origin.x;
        y = origin.y;
        z = origin.z;

    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {

        Vector3 offset = new Vector3(x , y, z);

        //GetComponent<Rigidbody>().MovePosition(origin + offset); //こっちだと子要素にしたとき滑ってしまう
        GetComponent<Rigidbody>().transform.position = origin + offset;


        theta += omega;
        z_theta += z_omega;

       
        y = origin.y + r * Mathf.Sin(theta);

        if(z_type == true)
        { 
            x = 0;//origin.x + r * Mathf.Cos(theta);
            z = origin.x + r * Mathf.Cos(theta);// origin.x + r * Mathf.Cos(theta);
        }
        else
        {
            x = origin.x + r * Mathf.Cos(theta); ;//origin.x + r * Mathf.Cos(theta);
            z = 0;// origin.x + r * Mathf.Cos(theta);
        }

      

    }

    private void OnCollisionEnter(Collision collision)
    {
        //プレイヤー意外だとおかしくなる場合がある
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.SetParent(this.transform);
        }
    }


    private void OnCollisionExit(Collision collision)
    {
        collision.gameObject.transform.SetParent(null);
    }
}
