using Player;
using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [Header("Input")]
    public KeyCode dashKey = KeyCode.LeftControl;

    [Header("Dash Characteristic")]
    public float dashSpeed = 15f;

    private void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(dashKey))
            Dashing();
        else
            GetComponent<PlayerMovement>()._playerSpeed = 2f;
            GetComponent<PlayerMovement>()._sprintSpeed = 5.335f;

    }

    private void Dashing()
    {
        GetComponent<PlayerMovement>()._playerSpeed = dashSpeed;
        GetComponent<PlayerMovement>()._sprintSpeed = dashSpeed;
    }
}