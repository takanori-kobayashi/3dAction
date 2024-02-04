using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// デバッグ表示
/// </summary>
public class DebugText : MonoBehaviour
{

    //点数を格納する変数
    public int score = 0;

    GameObject refObj;
    Player player;

    GameObject Ene5refObj;
    enemy05 ene5;

    float IntervalCount;
    // Use this for initialization
    void Start()
    {
        refObj = GameObject.Find("Player");
        player = refObj.GetComponent<Player>();

        Ene5refObj = GameObject.Find("enemy05");
        ene5 = Ene5refObj.GetComponent<enemy05>();
    }

    // Update is called once per frame
    void Update()
    {
        float level = Mathf.Abs(Mathf.Sin(Time.time * 10));
        IntervalCount += 0.017f;
        //myObject
        //this.GetComponent<Text>().text = "点数" + score + "点";
        //this.GetComponent<Text>().text = "x:" + myObject.GetTransform().position.x + " y:" + myObject.GetTransform().position.y + " z:" + myObject.GetTransform().position.z;
        //float x;
        //x = myObject.GetTransformX();

        //this.GetComponent<Text>().text = "x:" + Time.time + "\ny:" + level + "\nz:" + player.transform.position.z;
        //this.GetComponent<Text>().text = "\ne5/distance:" + ene5.distance;
        this.GetComponent<Text>().text = "";
    }
}