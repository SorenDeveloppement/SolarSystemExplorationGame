using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlantPlacementEditorWindow : EditorWindow
{
    private GameObject mesh;
    private Texture2D noiseTextureMap;
    private float density = 0.5f;
    private float minHeight = 50f;
    private float maxHeight = 58f;
    private GameObject prefab;

    [MenuItem("Tools/Wizards Code/Plant placement")]
    public static void ShowWindow()
    {
        GetWindow<PlantPlacementEditorWindow>("Plant placement");
    }

    private void OnGUI()
    {
        mesh = (GameObject) EditorGUILayout.ObjectField("Mesh", mesh, typeof(GameObject), true);

        EditorGUILayout.BeginHorizontal();
        noiseTextureMap = (Texture2D) EditorGUILayout.ObjectField("Noise Texture Map", noiseTextureMap, typeof(Texture2D), false);
        if (GUILayout.Button("Generate Noise"))
        {
            int width = (int) mesh.GetComponent<MeshFilter>().mesh.bounds.size.x;
            int height = (int) mesh.GetComponent<MeshFilter>().mesh.bounds.size.z;
            float scale = 5;
            noiseTextureMap = NoiseMap.GenerateNoiseMap(width, height, scale);
        }
        EditorGUILayout.EndHorizontal();

        density = EditorGUILayout.Slider("Density", density, 0, 1);
        minHeight = EditorGUILayout.Slider("Min Height", minHeight, 0, 256);
        maxHeight = EditorGUILayout.Slider("Max Height", maxHeight, 0, 256);

        prefab = (GameObject) EditorGUILayout.ObjectField("Object Prefab", prefab, typeof(GameObject), true);

        if (GUILayout.Button("Place Objects"))
        {
            PlaceObjects(mesh.GetComponent<MeshFilter>(), noiseTextureMap, density, minHeight, maxHeight, prefab);
        }
    }

    public static void PlaceObjects(MeshFilter mesh, Texture2D _noiseTextureMap, float _density, float _minHeight, float _maxHeight, GameObject _prefab)
    {
        Transform parent = new GameObject("PlacedObjects").transform;

        for (int x = 0; x < mesh.mesh.bounds.size.x; x++)
        {
            for (int z = 0; z < mesh.mesh.bounds.size.y; z++)
            {
                float noiseMapValue = _noiseTextureMap.GetPixel(x, z).g;

                if (noiseMapValue > 1 - _density)
                {
                    // Change the Y value by the height of the unit point on the shere at (x,z) coordinate && Object need to be // to the floor
                    Vector3 pos = new Vector3(x, 0, z);
                    pos.y = mesh.mesh.vertices[mesh.mesh.vertices.Length - 1 - (x + z)].y; // GetInterpolatedHeight(x / _terrain.terrainData.size.x, z / (float) _terrain.terrainData.size.y);

                    GameObject go = Instantiate(_prefab, pos, Quaternion.identity);
                    go.transform.SetParent(parent);
                }
            }
        }
    }
}
