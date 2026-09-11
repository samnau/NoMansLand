using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Collections.Generic;

[CustomEditor(typeof(SceneFadeTrigger))]
public class SceneFadeTriggerEditor : Editor
{
    private SerializedProperty targetSceneNameProperty;
    private string[] sceneNames;

    private void OnEnable()
    {
        targetSceneNameProperty = serializedObject.FindProperty("targetSceneName");
        LoadSceneNames();
    }

    private void LoadSceneNames()
    {
        var scenes = new List<string>();
        
        // Get all scenes from build settings
        foreach (var scene in EditorBuildSettings.scenes)
        {
            if (scene.enabled)
            {
                string scenePath = scene.path;
                string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
                scenes.Add(sceneName);
            }
        }

        sceneNames = scenes.ToArray();
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawDefaultInspector();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Scene Selection", EditorStyles.boldLabel);

        // Show the dropdown for targetSceneName
        if (sceneNames != null && sceneNames.Length > 0)
        {
            string currentScene = targetSceneNameProperty.stringValue;

            // Find current index
            int currentSceneIndex = System.Array.IndexOf(sceneNames, currentScene);
            if (currentSceneIndex < 0) currentSceneIndex = 0;

            // Dropdown showing scene names
            int newSceneIndex = EditorGUILayout.Popup("Target Scene", currentSceneIndex, sceneNames);

            if (newSceneIndex != currentSceneIndex)
            {
                targetSceneNameProperty.stringValue = sceneNames[newSceneIndex];
            }
        }
        else
        {
            EditorGUILayout.HelpBox("No scenes found in Build Settings. Add scenes to Build Settings first.", MessageType.Warning);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
