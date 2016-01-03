using UnityEngine;
using System.Collections.Generic;
using System.IO;
using UnityEditor;

public class Terrn : MonoBehaviour
{
    [MenuItem("Rigs of Rods/Export Texture")]
    public static void ExportTexture()
    {
        var terrain = GameObject.Find("Terrain");
        var data = terrain.GetComponent<Terrain>().terrainData;
        var texture = data.alphamapTextures;
        foreach (var texture2D in texture)
        {
            var bytes = texture2D.EncodeToJPG();
            File.WriteAllBytes(texture2D.name + ".jpg", bytes);
        }

    }

    public static void ExportTerrn()
    {
        var tn = PlayerSettings.productName;
        var fileContent = new List<string>();
    }
}
