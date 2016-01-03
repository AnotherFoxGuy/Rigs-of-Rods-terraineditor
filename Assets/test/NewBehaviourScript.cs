using UnityEngine;
using System.IO;
using UnityEditor;

public class NewBehaviourScript : MonoBehaviour
{
   
    [ContextMenu("mesh")]
    public void testmeshexp()
    {
        Objects.ExportMesh(GetComponent<MeshFilter>().sharedMesh, GetComponent<MeshRenderer>().sharedMaterials);
    }
    [ContextMenu("mat")]
    public void testmatexp()
    {
        Objects.ExportMaterial(GetComponent<MeshRenderer>().sharedMaterials);
    }
    [ContextMenu("test")]
    public void test()
    {
        var p = AssetDatabase.GetAssetPath(GetComponent<MeshFilter>().sharedMesh);
        
        Debug.Log(Path.GetFileName(p));
    }
    
}