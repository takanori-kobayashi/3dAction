using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 床のテクスチャ
/// </summary>
public class FloorTexture : MonoBehaviour
{
    public float tiling_x;
    public float tiling_y;

    public float scale_x;
    public float scale_y;

    Renderer render;
    Vector2 offset;
    Vector2 scale;

    // Start is called before the first frame update
    void Start()
    {
        // オブジェクトのレンダーを取得
        render = GetComponent<Renderer>();


        offset = new Vector2(tiling_x,tiling_y);
        scale = new Vector2(scale_x, scale_y);

        render.material.SetTextureOffset("_MainTex", offset);
        render.material.SetTextureScale("_MainTex", scale);
        render.material.SetTextureOffset("_BumpMap", offset);
        render.material.SetTextureScale("_BumpMap", scale);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
