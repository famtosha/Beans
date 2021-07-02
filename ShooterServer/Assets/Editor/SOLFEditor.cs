using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SynchronizedObjectsFacotry))]
public class SOLFEditor : Editor
{
    private SynchronizedObjectsFacotry _factory;

    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Update indexes"))
        {
            UpdateIndexes();
            Debug.Log("Level object indexes was updated");
        }
        DrawDefaultInspector();
    }

    private void UpdateIndexes()
    {
        _factory = target as SynchronizedObjectsFacotry;

        for (int i = 0; i < _factory.assets.Count; i++)
        {
            var levelObject = _factory.assets[i].GetComponent<StaticLevelObject>();
            var serialisedObj = new SerializedObject(levelObject);
            var idProperty = serialisedObj.FindProperty("_assetID");
            idProperty.intValue = i;
            serialisedObj.ApplyModifiedProperties();
        }
    }
}
