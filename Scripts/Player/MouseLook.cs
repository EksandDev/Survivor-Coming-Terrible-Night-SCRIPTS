using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] float _turnSpeed = 90f;
    [SerializeField] float _headUpperAngleLimit = 85f;
    [SerializeField] float _headLowerAngleLimit = -80f;

    float _yaw = 0f;
    float _pitch = 0f;
    Quaternion _bodyStartOrientation;
    Quaternion _headStartOrientation;
    Transform _head;

    private void Start()
    {
        _head = GetComponentInChildren<Camera>().transform;
        _bodyStartOrientation = transform.localRotation;
        _headStartOrientation = _head.transform.localRotation;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }

    private void FixedUpdate()
    {
        HeadRotation();
    }

    private void HeadRotation()
    {
        var horizontal = Input.GetAxis("Mouse X") * Time.deltaTime * _turnSpeed;
        var vertical = -Input.GetAxis("Mouse Y") * Time.deltaTime * _turnSpeed;
        _yaw += horizontal;
        _pitch += vertical;
        _pitch = Mathf.Clamp(_pitch, _headLowerAngleLimit, _headUpperAngleLimit);
        var bodyRotation = Quaternion.AngleAxis(_yaw, Vector3.up);
        var headRotation = Quaternion.AngleAxis(_pitch, Vector3.right);
        transform.localRotation = bodyRotation * _bodyStartOrientation;
        _head.localRotation = headRotation * _headStartOrientation;
    }
}
