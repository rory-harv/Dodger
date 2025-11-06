using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public UIDocument uiDocument;
    private Label titleText;
    private Button startButton;
    public bool start = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        titleText = uiDocument.rootVisualElement.Q<Label>("TitleLabel");
        startButton = uiDocument.rootVisualElement.Q<Button>("StartButton");
        startButton.clicked += ReloadScene;
        startButton.style.display = DisplayStyle.None;
    }

    // Update is called once per frame
    void Update()
    {
        if (start == true)
        {
            titleText.style.display = DisplayStyle.None;
        }
    }
    void ReloadScene()
    {
        start = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
