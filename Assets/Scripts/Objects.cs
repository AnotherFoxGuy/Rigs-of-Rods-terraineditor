using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEditor;
using UnityEngine;

public class Objects : MonoBehaviour
{
    public static void ExportMaterial(Material[] materials)
    {
        foreach (var material in materials)
        {
            var fileContent = new List<string>();
            fileContent.Add("material " + material.name);
            fileContent.Add("{");
            fileContent.Add("   technique ");
            fileContent.Add("   {");
            fileContent.Add("       pass");
            fileContent.Add("       {");
            fileContent.Add("           diffuse " + material.color.r + " ," + material.color.g + " ," + material.color.b +
                            " ," + material.color.a);
            fileContent.Add("           texture_unit");
            fileContent.Add("           {");
            fileContent.Add("               texture " +
                            Path.GetFileName(AssetDatabase.GetAssetPath(material.mainTexture)));
            fileContent.Add("           }");
            fileContent.Add("       }");
            fileContent.Add("   }");
            fileContent.Add("}");

            File.WriteAllLines(material.name + ".material", fileContent.ToArray());
        }

    }

    public static void ExportMesh(Mesh mesh, Material[] materials)
    {
        if (mesh.subMeshCount != materials.Length)
        {
            throw new UnityException("subMesh != materials");
        }

        var settings = new XmlWriterSettings
        {
            Indent = true,
            OmitXmlDeclaration = true,
            NewLineOnAttributes = true
        };

        using (XmlWriter writer = XmlWriter.Create(mesh.name + ".mesh.xml", settings))
        {

            writer.WriteStartElement("mesh");
            writer.WriteStartElement("sharedgeometry");
            writer.WriteAttributeString("vertexcount", mesh.vertices.Length.ToString());

            writer.WriteStartElement("vertexbuffer");
            writer.WriteAttributeString("positions", "true");
            writer.WriteAttributeString("normals", "true");
            writer.WriteAttributeString("texture_coords", "1");

            for (int i = 0; i < mesh.vertices.Length; i++)
            {
                writer.WriteStartElement("vertex");

                writer.WriteStartElement("position");
                writer.WriteAttributeString("x", mesh.vertices[i].x.ToString());
                writer.WriteAttributeString("y", mesh.vertices[i].y.ToString());
                writer.WriteAttributeString("z", mesh.vertices[i].z.ToString());
                writer.WriteEndElement(); //position

                writer.WriteStartElement("normal");
                writer.WriteAttributeString("x", mesh.normals[i].x.ToString());
                writer.WriteAttributeString("y", mesh.normals[i].y.ToString());
                writer.WriteAttributeString("z", mesh.normals[i].z.ToString());
                writer.WriteEndElement(); //normal

                writer.WriteStartElement("texcoord");
                writer.WriteAttributeString("u", mesh.uv[i].y.ToString());
                writer.WriteAttributeString("v", mesh.uv[i].x.ToString());
                writer.WriteEndElement(); //texcoord

                writer.WriteEndElement(); //vertex

            }
            writer.WriteEndElement(); //vertexbuffer
            writer.WriteEndElement(); //sharedgeometry

            writer.WriteStartElement("submeshes");
            for (int i = 0; i < mesh.subMeshCount; i++)
            {
                writer.WriteStartElement("submesh");
                writer.WriteAttributeString("material", materials[i].name);

                var smtr = mesh.GetTriangles(i);
                writer.WriteStartElement("faces");

                for (int j = 0; j < smtr.Length; j += 3)
                {
                    writer.WriteStartElement("face");
                    writer.WriteAttributeString("v1", smtr[j].ToString());
                    writer.WriteAttributeString("v2", smtr[j + 1].ToString());
                    writer.WriteAttributeString("v3", smtr[j + 2].ToString());
                    writer.WriteEndElement(); //face
                }
                writer.WriteEndElement(); //faces
                writer.WriteEndElement(); //submesh
            }
            writer.WriteEndElement(); //submeshes

            writer.WriteStartElement("submeshnames");
            for (int i = 0; i < mesh.subMeshCount; i++)
            {
                writer.WriteStartElement("submeshname");
                writer.WriteAttributeString("name", materials[i].name);
                writer.WriteAttributeString("index", i.ToString());
                writer.WriteEndElement(); //submeshname
            }
            writer.WriteEndElement(); //submeshnames


            writer.WriteEndElement(); //mesh
        }
    }
}