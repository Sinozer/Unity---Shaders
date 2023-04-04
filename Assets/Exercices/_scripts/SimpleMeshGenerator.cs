using UnityEngine;

public class SimpleMeshGenerator : MonoBehaviour
{
    [SerializeField] private Material _MeshMaterial;
    [SerializeField] private string _MeshName;

    private void Start()
    {
        MakeTriangle();
    }

    private void MakeTriangle()
    {
        Vector3[] vertices =
        {
            new(-1, 0, 0),
            new(0, 2, 0),
            new(1, 0, 0)
        };

        // Indices that will determine in which order the vertices will be drawn.
        int[] indices =
        {
            0, 1, 2
        };

        Color[] colors =
        {
            Color.white, Color.red, Color.blue
        };

        Vector2[] uvs =
        {
            new (0.6f,0),
            new (0.9f,0.5f),
            new (0.6f,0.4f)
        };

        BuildMesh(_MeshName, vertices, indices, uvs, colors);
    }

    protected void BuildMesh(string gameObjectName, Vector3[] vertices, int[] indices, Vector2[] uvs = null, Color[] colors = null)
    {
        // Search in the scene if there is a GameObject called "gameObjectName". If yes, we destroy it.
        GameObject oldOne = GameObject.Find(gameObjectName);
        if (oldOne != null)
            DestroyImmediate(oldOne);

        // Create a GameObject
        GameObject primitive = new GameObject(gameObjectName);

        // Add the components...
        MeshRenderer meshRenderer = primitive.AddComponent<MeshRenderer>();
        MeshFilter meshFilter = primitive.AddComponent<MeshFilter>();
        Mesh mesh = meshFilter.mesh;

        // ... and set the mesh buffers. 
        mesh.vertices = vertices;
        mesh.triangles = indices;
        mesh.uv = uvs;
        mesh.colors = colors;

        // Apply the material.
        meshRenderer.material = _MeshMaterial != null
            ? _MeshMaterial
            : new Material(Shader.Find("Universal Render Pipeline/Unlit"));
    }
}