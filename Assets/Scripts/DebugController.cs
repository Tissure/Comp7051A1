using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DebugController : MonoBehaviour
{
    bool showConsole;
    string input;

    public static DebugCommand<float, float, float> CHANGE_BG_COLOR;
    public static DebugCommand RESET_ROUND;

    private List<object> commandList;

    private void Awake()
    {
        CHANGE_BG_COLOR = new DebugCommand<float, float, float>("change_bg_color", "Changes background color.", "change_bg_color <R> <G> <B>", (r,g,b) => {
            GameObject.Find("Cube").gameObject.GetComponent<MeshRenderer>().material.color =  new Color(r, g, b);
        });

        RESET_ROUND = new DebugCommand("reset_round", "Resets the round", "reset_round", () => {
            PongGameManager.Instance.RestartLevel();
        });

        commandList = new List<object> { 
            CHANGE_BG_COLOR, 
            RESET_ROUND
        };
    }

    public void OnToggleDebug(InputValue value)
    {
        showConsole = !showConsole;
    }

    public void OnReturn(InputValue value)
    {
        if(showConsole)
        {
            HandleInput();
            input = "";
        }
    }

    private void OnGUI()
    {
        if(!showConsole) { return; }
        float y = 0f;
        GUI.Box(new Rect(0, y, Screen.width, 30), "");
        GUI.backgroundColor = new Color(0,0, 0, 0);
        GUI.SetNextControlName("console");
        input = GUI.TextField(new Rect(20f, y + 5f, Screen.width - 20f, 20f), input);
        GUI.FocusControl("console");
    }

    private void HandleInput()
    {

        string[] properties = input.Split(' ');

        for(int i = 0; i < commandList.Count; i++)
        {
            DebugCommandBase commandBase = commandList[i] as DebugCommandBase;
            if (input.Contains(commandBase.CommandId))
            {
                if (commandList[i]as DebugCommand != null)
                {
                    (commandList[i] as DebugCommand).Invoke();
                } else if (commandList[i] as DebugCommand<float, float, float> != null)
                {
                    (commandList[i] as DebugCommand<float, float, float>).Invoke(float.Parse(properties[1]), float.Parse(properties[2]), float.Parse(properties[3]));
                }
            }
        }
    }
}
