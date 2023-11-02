using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuUI : MonoBehaviour
{
    private Button _buttonMaze;
    //private Button _buttonVsAI;
    //private Button _buttonVsLocal;
    private Button _buttonExit;

    //private int m_ClickCount = 0;

    //Add logic that interacts with the UI controls in the `OnEnable` methods
    private void OnEnable()
    {
        // The UXML is already instantiated by the UIDocument component
        var uiDocument = GetComponent<UIDocument>();

        _buttonMaze = uiDocument.rootVisualElement.Q("buttonMaze") as Button;
        _buttonMaze.RegisterCallback<ClickEvent>(StartMaze);

        //_buttonVsAI = uiDocument.rootVisualElement.Q("buttonVsAI") as Button;
        //_buttonVsAI.RegisterCallback<ClickEvent>(StartVsAI);

        //_buttonVsLocal = uiDocument.rootVisualElement.Q("buttonVsLocal") as Button;
        //_buttonVsLocal.RegisterCallback<ClickEvent>(StartVsLocal);

        _buttonExit = uiDocument.rootVisualElement.Q("buttonExit") as Button;
        _buttonExit.RegisterCallback<ClickEvent>(ExitGame);

    }

    private void OnDisable()
    {
        _buttonMaze.UnregisterCallback<ClickEvent>(StartMaze);
        //_buttonVsAI.UnregisterCallback<ClickEvent>(StartVsAI);
        //_buttonVsLocal.UnregisterCallback<ClickEvent>(StartVsLocal);
        _buttonExit.UnregisterCallback<ClickEvent>(ExitGame);
    }

    private void StartMaze(ClickEvent evt)
    {
        Button button = evt.currentTarget as Button;
        //Debug.Log("Button was clicked! " + button.name);
        SceneManager.LoadScene(sceneName: "DesignMaze");
    }

    private void StartVsAI(ClickEvent evt)
    {
        Button button = evt.currentTarget as Button;
        //Debug.Log("Button was clicked! " + button.name);
        SceneManager.LoadScene(sceneName: "VsAiScene");
    }

    private void StartVsLocal(ClickEvent evt)
    {
        Button button = evt.currentTarget as Button;
        //Debug.Log("Button was clicked! " + button.name);
        SceneManager.LoadScene(sceneName: "VsLocalScene");
    }

    private void ExitGame(ClickEvent evt)
    {
        Button button = evt.currentTarget as Button;
        //Debug.Log("Button was clicked! " + button.name);
        Application.Quit();
    }

}
