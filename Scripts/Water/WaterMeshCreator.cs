using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMeshCreator
{
    Mesh mesh;
    int resolution;
    Vector3 localUp;
    Vector3 axisA;
    Vector3 axisB;

    public WaterMeshCreator(Mesh _mesh, int _resolution, Vector3 _localUp)
    {
        this.mesh = _mesh;
        this.resolution = _resolution;
        this.localUp = _localUp;

        axisA = new Vector3(localUp.y, localUp.z, localUp.x);
        axisB = Vector3.Cross(localUp, axisA);
    }

    public void ConstructMesh()
    {
        Vector3[] verticies = new Vector3[resolution * resolution];
        int[] triangles = new int[(resolution - 1) * (resolution - 1) * 6];
        int triIndex = 0;

        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                int i = x + y * resolution;
                Vector2 percent = new Vector2(x, y) / (resolution - 1);
                Vector3 pointOnUnitPlane = localUp + (percent.x - 0.5f) * 2 * axisA + (percent.y - 0.5f) * 2 * axisB;
                verticies[i] = pointOnUnitPlane;

                if (x != resolution - 1 && y != resolution - 1)
                {
                    triangles[triIndex] = i;
                    triangles[triIndex + 1] = i + resolution + 1;
                    triangles[triIndex + 2] = i + resolution;

                    triangles[triIndex + 3] = i;
                    triangles[triIndex + 4] = i + 1;
                    triangles[triIndex + 5] = i + resolution + 1;

                    triIndex += 6;
                }
            }
        }

        mesh.Clear();
        mesh.vertices = verticies;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    // Sinusoidal waves
    public void UpdateWaves(float wavesLength, float period, float amplitude)
    {
        float speed = (wavesLength / period);
        float k = (2 * Mathf.PI) / (wavesLength); // Waves number
        float w = (2 * Mathf.PI) / period; // Angular frequency
        float velocity = w / k;
        Vector3[] vertices = mesh.vertices;
        
        for (int i = 0; i < vertices.Length; i++)
        {
            float ratio = (k * (vertices[i].x)) - (w * Time.time);
            float y_pos = amplitude * Mathf.Sin(ratio);

            vertices[i].y = y_pos;
        }

        mesh.vertices = vertices;
    }
}
