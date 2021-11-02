using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    private Vector2 move;
    private Vector2 mousePos;
    private float xRotation;
    public float speed;
    public float mouseSensivity;

    public GameObject cameraOb;

    private void Update()
    {
        //Player movement
        transform.Translate(new Vector3(move.x, 0, move.y) * speed * Time.deltaTime);

        //Camera rotation
        transform.Rotate(new Vector3(0, mousePos.x * mouseSensivity, 0));

        xRotation -= mousePos.y;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cameraOb.transform.localRotation = Quaternion.Euler(xRotation * mouseSensivity, 0f, 0f);
    }

    public void MovePlayer(InputAction.CallbackContext moveVal)
    {
        move = moveVal.ReadValue<Vector2>();
    }

    public void RotateCamera(InputAction.CallbackContext mouseVal)
    {
        mousePos = mouseVal.ReadValue<Vector2>();
        mousePos.Normalize();
        mousePos.y = Mathf.Clamp(mousePos.y, -90f, 90f);
    }
}
