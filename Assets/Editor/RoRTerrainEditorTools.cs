using UnityEngine;
using UnityEditor;

public class RoRTerrainEditorTools : EditorWindow
{
    private string projectPath;

    public void OnEnable()
    {
        projectPath = EditorPrefs.GetString("projectPath");
    }

    void OnGUI()
    {
        GUI.TextField(new Rect(10, 10, 120, 20), projectPath);

        if (GUI.Button(new Rect(130, 10, 120, 20), "Set project path"))
        {
            projectPath = EditorUtility.SaveFolderPanel("project path", "/", "");
            EditorPrefs.SetString("projectPath", projectPath);
        }
    }
}