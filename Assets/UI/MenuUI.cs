using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuUI : MonoBehaviour
{
    private Button _buttonMaze;
    private Button _buttonLoad;
    private Button _buttonExit;

    //Add logic that interacts with the UI controls in the `OnEnable` methods
    private void OnEnable()
    {
        // The UXML is already instantiated by the UIDocument component
        var uiDocument = GetComponent<UIDocument>();

        _buttonMaze = uiDocument.rootVisualElement.Q("buttonMaze") as Button;
        _buttonMaze.RegisterCallback<ClickEvent>(StartMaze);

        _buttonLoad = uiDocument.rootVisualElement.Q("buttonLoad") as Button;
        _buttonLoad.RegisterCallback<ClickEvent>(LoadMaze);

        _buttonExit = uiDocument.rootVisualElement.Q("buttonExit") as Button;
        _buttonExit.RegisterCallback<ClickEvent>(ExitGame);

    }

    private void OnDisable()
    {
        _buttonMaze.UnregisterCallback<ClickEvent>(StartMaze);
        _buttonExit.UnregisterCallback<ClickEvent>(ExitGame);
    }

    private void StartMaze(ClickEvent evt)
    {
        Button button = evt.currentTarget as Button;
        //Debug.Log("Button was clicked! " + button.name);
        SceneManager.LoadScene(sceneName: "DesignMaze");
    }

    private void LoadMaze(ClickEvent evt)
    {
        Button button = evt.currentTarget as Button;
        SceneManager.LoadScene(sceneName: "DesignMaze");
    }

    private void StartVsAI(ClickEvent evt)
    {
        Button button = evt.currentTarget as Button;
        SceneManager.LoadScene(sceneName: "VsAiScene");
    }

    private void StartVsLocal(ClickEvent evt)
    {
        Button button = evt.currentTarget as Button;
        SceneManager.LoadScene(sceneName: "VsLocalScene");
    }

    private void ExitGame(ClickEvent evt)
    {
        Button button = evt.currentTarget as Button;
        Application.Quit();
    }

}
