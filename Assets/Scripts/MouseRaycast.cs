using UnityEngine;
using UnityEngine.InputSystem;

public class MouseRaycast : MonoBehaviour
{
    [Range(1f, 50f)] [SerializeField] private float rayMaxDistance = 20f;

    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private GameObject prefabToClone;

    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    public void OnClickToMove(InputAction.CallbackContext context)
    {
        Ray cameraRay = _mainCamera.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(cameraRay, out var hitInfo, rayMaxDistance, groundLayer.value)) return;
        
        Vector3 offset = new Vector3(0f, 1f, 0f);
        Instantiate(prefabToClone, hitInfo.point + offset, Quaternion.identity);
    }
}