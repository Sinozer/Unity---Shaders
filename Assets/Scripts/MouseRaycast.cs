using UnityEngine;

/// <summary>
/// Exemple où on spawn un clone d'un prefab.
/// A modifier selon votre gameplay.
/// </summary>
public class MouseRaycast : MonoBehaviour
{
    [Range(1f, 50f)]
    [SerializeField] private float _rayMaxDistance = 20f;

    [SerializeField] LayerMask _groundLayer;

    [SerializeField] GameObject PrefabToClone;

    private Camera _mainCamera;

    void Start()
    {
        _mainCamera = Camera.main;    
    }
   
    void Update()
    {
        if (!Input.GetMouseButtonDown(0))
            return;
        
        Ray cameraRay = _mainCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;
        if (Physics.Raycast(cameraRay, out hitInfo, _rayMaxDistance, _groundLayer.value))
        {
            // To do : Offset à certainement modifier.
            Vector3 offset = new Vector3(0f, 1f, 0f);
            Instantiate(PrefabToClone, hitInfo.point + offset, Quaternion.identity);
        }
    }
}
