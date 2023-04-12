using UnityEngine;

[ExecuteInEditMode]
public class GameObjectPositionToShaders : MonoBehaviour
{
    string _variableName;

    void Start()
    {
        _variableName = "_WorldSpace" + gameObject.name + "Pos";
    }

    void Update()
    {
        Shader.SetGlobalVector(_variableName, transform.position);
    }
}