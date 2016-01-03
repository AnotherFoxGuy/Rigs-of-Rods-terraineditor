using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

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
                var bytes = texture2D.EncodeToJPG();
                File.WriteAllBytes(EditorPrefs.GetString("projectPath") + "/" + texture2D.name + ".jpg", bytes);
            }
        }          
    }

    public static void ExportTerrn()
    {
        var fileContent = new List<string>();
        fileContent.Add("[General]");
        fileContent.Add("Name = " + PlayerSettings.productName);
        fileContent.Add("[Authors]");
        fileContent.Add("terrain = " + PlayerSettings.companyName);
        fileContent.Add("[Objects]");
        fileContent.Add(PlayerSettings.productName+".tobj=");
        File.WriteAllLines(EditorPrefs.GetString("projectPath") + "/" + PlayerSettings.productName + ".terrn2", fileContent.ToArray());
    }
}
