using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightMapMesh : MonoBehaviour
{
    public float heightMin = 0.5f;
    public float heightMax = 1f;

    public Mesh templateMesh;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHeightMap(Texture2D heightMap, Texture2D texture = null)
    {
        Vector3[] vertices = templateMesh.vertices;
        Vector2[] uvs = templateMesh.uv;

        for (int index = 0; index < vertices.Length; index++)
        {
            Vector2 uv = uvs[index];
            Color texel = heightMap.GetPixelBilinear(uv.x, uv.y);
            vertices[index] *= heightMin + texel.grayscale * (heightMax - heightMin);
        }

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.triangles = templateMesh.triangles;
        mesh.RecalculateNormals();

        GetComponent<MeshFilter>().mesh = mesh;
        
        if (texture)
        {
            GetComponent<MeshRenderer>().material.mainTexture = texture;
        }
    }
}
