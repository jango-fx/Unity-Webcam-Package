using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using UnityEditor.Recorder;
//using UnityEditor.Recorder.Input;

[ExecuteInEditMode]
public class GrabWebCam : MonoBehaviour
{
    public string name = "FaceTime HD Camera";
    [SerializeField] public WebCamTexture theWebCamTexture;
    public RenderTexture theRecorderRenderTexture;
    public Texture theTexture;
    //public Texture2D theTexture2D;
    public MeshRenderer theRenderer;
    public bool IsRecordingRenderTexture = false;


    void OnEnable()
    {
        string[] cams = GetCams();
        theWebCamTexture = new WebCamTexture();
        if (name != "")
            theWebCamTexture.deviceName = name;
        Debug.Log("[WEBCAM]: chose ›" + theWebCamTexture.deviceName + "‹");
        int w = theWebCamTexture.width;
        int h = theWebCamTexture.height;
        Debug.Log("[WEBCAM]: camera size: " + w + "x" + h);


        //theRecorderRenderTexture = GetComponent<Camera>().targetTexture;

        theWebCamTexture.Play();

        theTexture = theWebCamTexture;
        theRenderer = GetComponent<MeshRenderer>();
        theRenderer.material.SetTexture("_MainTex", theTexture);
    }
    string[] GetCams()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        string[] names = new string[devices.Length];
        string msg = "[WEBCAM]: " + devices.Length + " cameras found:";
        for (int i = 0; i < devices.Length; i++)
        {
            names[i] = devices[i].name;
            msg += "\n\t[" + i + "]: " + names[i];
        }
        Debug.Log(msg);
        return names;
    }

    void Update()
    {
        if (theWebCamTexture.width < 100)
        {
            Debug.Log("Still waiting another frame for correct info...");
            return;
        }
        if (IsRecordingRenderTexture)
            UpdateRenderTexture();
    }

    void UpdateRenderTexture()
    {
        /*
        if (theRecorderRenderTexture.width != theWebCamTexture.width)
        {
            int w = theWebCamTexture.width;
            int h = theWebCamTexture.height;
            
            Debug.Log("webcam size: " + w + "x" + h);

            theRecorderRenderTexture.width = w;
            theRecorderRenderTexture.height = h;

            float videoRatio = (float)w / (float)h;
        }
        */
        Graphics.Blit(theTexture, theRecorderRenderTexture);
    }
    void OnDisable() { theWebCamTexture.Stop(); }


    //void VideoRecordingStop() { TestRecorderController.StopRecording(); }
    /*
    void VideoRecordingStart()
    {
        if (theRecorderRenderTexture == null)
        {
            Debug.LogError($"You must assign a valid renderTexture before entering Play Mode");
            return;
        }

        UnityEditor.Recorder.RecorderOptions.VerboseMode = true;

        var controllerSettings = ScriptableObject.CreateInstance<UnityEditor.Recorder.RecorderControllerSettings>();
        var TestRecorderController = new UnityEditor.Recorder.RecorderController(controllerSettings);

        var videoRecorder = ScriptableObject.CreateInstance<UnityEditor.Recorder.MovieRecorderSettings>();
        videoRecorder.name = "WebCamRecorder";
        videoRecorder.Enabled = true;

        videoRecorder.ImageInputSettings = new UnityEditor.Recorder.Input.RenderTextureInputSettings()
        {
            OutputWidth = theRecorderRenderTexture.width,
            OutputHeight = theRecorderRenderTexture.height,
            FlipFinalOutput = true,
            RenderTexture = theRecorderRenderTexture
        };

        videoRecorder.AudioInputSettings.PreserveAudio = true;

        controllerSettings.AddRecorderSettings(videoRecorder);
        controllerSettings.SetRecordModeToFrameInterval(0, 59); // 2s @ 30 FPS
        controllerSettings.FrameRate = 60;
        TestRecorderController.PrepareRecording();
        TestRecorderController.StartRecording();
    }
    */
}
