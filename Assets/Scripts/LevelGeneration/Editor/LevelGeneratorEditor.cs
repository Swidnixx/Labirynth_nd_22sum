using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelGenerator))]
public class LevelGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if(GUILayout.Button("Generate"))
        {
            LevelGenerator generator = (LevelGenerator)target;
            generator.GenerateMap();
        }

        if (GUILayout.Button("Clear"))
        {
            LevelGenerator generator = (LevelGenerator)target;
            generator.DestroyMap();
        }
    }
}
