using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    InputActionsPlayer inputs;
    InputAction movement;
    InputAction cameraMovement;
    InputAction noclip;

    [SerializeField]
    private float movementSpeed = 5.0f;
    [SerializeField]
    private float mouseSensitivity = 10.0f;

    [SerializeField]
    private new Camera camera;
    private Rigidbody rb;
    private Collider collider;
    float xrotation = 0;
    float yrotation = 0;

    void Awake()
    {
        inputs = new InputActionsPlayer();
        movement = inputs.Player.Movement;
        cameraMovement = inputs.Player.Camera;
        noclip = inputs.Player.NoClip;
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        noclip.performed += context => {
            bool enabled = GetComponent<Collider>().enabled;
            collider.enabled = !enabled;
            rb.useGravity = !enabled;
            rb.velocity = Vector3.zero;
        };
    }

    private void OnEnable()
    {
        movement.Enable();
        cameraMovement.Enable();
        noclip.Enable();
    }

    private void OnDisable()
    {
        movement.Disable();
        cameraMovement.Disable();
        noclip.Disable();
    }

    private void Update()
    {

        RotateCamera();
        Vector2 v2 = movement.ReadValue<Vector2>();
        transform.Translate(movementSpeed * Time.deltaTime * new Vector3(v2.x, 0, v2.y));
    }

    void RotateCamera()
    {
        Vector2 mousemovement = cameraMovement.ReadValue<Vector2>();
        xrotation -= mousemovement.y * Time.deltaTime * mouseSensitivity;
        xrotation = Mathf.Clamp(xrotation, -90, 90);
        yrotation += mousemovement.x * Time.deltaTime * mouseSensitivity;
        camera.transform.rotation = Quaternion.Euler(xrotation, yrotation, 0);
        //Rotating the player
        transform.localRotation = Quaternion.Euler(0, yrotation, 0);
    }

 
}
