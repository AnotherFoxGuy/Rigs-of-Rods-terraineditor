using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class Odef : MonoBehaviour
{
    public static void Export(GameObject gameObject)
    {
        var mesh = new List<MeshFilter>();
        //mesh.Add(gameObject.GetComponent<MeshFilter>());
        mesh.Add(gameObject.GetComponentInChildren<MeshFilter>());
        if (mesh.Count > 0)
        {
            var scale = gameObject.transform.localScale;
            var fileContent = new List<string>();
            foreach (var meshFilter in mesh)
            {
                if (meshFilter != null)
                {
                    fileContent.Add(meshFilter.sharedMesh.name + ".mesh");
                    fileContent.Add(scale.x + ", " + scale.y + ", " + scale.z);
                    fileContent.Add("beginmesh");

                    fileContent.Add("mesh " + meshFilter.sharedMesh.name + ".mesh");

                    fileContent.Add("endmesh");
                    fileContent.Add("end");
                }
            }
            File.WriteAllLines(EditorPrefs.GetString("projectPath") + "/" + gameObject.name + ".odef",
                fileContent.ToArray());
            Debug.Log("generating " + EditorPrefs.GetString("projectPath") + "/" + gameObject.name + ".odef");
        }
    }
}