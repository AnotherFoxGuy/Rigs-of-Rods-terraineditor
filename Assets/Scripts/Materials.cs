using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class Materials : MonoBehaviour
{
    public static void Export(Material[] materials)
    {
        foreach (var material in materials)
        {
            var texture = Path.GetFileName(AssetDatabase.GetAssetPath(material.mainTexture));
            if (texture.Length > 1)
            {
                var localTexture = Application.dataPath + "ncfsek";
                localTexture = localTexture.Replace("/Assetsncfsek", "/");
                localTexture += AssetDatabase.GetAssetPath(material.mainTexture);
                if (!File.Exists(EditorPrefs.GetString("projectPath") + "/" + texture))
                {
                    File.Copy(localTexture, EditorPrefs.GetString("projectPath") + "/" + texture);
                }

            }
            var divc = material.color;

            var fileContent = new List<string>();
            fileContent.Add("material " + material.name);
            fileContent.Add("{");
            fileContent.Add("   technique ");
            fileContent.Add("   {");
            fileContent.Add("       pass");
            fileContent.Add("       {");
            fileContent.Add("           diffuse " + divc.r + " " + divc.g + " " + divc.b + " " + divc.a);
            if (texture.Length > 1)
            {
                fileContent.Add("           texture_unit");
                fileContent.Add("           {");
                fileContent.Add("               texture " + texture);
                fileContent.Add("           }");
            }
            fileContent.Add("       }");
            fileContent.Add("   }");
            fileContent.Add("}");

            File.WriteAllLines(EditorPrefs.GetString("projectPath") + "/" + material.name + ".material",
                fileContent.ToArray());
        }
    }
}