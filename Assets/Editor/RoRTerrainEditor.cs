using UnityEditor;
using UnityEngine;

public class TerrainEditor : MonoBehaviour
{
    [MenuItem("Rigs of Rods/Import")]
    public static void Import()
    {
        var file = EditorUtility.OpenFilePanelWithFilters("Open tobj file", "/", new[] { "Rigs of Rods tobj file", "tobj" });
        Tobj.Import(file);
    }

    [MenuItem("Rigs of Rods/Export")]
    public static void Export()
    {
        var writeFile = EditorUtility.SaveFilePanel("Save tobj file", "/", "", "tobj");
        Tobj.Export(writeFile);
    }

    [MenuItem("Rigs of Rods/Tools")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof (RoRTerrainEditorTools));
    }
}