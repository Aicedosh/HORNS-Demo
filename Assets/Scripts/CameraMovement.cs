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

    public Transform Followed;
    private Vector3 oldPos;

    private void Start()
    {
        if(Followed != null)
        {
            oldPos = Followed.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Followed != null)
        {
            transform.Translate(Followed.position - oldPos, Space.World);
            oldPos = Followed.position;
        }

        float a = (ZeroLevel-transform.position.y) / transform.forward.y;
        Vector3 pivot = new Vector3(transform.position.x + a * transform.forward.x, ZeroLevel, transform.position.z + a * transform.forward.z);

        Vector3 forward = transform.worldToLocalMatrix.MultiplyVector(transform.forward);

        transform.RotateAround(pivot, transform.right, -Input.mouseScrollDelta.y * AngularSpeed * Time.deltaTime / Time.timeScale);
        transform.Translate(forward * Input.mouseScrollDelta.y * TowardsSpeed * Time.deltaTime / Time.timeScale);

        Vector3 forward2d = new Vector3(transform.forward.x, 0, transform.forward.z).normalized;
        Vector3 right2d = new Vector3(transform.right.x, 0, transform.right.z).normalized;

        if(Input.GetKey(KeyCode.W))
        {
            transform.Translate(forward2d * MoveSpeed * Time.deltaTime / Time.timeScale, Space.World);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(-forward2d * MoveSpeed * Time.deltaTime / Time.timeScale, Space.World);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(right2d * MoveSpeed * Time.deltaTime / Time.timeScale, Space.World);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-right2d * MoveSpeed * Time.deltaTime / Time.timeScale, Space.World);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            transform.RotateAround(pivot, Vector3.up, RotateSpeed * Time.deltaTime / Time.timeScale);
        }

        if (Input.GetKey(KeyCode.E))
        {
            transform.RotateAround(pivot, Vector3.up, -RotateSpeed * Time.deltaTime / Time.timeScale);
        }

        if (Input.GetKey(KeyCode.Equals) || Input.GetKey(KeyCode.KeypadPlus))
        {
            transform.RotateAround(pivot, transform.right, -AngularSpeed * Time.deltaTime / Time.timeScale);
            transform.Translate(forward * TowardsSpeed * Time.deltaTime / Time.timeScale);
        }

        if (Input.GetKey(KeyCode.Minus) || Input.GetKey(KeyCode.KeypadMinus))
        {
            transform.RotateAround(pivot, transform.right, AngularSpeed * Time.deltaTime / Time.timeScale);
            transform.Translate(forward * -TowardsSpeed * Time.deltaTime / Time.timeScale);
        }
    }
}
