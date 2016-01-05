using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class Terrn : MonoBehaviour
{
    //[MenuItem("Rigs of Rods/Export Texture")]
    public static void ExportTextures()
    {
        var terrain = GameObject.Find("Terrain");
        if (terrain != null)
        {
            var data = terrain.GetComponent<Terrain>().terrainData;
            var texture = data.alphamapTextures;
            foreach (var texture2D in texture)
            {
                var bytes = texture2D.EncodeToPNG();
                File.WriteAllBytes(EditorPrefs.GetString("projectPath") + "/" + texture2D.name + ".png", bytes);
            }
        }
    }

    public static void ExportTerrn()
    {
        var root = GameObject.Find("root");
        var terrain = GameObject.Find("Terrain");
        if (root != null && terrain != null)
        {
            var td = terrain.GetComponent<Terrain>().terrainData;
            root.transform.position = new Vector3(0, 0, td.size.z);
            root.transform.localScale = new Vector3(1, 1, -1);

            var StartPosition = GameObject.Find("StartPosition");
            var fileContent = new List<string>();
            fileContent.Add("[General]");
            fileContent.Add("Name = " + PlayerSettings.productName);
            fileContent.Add("GeometryConfig = " + PlayerSettings.productName + ".otc");
            if (StartPosition != null)
                fileContent.Add("StartPosition = " + StartPosition.transform.position.x + ", " +
                                StartPosition.transform.position.y + ", " + StartPosition.transform.position.z);
            fileContent.Add("Water=0");
            fileContent.Add("CategoryID = 129");
            fileContent.Add("Version = 1");
            fileContent.Add("[Authors]");
            fileContent.Add("terrain = " + PlayerSettings.companyName);
            fileContent.Add("[Objects]");
            fileContent.Add(PlayerSettings.productName + ".tobj=");
            File.WriteAllLines(EditorPrefs.GetString("projectPath") + "/" + PlayerSettings.productName + ".terrn2",
                fileContent.ToArray());

            root.transform.position = Vector3.zero;
            root.transform.localScale = Vector3.one;
        }
    }
}