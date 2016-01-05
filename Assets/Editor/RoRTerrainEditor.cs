using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            Tobj.Export();
            Otc.ExportMain();
            Otc.ExportPage();
            var root = GameObject.Find("root");
            if (root != null)
            {
                var objectExported = new List<string>();
                var meshesExported = new List<string>();
                foreach (Transform child in root.transform)
                {
                    if (!objectExported.Contains(child.name) && child.tag != "DefaultContent")
                    {
                        objectExported.Add(child.name);
                        Odef.Export(child.gameObject);
                        /*
                        var m = child.GetComponentsInChildren<MeshFilter>();
                        var mr = child.GetComponentsInChildren<MeshRenderer>();


                        if (m.Length > 1 && !meshesExported.Contains(m[0].sharedMesh.name))
                        {
                            var combinedMesh = new Mesh();
                            var combineIns = new List<CombineInstance>();
                            var materials = new List<Material>();
                            for (var i = 0; i < m.Length; i++)
                            {
                                print("combing " + m[i].sharedMesh.name);
                                combineIns.Add(new CombineInstance
                                {
                                    mesh = m[i].sharedMesh,
                                    transform = m[i].transform.worldToLocalMatrix

                                });
                                materials.AddRange(mr[i].sharedMaterials);
                            }
                            combinedMesh.name = m[0].sharedMesh.name;
                            materials = materials.Distinct().ToList();
                            combinedMesh.CombineMeshes(combineIns.ToArray(), false);
                            Meshes.Export(combinedMesh, materials.ToArray());
                            Materials.Export(materials.ToArray());
                            meshesExported.Add(combinedMesh.name);
                        }
                        else if (m.Length == 1 && !meshesExported.Contains(m[0].sharedMesh.name))
                        {
                            Meshes.Export(m[0].sharedMesh, mr[0].sharedMaterials);
                            Materials.Export(mr[0].sharedMaterials);
                            meshesExported.Add(m[0].sharedMesh.name);
                        }
                        */
                    }
                }
                
                var m = root.GetComponentsInChildren<MeshFilter>();
                var mr = root.GetComponentsInChildren<MeshRenderer>();
                
                if (m.Length > 0)
                {
                    for (var i = 0; i < m.Length; i++)
                    {
                        if (!meshesExported.Contains(m[i].sharedMesh.name) && m[i].gameObject.tag != "DefaultContent")
                        {
                            Meshes.Export(m[i].sharedMesh, mr[i].sharedMaterials);
                            Materials.Export(mr[i].sharedMaterials);
                            meshesExported.Add(m[i].sharedMesh.name);
                        }
                    }
                }
                
            }
        }
    }

    [MenuItem("Rigs of Rods/Tools")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof (RoRTerrainEditorTools));
    }

    //[MenuItem("Rigs of Rods/Merge")]
    public static void merge()
    {
        var sel = Selection.activeGameObject;

        var m = sel.GetComponentsInChildren<MeshFilter>();
        var mr = sel.GetComponentsInChildren<MeshRenderer>();

        var combinedMesh = new Mesh();
        var combineIns = new List<CombineInstance>();
        var materials = new List<Material>();
        for (var i = 0; i < m.Length; i++)
        {
            print("combing " + m[i].sharedMesh.name);
            combineIns.Add(new CombineInstance
            {
                mesh = m[i].sharedMesh,
                //subMeshIndex = i
            });
            materials.AddRange(mr[i].sharedMaterials);
        }
        combinedMesh.name = m[0].sharedMesh.name;
        materials = materials.Distinct().ToList();
        combinedMesh.CombineMeshes(combineIns.ToArray(), false,false);

        var no = new GameObject("combine");
        no.transform.position = sel.transform.position;
        no.AddComponent<MeshFilter>().mesh = combinedMesh;
        no.AddComponent<MeshRenderer>().materials = materials.ToArray();
    }
}