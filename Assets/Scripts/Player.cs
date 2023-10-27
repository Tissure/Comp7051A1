using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    InputActionsPlayer inputs;
    InputAction movement;

    [SerializeField]
    public float movementSpeed = 5.0f;
    [SerializeField]
    public float mouseSensitivity = 10.0f;

    [SerializeField]
    private Camera camera;
    float xrotation = 0;
    float yrotation = 0;

    void Awake()
    {
        inputs = new InputActionsPlayer();
        movement = inputs.Player.Movement;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        movement.Enable();
    }

    private void OnDisable()
    {
        movement.Disable();
    }

    private void Update()
    {

        RotateCamera();
        Vector2 v2 = movement.ReadValue<Vector2>();
        transform.Translate(new Vector3(v2.x, 0, v2.y) * movementSpeed * Time.deltaTime);
    }

    void RotateCamera()
    {
        Vector2 mousemovement = Mouse.current.delta.ReadValue();
        xrotation -= mousemovement.y * Time.deltaTime * mouseSensitivity;
        xrotation = Mathf.Clamp(xrotation, -90, 90);
        yrotation += mousemovement.x * Time.deltaTime * mouseSensitivity;
        camera.transform.rotation = Quaternion.Euler(xrotation, yrotation, 0);
        //Rotating the player
        transform.localRotation = Quaternion.Euler(0, yrotation, 0);
    }
}
