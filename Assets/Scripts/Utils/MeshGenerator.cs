using System.Drawing;
using TMPro;
using UnityEngine;

namespace Utils
{
    public static class MeshGenerator
    {
        public static Mesh CreatePlane(int xSize, int zSize, float distanceBetweenVerticles)
        {
            var plane = new Mesh();

            var vertices = new Vector3[(xSize + 1) * (zSize + 1)];
            var uvs = new Vector2[vertices.Length];
            var tangents = new Vector4[vertices.Length];

            Vector4 tangent = new Vector4(1f, 0f, 0f, -1f);

            for (int i = 0, z = 0; z <= zSize; z++)
            {
                for (int x = 0; x <= xSize; x++, i++)
                {
                    vertices[i] = new Vector3(x,0 ,z) * distanceBetweenVerticles;
                    uvs[i] = new Vector2((float)x / xSize, (float)z / zSize);
                    tangents[i] = tangent;
                }
            }

            int[] triangles = new int[xSize * zSize * 6];
            int ti = 0, vi = 0;
            for (int y = 0; y < zSize; y++, vi++)
            {
                for (int x = 0; x < xSize; x++, ti += 6, vi++)
                {
                    triangles[ti] = vi;
                    triangles[ti + 1] = triangles[ti + 4] = vi + xSize + 1;
                    triangles[ti + 2] = triangles[ti + 3] = vi + 1;
                    triangles[ti + 5] = vi + xSize + 2;
                }
            }

            plane.vertices = vertices;
            plane.triangles = triangles;
            plane.tangents = tangents;
            plane.RecalculateNormals();

            return plane;
        }

    }
}
