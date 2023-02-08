using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject Button;
    [SerializeField] Sprite StartBtnImage;//�^��J�n�p�̃{�^��
    [SerializeField] Sprite StopBtnImage;//�^��I���p�̃{�^��
    Image BtnImage;//�{�^���̉摜

    private bool isStart = true;//�{�^���̃X�e�[�g�Ǘ��t���O

/*    private bool clickable = false; //�{�^���������t���O
    private const float clickSpan = 1.0f;//���ɃN���b�N�ł���܂ł̎���
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
        //�����ɘ^��J�n������ǉ�����
        Debug.Log("called StartRecording");
        isStart = !isStart;
        BtnImage.sprite = StopBtnImage;
    }

    void StopRecording()
    {
        //�����ɘ^��I��������ǉ�����
        Debug.Log("called StopRecording");
        isStart = !isStart;
        BtnImage.sprite = StartBtnImage;
    }
}
