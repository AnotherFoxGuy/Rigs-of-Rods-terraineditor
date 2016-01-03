using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class TerrainEditor : MonoBehaviour
{
    //[MenuItem("Rigs of Rods/Import")]
    public static void Import()
    {
        var file = EditorUtility.OpenFilePanelWithFilters("Open tobj file", "/",
            new[] {"Rigs of Rods tobj file", "tobj"});
        Tobj.Import(file);
    }

    [MenuItem("Rigs of Rods/Export")]
    public static void Export()
    {
        var p = EditorPrefs.GetString("projectPath");
        if (Directory.Exists(p))
        {
            Terrn.ExportTerrn();
            Terrn.ExportTextures();

            var root = GameObject.Find("root");
            if (root != null)
            {
                var m = root.GetComponentsInChildren<MeshFilter>();
                var mr = root.GetComponentsInChildren<MeshRenderer>();
                var mp = new List<string>();
                if (m.Length > 0)
                {
                    for (var i = 0; i < m.Length; i++)
                    {
                        if (!mp.Contains(m[i].sharedMesh.name))
                        {
                            Objects.ExportMesh(m[i].sharedMesh, mr[i].sharedMaterials);
                            Objects.ExportMaterial(mr[i].sharedMaterials);
                            mp.Add(m[i].sharedMesh.name);
                        }
                        
                    }
                }
            }

        }
        else
        {

        }
    }

    [MenuItem("Rigs of Rods/Tools")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof (RoRTerrainEditorTools));
    }
}