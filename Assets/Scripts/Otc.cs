using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class Otc : MonoBehaviour
{
    public static void ExportMain()
    {
        var terrain = GameObject.Find("Terrain");
        if (terrain != null)
        {
            var terrndata = terrain.GetComponent<Terrain>().terrainData;
            var fileContent = new List<string>();

            fileContent.Add("WorldSizeX=" + terrndata.size.x);
            fileContent.Add("WorldSizeZ=" + terrndata.size.z);
            fileContent.Add("WorldSizeY=" + terrndata.size.y);
            fileContent.Add("PageSize = " + terrndata.heightmapResolution);
            fileContent.Add("PageFileFormat = " + PlayerSettings.productName + "-page-0-0.otc");
            fileContent.Add("");
            fileContent.Add("Heightmap.0.0.raw.size = " + terrndata.heightmapResolution);
            fileContent.Add("Heightmap.0.0.raw.bpp = 2");
            fileContent.Add("Heightmap.0.0.flipX = 0");
            fileContent.Add("");
            fileContent.Add("disableCaching = 1");
            fileContent.Add("LightmapEnabled = 0");
            fileContent.Add("NormalMappingEnabled = 0");
            fileContent.Add("SpecularMappingEnabled = 1");
            fileContent.Add("ParallaxMappingEnabled = 0");
            fileContent.Add("GlobalColourMapEnabled = 0");
            fileContent.Add("ReceiveDynamicShadowsDepth = 0");

            File.WriteAllLines(EditorPrefs.GetString("projectPath") + "/" + PlayerSettings.productName + ".otc",
                fileContent.ToArray());
            Debug.Log("generating " + EditorPrefs.GetString("projectPath") + "/" + PlayerSettings.productName + ".otc");
        }
    }

    public static void ExportPage()
    {
        var file = EditorPrefs.GetString("projectPath") + "/" + PlayerSettings.productName + "-page-0-0.otc";
        var terrain = GameObject.Find("Terrain");
        if (terrain != null && !File.Exists(file))
        {         
            var fileContent = new List<string>();

            fileContent.Add(PlayerSettings.productName + ".raw");
            fileContent.Add("1");
            fileContent.Add("; Params: [worldSize], [diffusespecular], [normalheight], [blendmap], [blendmapmode], [alpha]");
            fileContent.Add("; The ground layer:");
            fileContent.Add("5 ,airportgrass.dds , airportgrass.dds");
            fileContent.Add("; Other layers:");

            File.WriteAllLines(file,
                fileContent.ToArray());
            Debug.Log("generating " + file);
        }
    }
}