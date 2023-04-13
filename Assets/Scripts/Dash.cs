using Player;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [Header("Input")] public KeyCode dashKey = KeyCode.LeftControl;

    [Header("Dash Characteristic")] public float dashSpeed = 15f;

    private PlayerMovement _playerMovement;

    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(dashKey)) Dashing();
        else _playerMovement._playerSpeed = 2f;

        _playerMovement._sprintSpeed = 5.335f;
    }

    private void Dashing()
    {
        _playerMovement._playerSpeed = dashSpeed;
        _playerMovement._sprintSpeed = dashSpeed;
    }
}