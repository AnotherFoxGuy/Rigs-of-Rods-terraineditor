using System;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.SceneManagement;

public class Tobj : MonoBehaviour
{
    public static void Import(string file)
    {
        EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());

        var root = GameObject.Find("root");
        if (root != null)
        {
            DestroyImmediate(root);
        }
        root = new GameObject("root");

        print("Opening file: " + file);
        var terrain = GameObject.Find("Terrain");

        if (File.Exists(file) && terrain != null)
        {
            var td = terrain.GetComponent<Terrain>().terrainData;

            root.transform.position = new Vector3(0, 0, td.size.z);
            root.transform.localScale = new Vector3(1, 1, -1);

            var con = File.ReadAllLines(file);
            foreach (var s in con)
            {
                if (!s.Contains("//"))
                {
                    try
                    {
                        var obdata = Regex.Split(s, ",");
                        if (obdata.Length == 7)
                        {
                            var pos = new Vector3(Convert.ToSingle(obdata[0]), Convert.ToSingle(obdata[1]),
                                Convert.ToSingle(obdata[2]));
                            var rot = new Vector3(Convert.ToSingle(obdata[3]) - 90f, Convert.ToSingle(obdata[4]),
                                Convert.ToSingle(obdata[5]));

                            var obj = Resources.Load(obdata[6].Trim()) as GameObject;
                            GameObject o;

                            if (obj != null)
                                o = Instantiate(obj);
                            else
                                o = GameObject.CreatePrimitive(PrimitiveType.Cube);

                            o.transform.parent = root.transform;
                            o.transform.name = obdata[6].Trim();
                            o.transform.position = pos;
                            o.transform.eulerAngles = new Vector3(rot.x, -rot.y - 180, rot.z);
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError("Error while pasting the tobj-file \n" + ex.StackTrace);
                    }

                }
            }
            root.transform.position = Vector3.zero;
            root.transform.localScale = Vector3.one;

            foreach (Transform child in root.transform)
            {
                child.transform.localScale = Vector3.one;
            }
        }
    }

    public static void Export()
    {
        var root = GameObject.Find("root");
        var terrain = GameObject.Find("Terrain");
        if (root != null && terrain != null)
        {
            var td = terrain.GetComponent<Terrain>().terrainData;

            root.transform.position = new Vector3(0, 0, td.size.z);
            root.transform.localScale = new Vector3(1, 1, -1);

            var file = "// Created With unity Rigs of Rods Terrain Editor";
            foreach (Transform child in root.transform)
            {
                var p = child.position;
                var rot = child.transform.eulerAngles;
                var r = new Vector3(rot.x + 90, -rot.y + 180, rot.z);
                file += "\n" + p.x + ", \t" + p.y + ", \t" + p.z + ", \t" + Mathf.Repeat(r.x, 360) + ", \t" +
                        Mathf.Repeat(r.y, 360) + ", \t" + Mathf.Repeat(r.z, 360) + ", \t" +
                        child.name;
            }


            print("Saving file: " + EditorPrefs.GetString("projectPath") + "/" + PlayerSettings.productName + ".tobj");
            File.WriteAllText(EditorPrefs.GetString("projectPath") +"/"+ PlayerSettings.productName + ".tobj", file);

            root.transform.position = Vector3.zero;
            root.transform.localScale = Vector3.one;
        }
    }
}
