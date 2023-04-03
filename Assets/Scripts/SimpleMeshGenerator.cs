using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMeshGenerator : MonoBehaviour
{
    public Material _MeshMaterial;

    void Start()
    {
        //MakeTriangle();
        
    }

    void MakeTriangle()
    {
		// TO DO: Vertices array of type Vector3
		// TO DO: Indices array of type int
        
		// TO DO: appeller la fonction BuildMesh avec les bons paramètres
    }

    protected void BuildMesh(string gameObjectName, Vector3[] vertices, int[] indices, Vector2[] uvs = null)
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
      
        // ... and set the mesh buffers. 
        meshFilter.mesh.vertices = vertices;
        meshFilter.mesh.triangles = indices;
        meshFilter.mesh.uv = uvs;

        // Apply the material.
        meshRenderer.material = _MeshMaterial != null ? _MeshMaterial : new Material(Shader.Find("Universal Render Pipeline/Unlit"));
    }
}
