using UnityEngine;

/// <summary>
/// Demo, sert en tant que référence. 
/// A modifier selon vos besoins.
/// </summary>
[RequireComponent(typeof(MeshRenderer))]
public class UpdateShaderData : MonoBehaviour
{
    [SerializeField] AnimationCurve _curve;
    Material _material;

    void Start()
    {       
        _material = GetComponent<MeshRenderer>().material;
    }
   
    void Update()
    {
        // Tous les shaders peuvent avoir accès à cette variable.
        Shader.SetGlobalVector("_WorldSpaceSpherePos", transform.position);


        // Variables seulement updatées pour ce material.
        float value = _curve.Evaluate(Time.time);  // Evaluate(time) because this curve is in loop mode.
        _material.SetFloat("_LerpAlpha", value);

        if (Input.GetKeyDown(KeyCode.H))
            _material.SetColor("_SecondColor", Color.green);
    }
}
