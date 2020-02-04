using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMovement : MonoBehaviour
{
    public float MoveSpeed;
    public float VerticalSpeed;
    public float ZoomSpeed;
    public float ZoomMouseMult;
    public float RotateSpeed;
    public float RotateMouseMult;

    public float ZeroLevel;
    public int Boundary;

    private bool freeLooking;

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ToggleFreeLook(true);
        }

        if (Input.GetMouseButtonUp(1))
        {
            ToggleFreeLook(false);
        }

        Zoom();

        Move();

        MoveVertical();

        Rotate();
    }

    private void ToggleFreeLook(bool value)
    {
        freeLooking = value;
        Cursor.visible = !value;
        Cursor.lockState = value ? CursorLockMode.Locked : CursorLockMode.None;
    }

    private void Zoom()
    {
        float zoomFactor = 0;
        if (EventSystem.current.IsPointerOverGameObject() == false && Input.mouseScrollDelta.y != 0)
        {
            zoomFactor += Input.mouseScrollDelta.y * ZoomMouseMult;
        }

        if (Input.GetKey(KeyCode.Equals) || Input.GetKey(KeyCode.KeypadPlus))
        {
            zoomFactor += 1;
        }

        if (Input.GetKey(KeyCode.Minus) || Input.GetKey(KeyCode.KeypadMinus))
        {
            zoomFactor -= 1;
        }
        
        Vector3 newPos = zoomFactor * transform.forward * ZoomSpeed * Time.unscaledDeltaTime + transform.position;
        newPos.y = Mathf.Max(newPos.y, ZeroLevel);
        transform.position = newPos;
    }

    private void Move()
    {
        float forwardFactor = 0, rightFactor = 0;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) 
            || (EventSystem.current.IsPointerOverGameObject() == false && Input.mousePosition.y > Screen.height - Boundary))
        {
            forwardFactor += 1;
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) 
            || (EventSystem.current.IsPointerOverGameObject() == false && Input.mousePosition.y < Boundary))
        {
            forwardFactor -= 1;
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) 
            || (EventSystem.current.IsPointerOverGameObject() == false && Input.mousePosition.x > Screen.width - Boundary))
        {
            rightFactor += 1;
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) 
            || (EventSystem.current.IsPointerOverGameObject() == false && Input.mousePosition.x < Boundary))
        {
            rightFactor -= 1;
        }

        Vector3 forward2d = new Vector3(transform.forward.x, 0, transform.forward.z).normalized;
        Vector3 right2d = new Vector3(transform.right.x, 0, transform.right.z).normalized;
        transform.Translate(forwardFactor * forward2d * MoveSpeed * Time.unscaledDeltaTime, Space.World);
        transform.Translate(rightFactor * right2d * MoveSpeed * Time.unscaledDeltaTime, Space.World);
    }

    private void MoveVertical()
    {
        float upFactor = 0;
        if (Input.GetKey(KeyCode.PageUp))
        {
            upFactor += 1;
        }

        if (Input.GetKey(KeyCode.PageDown))
        {
            upFactor -= 1;
        }

        Vector3 newPos = upFactor * Vector3.up * VerticalSpeed * Time.unscaledDeltaTime + transform.position;
        newPos.y = Mathf.Max(newPos.y, ZeroLevel);
        transform.position = newPos;
    }

    private void Rotate()
    {
        float xFactor = 0, yFactor = 0;
        if (freeLooking)
        {
            xFactor -= Input.GetAxis("Mouse Y") * RotateMouseMult;
            yFactor += Input.GetAxis("Mouse X") * RotateMouseMult;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            yFactor -= 1;
        }

        if (Input.GetKey(KeyCode.E))
        {
            yFactor += 1;
        }
        
        float newx = transform.localEulerAngles.x + xFactor * RotateSpeed * Time.unscaledDeltaTime;
        if (newx < 0)
        {
            newx += 360;
        }
        if (newx < 180)
        {
            newx = Mathf.Clamp(newx, 0, 90);
        }
        else
        {
            newx = Mathf.Clamp(newx, 270, 360);
        }
        float newy = transform.localEulerAngles.y + yFactor * RotateSpeed * Time.unscaledDeltaTime;
        transform.localEulerAngles = new Vector3(newx, newy, 0f);
    }
}
