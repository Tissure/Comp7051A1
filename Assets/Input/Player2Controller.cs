using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player2Controller : MonoBehaviour
{
    // Create Private Internal References
    private InputActionsP2 inputActions;
    private InputAction movement;

    [SerializeField]
    public float movementSpeed = 5.0f;

    private void Awake()
    {
        // Create new InputActions
        inputActions = new InputActionsP2();

        movement = inputActions.Player2.Movement;
    }

    // Called when Script is Enabled
    private void OnEnable()
    {
        movement.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        // Extract 2D Input Data
        Vector2 v2 = movement.ReadValue<Vector2>();

        transform.Translate(new Vector3(v2.y, v2.x, 0) * movementSpeed * Time.deltaTime);

    }
}
