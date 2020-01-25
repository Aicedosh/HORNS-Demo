using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float TowardsSpeed;
    public float AngularSpeed;
    public float MoveSpeed;
    public float RotateSpeed;

    public float ZeroLevel;

    private bool freeLooking;

    private int boundary = 10;

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
        if (Input.mouseScrollDelta.y != 0)
        {
            zoomFactor += Input.mouseScrollDelta.y;
        }

        if (Input.GetKey(KeyCode.Equals) || Input.GetKey(KeyCode.KeypadPlus))
        {
            zoomFactor += 1;
        }

        if (Input.GetKey(KeyCode.Minus) || Input.GetKey(KeyCode.KeypadMinus))
        {
            zoomFactor -= 1;
        }
        
        Vector3 newPos = zoomFactor * transform.forward * TowardsSpeed * Time.deltaTime / Time.timeScale + transform.position;
        newPos.y = Mathf.Max(newPos.y, ZeroLevel);
        transform.position = newPos;
    }

    private void Move()
    {
        float forwardFactor = 0, rightFactor = 0;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.mousePosition.y > Screen.height - boundary)
        {
            forwardFactor += 1;
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) || Input.mousePosition.y < boundary)
        {
            forwardFactor -= 1;
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || Input.mousePosition.x > Screen.width - boundary)
        {
            rightFactor += 1;
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || Input.mousePosition.x < boundary)
        {
            rightFactor -= 1;
        }

        Vector3 forward2d = new Vector3(transform.forward.x, 0, transform.forward.z).normalized;
        Vector3 right2d = new Vector3(transform.right.x, 0, transform.right.z).normalized;
        transform.Translate(forwardFactor * forward2d * MoveSpeed * Time.deltaTime / Time.timeScale, Space.World);
        transform.Translate(rightFactor * right2d * MoveSpeed * Time.deltaTime / Time.timeScale, Space.World);
    }

    private void Rotate()
    {
        float xfactor = 0, yfactor = 0;
        if (freeLooking)
        {
            xfactor -= Input.GetAxis("Mouse Y");
            yfactor += Input.GetAxis("Mouse X");
        }

        if (Input.GetKey(KeyCode.Q))
        {
            yfactor -= 1;
        }

        if (Input.GetKey(KeyCode.E))
        {
            yfactor += 1;
        }
        
        float newx = transform.localEulerAngles.x + xfactor * RotateSpeed * Time.deltaTime / Time.timeScale;
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
        float newy = transform.localEulerAngles.y + yfactor * RotateSpeed * Time.deltaTime / Time.timeScale;
        transform.localEulerAngles = new Vector3(newx, newy, 0f);
    }
}
