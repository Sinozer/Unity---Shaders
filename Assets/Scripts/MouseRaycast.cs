using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Exemple où on spawn un clone d'un prefab.
/// A modifier selon votre gameplay.
/// </summary>
public class MouseRaycast : MonoBehaviour
{
    [Range(1f, 50f)] [SerializeField] private float rayMaxDistance = 20f;

    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private GameObject prefabToClone;

    private Camera _mainCamera;

    void Start()
    {
        _mainCamera = Camera.main;
    }

    public void OnClickToMove(InputAction.CallbackContext context)
    {
        Ray cameraRay = _mainCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;
        if (Physics.Raycast(cameraRay, out hitInfo, rayMaxDistance, groundLayer.value))
        {
            // To do : Offset à certainement modifier.
            Vector3 offset = new Vector3(0f, 1f, 0f);
            Instantiate(prefabToClone, hitInfo.point + offset, Quaternion.identity);
        }
    }
}