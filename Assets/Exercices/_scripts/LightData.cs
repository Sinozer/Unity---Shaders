using UnityEngine;


/// <summary>
/// Only works with Directional Light
/// </summary>
[ExecuteInEditMode]
[RequireComponent(typeof(Light))]
public class LightData : MonoBehaviour
{
    Light _light;

    private void Start()
    {
        _light = GetComponent<Light>();
    }

    void Update()
    {
        Shader.SetGlobalColor("_LightColor", _light.color);
        Shader.SetGlobalVector("_WorldSpaceLightDir", transform.forward);
    }
}
