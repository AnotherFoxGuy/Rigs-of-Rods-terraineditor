// Decompiled with JetBrains decompiler
// Type: MeshTools2.Module1
// Assembly: MeshTools2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 889FFD51-9771-440E-BB1B-D088F9E416EC
// Assembly location: E:\Users\hasel\Downloads\MeshTools2\MeshTools2.exe

using System;
using System.Collections.Generic;
using System.IO;

namespace MeshTools2
{
    internal sealed class Module1
    {
        private static readonly List<xyz> vertices = new List<xyz>();
        private static readonly List<uv> uvs = new List<uv>();
        private static readonly List<xyz> normals = new List<xyz>();
        private static readonly List<face> faces = new List<face>();
        private static readonly List<string> submeshNames = new List<string>();

        public static void Main()
        {
            var path = ".mesh.xml";
            Console.WriteLine("Converting  to XML Mesh format...");

            var index = 0;
            while (index < 10)
            {
                var str = "";
                if (str.StartsWith("vn "))
                {
                    var num1 = Convert.ToSingle(str.Split(' ')[1]);
                    var num2 = Convert.ToSingle(str.Split(' ')[2]);
                    var num3 = Convert.ToSingle(str.Split(' ')[3]);
                    normals.Add(new xyz
                    {
                        x = Convert.ToString(num1),
                        y = Convert.ToString(num2),
                        z = Convert.ToString(num3)
                    });
                }
                if (str.StartsWith("vt "))
                {
                    var num1 = Convert.ToSingle(str.Split(' ')[1]);
                    var num2 = Convert.ToSingle(str.Split(' ')[2]);
                    uvs.Add(new uv
                    {
                        u = Convert.ToString(num1),
                        v = Convert.ToString(num2)
                    });
                }
                if (str.StartsWith("g "))
                    submeshNames.Add(str.Split(' ')[1]);
                if (str.StartsWith("v "))
                {
                    var num1 = Convert.ToSingle(str.Split(' ')[1]);
                    var num2 = Convert.ToSingle(str.Split(' ')[2]);
                    var num3 = Convert.ToSingle(str.Split(' ')[3]);
                    vertices.Add(new xyz
                    {
                        x = Convert.ToString(num1),
                        y = Convert.ToString(num2),
                        z = Convert.ToString(num3)
                    });
                }
                if (str.StartsWith("f "))
                {
                    var num1 = Convert.ToSingle(str.Split(' ')[1].Split('/')[0]);
                    var num2 = Convert.ToSingle(str.Split(' ')[2].Split('/')[0]);
                    var num3 = Convert.ToSingle(str.Split(' ')[3].Split('/')[0]);
                    faces.Add(new face
                    {
                        i0 = Convert.ToString(num1),
                        i1 = Convert.ToString(num2),
                        i2 = Convert.ToString(num3)
                    });
                }
                checked
                {
                    ++index;
                }
            }
            var newLine = Environment.NewLine;
            var str1 = "";
            if (submeshNames.Count == 0)
                submeshNames.Add("");
            var str2 = str1 + "<mesh>" + newLine + "<submeshes>" + newLine +
                       "<submesh operationtype=\"triangle_list\" use32bitindexes=\"false\" usesharedvertices=\"false\" material=\"" +
                       submeshNames[0] + "\">" + newLine + "<faces count=\"" +
                       Convert.ToString(faces.Count) + "\">" + newLine;
            List<face>.Enumerator enumerator1;
            try
            {
                enumerator1 = faces.GetEnumerator();
                while (enumerator1.MoveNext())
                {
                    var current = enumerator1.Current;
                    str2 = str2 + "<face v3=\"" + Convert.ToString(Convert.ToDouble(current.i2) - 1.0) +
                           "\" v2=\"" + Convert.ToString(Convert.ToDouble(current.i1) - 1.0) + "\" v1=\"" +
                           Convert.ToString(Convert.ToDouble(current.i0) - 1.0) + "\" />" + newLine;
                }
            }
            finally
            {
            }
            var str3 = str2 + "</faces>" + newLine + "<geometry vertexcount=\"" +
                       Convert.ToString(vertices.Count) + "\">" + newLine +
                       "<vertexbuffer positions=\"true\" colours_diffuse=\"true\" >" + newLine;
            List<xyz>.Enumerator enumerator2;
            try
            {
                enumerator2 = vertices.GetEnumerator();
                while (enumerator2.MoveNext())
                {
                    var current = enumerator2.Current;
                    str3 = str3 + "<vertex>" + newLine;
                    str3 = str3 + "<position z=\"" + current.z + "\" y=\"" + current.y + "\" x=\"" + current.x +
                           "\" />" + newLine;
                    str3 = str3 + "<colour_diffuse value=\"1 1 1 1\" />" + newLine;
                    str3 = str3 + "</vertex>" + newLine;
                }
            }
            finally
            {
            }
            var str4 = str3 + "</vertexbuffer>" + newLine + "<vertexbuffer normals=\"true\">" + newLine;
            List<xyz>.Enumerator enumerator3;
            try
            {
                enumerator3 = normals.GetEnumerator();
                while (enumerator3.MoveNext())
                {
                    var current = enumerator3.Current;
                    str4 = str4 + "<vertex>" + newLine;
                    str4 = str4 + "<normal z=\"" + current.z + "\" y=\"" + current.y + "\" x=\"" + current.x +
                           "\" />" + newLine;
                    str4 = str4 + "</vertex>" + newLine;
                }
            }
            finally
            {
            }
            var str5 = str4 + "</vertexbuffer>" + newLine +
                       "<vertexbuffer texture_coords=\"1\" texture_coord_dimensions_0=\"2\">" + newLine;
            List<uv>.Enumerator enumerator4;
            try
            {
                enumerator4 = uvs.GetEnumerator();
                while (enumerator4.MoveNext())
                {
                    var current = enumerator4.Current;
                    str5 = str5 + "<vertex>" + newLine;
                    str5 = str5 + "<texcoord v=\"" + current.v + "\" u=\"" + current.u + "\" />" + newLine;
                    str5 = str5 + "</vertex>" + newLine;
                }
            }
            finally
            {
            }
            var contents = str5 + "</vertexbuffer>" + newLine + "</geometry>" + newLine + "</submesh>" + newLine +
                           "</submeshes>" + newLine + "<submeshnames>" + "<submeshname index=\"0\" name=\"" +
                           submeshNames[0] + "\" />" + newLine + "</submeshnames>" + newLine + "</mesh>";
            File.WriteAllText(path, contents);
            Console.WriteLine("Conversion success! File was written to:" + path);
            Console.WriteLine("File contains:Verticies:" + Convert.ToString(vertices.Count) + " Faces:" +
                              Convert.ToString(faces.Count) + " UV:" +
                              Convert.ToString(uvs.Count) + " Normals:" +
                              Convert.ToString(normals.Count));
            Console.WriteLine("------");
            Console.WriteLine("Press ENTER to close this window...");
            Console.Read();
        }
    }


    public struct xyz
    {
        public string x;
        public string y;
        public string z;
    }

    public struct uv
    {
        public string u;
        public string v;
    }

    public struct face
    {
        public string i0;
        public string i1;
        public string i2;
    }
}