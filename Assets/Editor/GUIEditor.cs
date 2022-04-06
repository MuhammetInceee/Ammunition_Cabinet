using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PhaseManager))]

public class GUIEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        PhaseManager _phaseManager = (PhaseManager)target;
        //_phaseManager._zVariable = EditorGUILayout.FloatField("Uzaklık", _phaseManager._zVariable);
        if (GUILayout.Button("Create Level"))
        {
            _phaseManager.GenerateLevel();
        }
        if (GUILayout.Button("Destroy All"))
        {
            _phaseManager.DeleteAll();
        }
    }
}
