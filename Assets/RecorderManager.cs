using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using UnityEngine;


public class RecorderManager : MonoBehaviour
{
    [SerializeField] private GameObject Button;
    [SerializeField] Sprite StartBtnImage;//録画開始用のボタン
    [SerializeField] Sprite StopBtnImage;//録画終了用のボタン
    Image BtnImage;//ボタンの画像
    private bool isStart = true;//ボタンのステート管理フラグ

    //Sunshine Android Native Screen Recorder
    #region Sunshine Android Native Screen Recorder variable
    [SerializeField] private string folderName;
    [SerializeField] private bool isAudioRecording = true;
    [SerializeField] private int bitrate;
    [SerializeField] private int fps;
    [SerializeField] private SmileSoftScreenRecordController.VideoEncoder videoEncoder = SmileSoftScreenRecordController.VideoEncoder.H264;
    [SerializeField] private Text savedPathText;
    private string _recordedFilePath;
    #endregion


    void Awake()
    {
        Debug.Log("isStart; " + isStart);
        BtnImage = Button.GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetUp();
    }

    #region Sunshine Android Native Screen Recorder method
    void SetUp()
    {
        // If want to store video in persistant data Path (Private Path) then use following line
        //SmileSoftScreenRecordController.instance.SetVideoStoringDestination(Application.persistentDataPath);
        //Do not want to show stored videos in gallery ,then uncomment following line.
        //SmileSoftScreenRecordController.instance.SetGalleryAddingCapabilities(false);

        SmileSoftScreenRecordController.instance.SetStoredFolderName(folderName); // only Android
        SmileSoftScreenRecordController.instance.SetBitRate(bitrate); // only Android
        SmileSoftScreenRecordController.instance.SetFPS(fps); // only Android
        SmileSoftScreenRecordController.instance.SetVideoEncoder((int)videoEncoder); // only Android
        SmileSoftScreenRecordController.instance.SetVideoSize((int)(Screen.width), (int)(Screen.height)); // only Android

        SmileSoftScreenRecordController.instance.SetAudioCapabilities(isAudioRecording);  // both Android & iOS
    }

    private void SetFileName()
    {
        System.DateTime now = System.DateTime.Now;
        string date = now.ToShortDateString().Replace('/', '_')
                    + now.ToLongTimeString().Replace(':', '_');
        string fileName = "Record_" + date;

        SmileSoftScreenRecordController.instance.SetVideoName(fileName);
        Debug.Log("FileName: " + fileName);
    }

    public void PreviewVideo()
    {
        //for iOS this perameter is not affect anything. You can just send a null or empty value as a perameter for iOS 
        SmileSoftScreenRecordController.instance.PreviewVideo(_recordedFilePath);
    }

    private void ShowAndroidVideoCompletatoonDialog()
    {
        //afterVideoCompletePanel.SetActive(true);
        if (_recordedFilePath != null && File.Exists(_recordedFilePath))
        {
            //previewButton.interactable = true;
            //ShareButton.interactable = true;
            savedPathText.text = "Video saved successfully at : " + _recordedFilePath;
        }
        else
        {
            //previewButton.interactable = false;
            //ShareButton.interactable = false;
            savedPathText.text = "Error occured. Can not record video";
        }

    }

    #endregion

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
    }

    void StartRecording()
    {
        //ボタン切り替え処理
        Debug.Log("called StartRecording");
        isStart = !isStart;
        BtnImage.sprite = StopBtnImage;

        //ここに録画開始処理を追加する
        SetFileName();

        bool iSitIos = SmileSoftScreenRecordController.instance.IsIosPlatform();
        // If it is iOS 
        if (iSitIos && SmileSoftScreenRecordController.instance.IsRecordingAvailable() == false)
        {
            // show error message
            //alertPanel.ShowAlert("Recorder Unavailable in this device!");
            return;
        }
        SmileSoftScreenRecordController.instance.StartRecording();
    }

    void StopRecording()
    {
        //ボタン切り替え処理
        Debug.Log("called StopRecording");
        isStart = !isStart;
        BtnImage.sprite = StartBtnImage;
        //ここに録画終了処理を追加する
        _recordedFilePath = null;
        _recordedFilePath = SmileSoftScreenRecordController.instance.StopRecording();
        Debug.Log("File Path: " + _recordedFilePath);

        if (SmileSoftScreenRecordController.instance.IsIosPlatform())
        {
            PreviewVideo();
        }
        /*if (SmileSoftScreenRecordController.instance.IsAndroidPlatform())
        {
            ShowAndroidVideoCompletatoonDialog();
        }*/

    }
}
