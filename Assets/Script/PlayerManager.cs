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
    public float jumpForce;
    public float rayDis;
    public float armReach;
    private bool mouseIsLocked = false;
    private bool isGrounded;

    public GameObject cameraOb;
    public GameObject rayPoint;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        mouseIsLocked = true;
    }

    private void FixedUpdate()
    {
        //Ground raycast
        RaycastHit hitData;
        var ray = Physics.Raycast(rayPoint.transform.position, Vector3.down, out hitData, rayDis);

        if (hitData.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        //Cursor raycast
        RaycastHit hit;
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(mouseRay, out hit, armReach))
        {
            print(hit.collider.name);
        }
    }

    private void Update()
    {
        

        if (mouseIsLocked)
        {
            Cursor.visible = false;
        }

        else
        {
            Cursor.visible = true;
        }

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
    }

    public void Jump(InputAction.CallbackContext jumpVal)
    {
        if (isGrounded)
        {
            rb.AddForce(transform.up * jumpForce);
        }
    }
}
