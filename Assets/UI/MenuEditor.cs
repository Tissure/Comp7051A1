using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuEditor : EditorWindow
{

    [MenuItem("Window/UI Toolkit/MenuEditor")]
    public static void ShowExample()
    {
        MenuEditor wnd = GetWindow<MenuEditor>();
        wnd.titleContent = new GUIContent("MenuEditor");
    }

    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;
    private int m_ClickCount = 0;
    private const string m_ButtonPrefix = "button";

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        //// VisualElements objects can contain other VisualElement following a tree hierarchy.
        //VisualElement label = new Label("Hello World! From C#");
        //root.Add(label);

        // Instantiate UXML
        VisualElement labelFromUXML = m_VisualTreeAsset.Instantiate();
        root.Add(labelFromUXML);

        //Call the event handler
        SetupButtonHandler();
    }
    //Functions as the event handlers for your button click and number counts 
    private void SetupButtonHandler()
    {
        VisualElement root = rootVisualElement;

        var buttons = root.Query<Button>();
        buttons.ForEach(RegisterHandler);
    }

    private void RegisterHandler(Button button)
    {
        button.RegisterCallback<ClickEvent>(PrintClickMessage);
    }

    private void PrintClickMessage(ClickEvent evt)
    {
        VisualElement root = rootVisualElement;

        ++m_ClickCount;

        //Because of the names we gave the buttons and toggles, we can use the
        //button name to find the toggle name.
        Button button = evt.currentTarget as Button;

        Debug.Log("Button was clicked! " +
            button.name + " Count: " + m_ClickCount );
    }
}
