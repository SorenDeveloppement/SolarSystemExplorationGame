using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ocean : MonoBehaviour
{
    public Material material;
    [Range(2, 512)] public int resolution;
    [SerializeField, HideInInspector] MeshFilter meshFilter = new MeshFilter();
    WaterMeshCreator waterMeshCreator;
    public bool autoUpdate = true;
    public float wavesLength = 0.7f;
    public float amplitude = 0.03f;
    public float period = 1f;

    void Initialize()
    {

        if (meshFilter == null)
        {
            GameObject meshObj = new GameObject("mesh");
            meshObj.transform.parent = transform;

            meshObj.AddComponent<MeshRenderer>();
            meshFilter = meshObj.AddComponent<MeshFilter>();
            meshFilter.sharedMesh = new Mesh();
        
            meshObj.AddComponent<MeshCollider>();

            meshFilter.GetComponent<MeshRenderer>().sharedMaterial = material;
        }

        waterMeshCreator = new WaterMeshCreator(meshFilter.sharedMesh, resolution, Vector3.up);
        meshFilter.gameObject.SetActive(true);
    }

    void OnValidate()
    {
        if (autoUpdate)
        {
            GenerateOcean();
            waterMeshCreator.UpdateWaves(wavesLength, period, amplitude);
        }
    }

    void GenerateOcean()
    {
        Initialize();
        GenerateMesh();
    }

    void GenerateMesh()
    {
        waterMeshCreator.ConstructMesh();
    }

    // Waves generation //

    private void Update() {
        waterMeshCreator.UpdateWaves(wavesLength, period, amplitude);
    }
}
