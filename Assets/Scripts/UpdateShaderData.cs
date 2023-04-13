using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class UpdateShaderData : MonoBehaviour
{
    [SerializeField] private AnimationCurve _curve;
    private Material _material;
    
    private static readonly int WorldSpaceSpherePos = Shader.PropertyToID("_WorldSpaceSpherePos");
    private static readonly int LerpAlpha = Shader.PropertyToID("_LerpAlpha");
    private static readonly int SecondColor = Shader.PropertyToID("_SecondColor");

    private void Start() => _material = GetComponent<MeshRenderer>().material;

    private void Update()
    {
        Shader.SetGlobalVector(WorldSpaceSpherePos, transform.position);
        
        float value = _curve.Evaluate(Time.time);
        _material.SetFloat(LerpAlpha, value);

        if (Input.GetKeyDown(KeyCode.H)) _material.SetColor(SecondColor, Color.green);
    }
}