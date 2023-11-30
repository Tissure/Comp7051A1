using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PauseUI : MonoBehaviour
{
    private Button _buttonResume;
    private Button _buttonSave;
    private Button _buttonReturn;

    //private int m_ClickCount = 0;

    //Add logic that interacts with the UI controls in the `OnEnable` methods
    private void Awake()
    {
        //DontDestroyOnLoad(this);
    }
    private void OnEnable()
    {
        // The UXML is already instantiated by the UIDocument component
        var uiDocument = GetComponent<UIDocument>();

        _buttonResume = uiDocument.rootVisualElement.Q("buttonResume") as Button;
        _buttonResume.RegisterCallback<ClickEvent>(Resume);

        _buttonSave = uiDocument.rootVisualElement.Q("buttonSave") as Button;
        _buttonSave.RegisterCallback<ClickEvent>(Save);

        _buttonReturn = uiDocument.rootVisualElement.Q("buttonReturn") as Button;
        _buttonReturn.RegisterCallback<ClickEvent>(Return);

        //MazeGameManager.Instance.SetPauseMenu(gameObject);

    }

    private void OnDisable()
    {
        _buttonResume.UnregisterCallback<ClickEvent>(Resume);
        _buttonSave?.UnregisterCallback<ClickEvent>(Save);
        _buttonReturn.UnregisterCallback<ClickEvent>(Return);
    }

   void Resume(ClickEvent evt)
    {
        Debug.Log("resume");
        MazeGameManager.Instance.TogglePause();
    }
    void Save(ClickEvent evt)
    {
        Debug.Log("save");
        MazeGameManager.Instance.SaveGame();
    }
    void Return(ClickEvent evt)
    {
        Debug.Log("return");
        MazeGameManager.Instance.LeaveGame() ;
        SceneManager.LoadScene("MainMenuScene");
    }
}
