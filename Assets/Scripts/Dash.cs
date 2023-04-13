using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("References")]
    public Transform orientation;
    public Transform playerCam;
    private Rigidbody rb;

   
    [Header("Dashing")]
    public float dashForce;
    public float dashUpwardForce;
    public float maxDashYSpeed;
    public float dashDuration;

    [Header("CameraEffects")]

    public float dashFov;

    [Header("Settings")]
    public bool useCameraForward = true;
    public bool allowAllDirections = true;
    public bool disableGravity = false;
    public bool resetVel = true;

    [Header("Cooldown")]
    public float dashCd;
    private float dashCdTimer;

    [Header("Input")]
    public KeyCode dashKey = KeyCode.LeftControl;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

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