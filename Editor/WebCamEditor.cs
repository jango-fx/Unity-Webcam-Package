using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using ƒx.UnityUtils.Webcam;

namespace ƒx.UnityUtils.Webcam
{
    [CustomEditor(typeof(GrabWebCam))]
    public class WebCamEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            GrabWebCam script = (GrabWebCam)target;
            GUIContent cameraList = new GUIContent("Select camera: ");
            script.cameraID = EditorGUILayout.Popup(cameraList, script.cameraID, script.GetCams());

            base.OnInspectorGUI();

        }
    }
}