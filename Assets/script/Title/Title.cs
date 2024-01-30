using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    readonly int CURSOR_START = 0;
    readonly int CURSOR_EXIT = 1;
    int cursor;
    float inputVertical;
    float inputFire1;

    bool inputpush;

    private Text m_text;

    // Start is called before the first frame update
    void Start()
    {
        cursor = CURSOR_START;
        inputpush = false;

        inputVertical = 0.0f;
        inputFire1 = 0.0f;

       m_text = this.GetComponent<Text>();
    }
     

    // Update is called once per frame
    void Update()
    {
        if (cursor == CURSOR_START)
        {
            m_text.text = "SELECT PUSH FIRE1\n\n         >START\n           EXIT";
        }
        else if(cursor == CURSOR_EXIT)
        {
            m_text.text = "SELECT PUSH FIRE1\n\n           START\n         >EXIT";
        }

        //入力を取得-----------------------------------
        inputVertical = Input.GetAxisRaw("Vertical");
        inputFire1 = Input.GetAxisRaw("Fire1");
        //カーソルの動き
        if (inputVertical != 0.0f)
        {

            if (inputpush == false)
            {
                inputpush = true;
                cursor++;

                if (1 < cursor)
                {
                    cursor = 0;
                }
            }
        }
        else
        {
            inputpush = false;
        }
        //攻撃ボタン
        if (Input.GetAxisRaw("Fire1") == 1.0)
        {
            if (cursor == CURSOR_START)
            {
                SceneManager.LoadScene("SampleScene");
            }
            else
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
    UnityEngine.Application.Quit();
#endif
            }

        }
        //-----------------------------------------------
    }
}
