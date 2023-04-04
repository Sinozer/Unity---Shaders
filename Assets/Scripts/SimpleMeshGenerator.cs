using UnityEngine;

public class SimpleMeshGenerator : MonoBehaviour {
    public Material meshMaterial;
    [SerializeField] private Vector3 point1;
    [SerializeField] private Vector3 point2;
    [SerializeField] private Vector3 point3;

    [SerializeField] private Color color1;
    [SerializeField] private Color color2;
    [SerializeField] private Color color3;

    void Start() {
        MakeTriangle();
    }

    void MakeTriangle() {
        //Vertices array of type Vector3
        Vector3[] vertices = { point1, point2, point3 };

        //Indices array of type int
        int[] index = { 0, 1, 2 };

        Color[] colors = {
            color1,
            color2,
            color3
        };

        //Call BuildMesh function
        BuildMesh("Triangle",vertices,index, null, colors);
    }

    protected void BuildMesh(string gameObjectName, Vector3[] vertices, int[] indices, Vector2[] uvs = null, Color[] colors = null) {
        // Search in the scene if there is a GameObject called "gameObjectName". If yes, we destroy it.
        GameObject oldOne = GameObject.Find(gameObjectName);
        if (oldOne != null)
            DestroyImmediate(oldOne);

        // Create a GameObject
        GameObject primitive = new GameObject(gameObjectName);

        // Add the components...
        MeshRenderer meshRenderer = primitive.AddComponent<MeshRenderer>();
        MeshFilter meshFilter = primitive.AddComponent<MeshFilter>();

        // ... and set the mesh buffers. 
        meshFilter.mesh.vertices = vertices;
        meshFilter.mesh.triangles = indices;
        meshFilter.mesh.uv = uvs;
        meshFilter.mesh.colors = colors;

        // Apply the material.
        meshRenderer.material = meshMaterial != null
            ? meshMaterial
            : new Material(Shader.Find("Universal Render Pipeline/Unlit"));
    }
}