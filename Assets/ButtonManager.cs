using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject Button;
    [SerializeField] Sprite StartBtnImage;//録画開始用のボタン
    [SerializeField] Sprite StopBtnImage;//録画終了用のボタン
    Image BtnImage;//ボタンの画像

    private bool isStart = true;//ボタンのステート管理フラグ

/*    private bool clickable = false; //ボタン押下許可フラグ
    private const float clickSpan = 1.0f;//次にクリックできるまでの時間
    private float timer = 0f;*/

    void Awake()
    {
        Debug.Log("isStart; " + isStart);
        //BtnSprite = Button.GetComponent<SpriteRenderer>();
        BtnImage = Button.GetComponent<Image>();
    }

/*    void Update()
    {
        if (clickable == false)
        {
            return;
        }

        clickable = false;

        StartAndStopButton();
    }*/

    public void StartAndStopButton()
    {
        if (isStart)
        {
            StartRecording();
        }
        else
        {
            StopRecording();
        }


        /*if (clickable)
        {
            if (isStart)
            {
                StartRecording();
            } else
            {
                StopRecording();
            }
        } else
        {
            Debug.Log("clickable is false");
            return;
        }*/
        
    }

    void StartRecording()
    {
        //ここに録画開始処理を追加する
        Debug.Log("called StartRecording");
        isStart = !isStart;
        BtnImage.sprite = StopBtnImage;
    }

    void StopRecording()
    {
        //ここに録画終了処理を追加する
        Debug.Log("called StopRecording");
        isStart = !isStart;
        BtnImage.sprite = StartBtnImage;
    }
}
