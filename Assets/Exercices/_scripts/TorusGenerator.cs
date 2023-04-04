using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TorusGenerator : SimpleMeshGenerator  
{
    [Range(3, 30)]
    public int TorusSides = 3;
    [Range(1f, 3f)]
    public float TorusRadius = 2f;
    [Range(0.2f, 1f)]
    public float TorusHeight = 0.5f;

    public bool RecomputeTorus = false;

    private void Start()
    {
        MakeTorus();
    }

    private void Update()
    {
        if (!RecomputeTorus) return;
        
        RecomputeTorus = false;
        MakeTorus();
    }

    private void MakeTorus()
    {
        Vector3[] vertices = new Vector3[TorusSides * 2];
        int[] indices = new int[TorusSides * 6];
        
        for (int i = 0; i < TorusSides; i++)
        {
            float angle = i * 2 * Mathf.PI / TorusSides;
            float x = Mathf.Cos(angle) * TorusRadius;
            float y = Mathf.Sin(angle) * TorusRadius;
            float z = TorusHeight;
            vertices[i] = new Vector3(x, y, z);
            vertices[i + TorusSides] = new Vector3(x, y, -z);
            
            indices[i * 6] = i;
            indices[i * 6 + 1] = (i + 1) % TorusSides;
            indices[i * 6 + 2] = i + TorusSides;
            indices[i * 6 + 3] = (i + 1) % TorusSides;
            indices[i * 6 + 4] = (i + 1) % TorusSides + TorusSides;
            indices[i * 6 + 5] = i + TorusSides;
        }

        BuildMesh("Torus", vertices, indices);
    }
}
