using UnityEngine;
using System.IO;
using UnityEditor;

public class TerrnExporter : MonoBehaviour
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
            File.WriteAllBytes(texture2D.name+".jpg", bytes);
        }
        
    }
}
