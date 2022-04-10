using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_FreeCamera : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _Speed = 5;
    [SerializeField] private float _ScrollSpeed = 20;
    [SerializeField] private float _SprintSpeed = 8;
    [SerializeField] private bool _InverseScroll = false;
    [SerializeField] private bool _CheckCorner = true;

    private float _CurrentSpeed;

    void Update()
    {
        //variables
        float xas = 0;
        float zas = 0;
        float yas = Input.mouseScrollDelta.y;

        //Mouse
        if (_CheckCorner)
        {
            if (Input.mousePosition.x < 10)
                xas = -1;
            if (Input.mousePosition.x > Screen.width - 10)
                xas = 1;
            if (Input.mousePosition.y < 10)
                zas = -1;
            if (Input.mousePosition.y > Screen.height - 10)
                zas = 1;
        }

        //WSAD
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            xas = Input.GetAxis("Horizontal");
            zas = Input.GetAxis("Vertical");
        }

        if (Input.GetKey(KeyCode.LeftShift))
            _CurrentSpeed = _SprintSpeed;
        else
            _CurrentSpeed = _Speed;

        //Zoom/Scroll
        if (_InverseScroll)
            yas *= _ScrollSpeed;
        else
            yas *= -_ScrollSpeed;

        //Apply Movement
        transform.Translate(new Vector3(xas * _CurrentSpeed, yas, zas * _CurrentSpeed) * Time.deltaTime);
    }
}
