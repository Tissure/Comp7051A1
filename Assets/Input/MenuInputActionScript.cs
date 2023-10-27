using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuInputActionScript : MonoBehaviour
{
    private MenuInputActions inputActions;
    private InputAction keymaps;

    private void Awake()
    {
        inputActions = new MenuInputActions();
        keymaps = inputActions.PlayerMenu.Menu;
    }

    private void OnEnable()
    {
        keymaps.Enable();

        inputActions.PlayerMenu.Menu.performed += DoMenu;
        inputActions.PlayerMenu.Menu.Enable();

    }

    private void OnDisable()
    {
        keymaps.Disable();

        inputActions.PlayerMenu.Menu.performed -= DoMenu;
        inputActions.PlayerMenu.Menu.Disable();
    }

    private void DoMenu(InputAction.CallbackContext obj)
    {
        Debug.Log("Do Menu");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
