using UnityEngine;
using System.Collections;

/// <summary>
/// カメラの切り替え(テスト用)
/// </summary>
public class ChangeCamera : MonoBehaviour
{
    /// <summary>
    /// メインカメラ
    /// </summary>
    [SerializeField] private GameObject mainCamera;

    /// <summary>
    /// 切り替える他のカメラ
    /// </summary>
    [SerializeField] private GameObject otherCamera;

    /// <summary>
    /// 切り替える他のカメラ
    /// </summary>
    [SerializeField] private GameObject TpsCamera;

    /// <summary>
    /// Script用のゲームオブジェクト
    /// </summary>
    GameObject m_tpsCamera;

    private void Start()
    {
        m_tpsCamera = GameObject.Find("Script");
        TpsCamera.GetComponent<TpsCamera>().ResetCamera();
    }

    // Update is called once per frame
    void Update()
    {
        //以下デバッグ用
        /*
        //　1キーを押したらカメラの切り替えをする
        if (Input.GetKeyDown("1"))
        {

            TpsCamera t1 = m_tpsCamera.GetComponent<TpsCamera>();

            type++;
            switch (type) {
                case 0:
                    mainCamera.SetActive(true);
                    otherCamera.SetActive(false);
                    TpsCamera.SetActive(false);
                    break;
                case 1:
                    mainCamera.SetActive(false);
                    otherCamera.SetActive(true);
                    TpsCamera.SetActive(false);                    
                    break;
                case 2:
                    mainCamera.SetActive(false);
                    otherCamera.SetActive(false);
                    TpsCamera.SetActive(true);
                    TpsCamera.GetComponent<TpsCamera>().ResetCamera();
                    break;
                default:
                    type = 0;
                    mainCamera.SetActive(true);
                    otherCamera.SetActive(false);
                    TpsCamera.SetActive(false);
                    break;
            }
        }
        */
    }
}
