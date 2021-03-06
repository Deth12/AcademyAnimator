﻿using System;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private bool _isCameraLocked;
    public bool IsCameraLocked
    {
        get => _isCameraLocked;
        set
        {
            _isCameraLocked = value;
            Cursor.visible = !value;
            Cursor.lockState = value ? CursorLockMode.Locked : CursorLockMode.None;
        }
    }
    
    [SerializeField] private float _followSpeed = 4f;
    [SerializeField] private float _mouseSpeed = 3f;

    [SerializeField] private Transform _target = default;
    [SerializeField] private Transform _pivot = default;
    [SerializeField] private Transform _cameraTransform = default;
    
    [SerializeField] private float _minAngle = -35f;
    [SerializeField] private float _maxAngle = 35f;

    private float _lookAngle;
    private float _tiltAngle;

    private void OnEnable()
    {
        GlobalEvents.OnGamePause += () =>
        {
            IsCameraLocked = false;
        };
        GlobalEvents.OnGameUnpause += () =>
        {
            IsCameraLocked = true;
        };
    }

    private void Start()
    {
        IsCameraLocked = true;

        if(_cameraTransform == null)
            _cameraTransform = Camera.main.transform;
        if(_pivot == null)
            _pivot = _cameraTransform.parent;
    }

    private void LateUpdate()
    {
        if(!IsCameraLocked)
            return;
        
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");
        
        float targetSpeed = _mouseSpeed;

        FollowTarget();
        HandleRotations(h, v, targetSpeed);
    }

    private void FollowTarget()
    {
        float speed = Time.deltaTime * _followSpeed;
        Vector3 targetPosition = Vector3.Lerp(transform.position, _target.position, speed);
        transform.position = targetPosition;
    }

    private void HandleRotations(float h, float v, float targetSpeed)
    {
        // Workaround for camera jump on first mouse move
        //if(h < -5 || v < -5)
        //    return;
        
        _tiltAngle -= v * targetSpeed;
        _tiltAngle = Mathf.Clamp(_tiltAngle, _minAngle, _maxAngle);
        _pivot.localRotation = Quaternion.Euler(_tiltAngle, 0, 0);

        _lookAngle += h * targetSpeed;
        transform.rotation = Quaternion.Euler(0, _lookAngle, 0);
    }

    private void OnDisable()
    {
        GlobalEvents.OnGamePause -= () =>
        {
            IsCameraLocked = false;
        };
        GlobalEvents.OnGameUnpause -= () =>
        {
            IsCameraLocked = true;
        };
    }
}
