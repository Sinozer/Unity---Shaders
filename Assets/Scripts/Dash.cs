using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [Header("Input")]
    public KeyCode dashKey = KeyCode.LeftControl;

    private void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(dashKey))
            Dashing();
        else
            GetComponent<ThirdPersonController>().MoveSpeed = 2;

    }

    private void Dashing()
    {
        GetComponent<ThirdPersonController>().MoveSpeed = 100;
    }
}