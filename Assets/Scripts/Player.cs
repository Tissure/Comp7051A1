using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    InputActionsPlayer inputs;
    InputAction movement;
    InputAction cameraMovement;
    InputAction noclip;
    InputAction shoot;
    InputAction pause;

    [SerializeField]
    private float movementSpeed = 10.0f;
    [SerializeField]
    private float mouseSensitivity = 15.0f;
    [SerializeField]
    private float shootForce = 400f;

    [SerializeField]
    private new Camera camera;
    private Rigidbody rb;
    private new Collider collider;
    float xrotation = 0;
    float yrotation = 0;

    [SerializeField]
    private GameObject ProjectilePrefab;

    private Transform movementTransform;
    void Awake()
    {
        inputs = new InputActionsPlayer();
        movement = inputs.Player.Movement;
        cameraMovement = inputs.Player.Camera;
        noclip = inputs.Player.NoClip;
        shoot = inputs.Player.Shoot;
        pause = inputs.Player.PauseGame;
        Cursor.lockState = CursorLockMode.Locked;

        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        movementTransform = GetComponent<Transform>();

        noclip.performed += context => {
            bool enabled = GetComponent<Collider>().enabled;
            movementTransform = (!enabled) ? transform : camera.transform;
            collider.enabled = !enabled;
            rb.useGravity = !enabled;
            rb.velocity = Vector3.zero;
        };

        shoot.performed += context => {
            Debug.Log("Shootymcshootshoot");
            GameObject projectile = Instantiate(ProjectilePrefab, camera.transform.position, camera.transform.rotation);
            projectile.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, shootForce));
        };

        pause.performed += context => {
            MazeGameManager.Instance.TogglePause();
        };
    }

    private void OnEnable()
    {
        movement.Enable();
        cameraMovement.Enable();
        noclip.Enable();
        shoot.Enable();
        pause.Enable();
    }

    private void OnDisable()
    {
        movement.Disable();
        cameraMovement.Disable();
        noclip.Disable();
        shoot.Disable();
        pause.Disable();
    }

    private void Update()
    {
        RotateCamera();

    }

    private void FixedUpdate()
    {
        Vector2 v2 = movement.ReadValue<Vector2>();
        rb.MovePosition(transform.position + (movementTransform.forward * v2.y * movementSpeed * Time.deltaTime)
            + (movementTransform.right * v2.x * movementSpeed * Time.deltaTime));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("AI"))
        {
            killPlayer();
        }
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

    void killPlayer()
    {
        Debug.Log("Dies");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
