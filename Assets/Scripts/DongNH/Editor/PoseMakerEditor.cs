using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PoseMaker))]
public class PoseMakerEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PoseMaker myScript = (PoseMaker)target;
        if (GUILayout.Button("SAVE"))
        {
            myScript.SavePose();
        }
        if (GUILayout.Button("LOAD"))
        {
            myScript.LoadPose();
        }
        if (GUILayout.Button("pose"))
        {
            myScript.MaKeStatic();
        }
    }
    // Use this for initialization
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}
}


