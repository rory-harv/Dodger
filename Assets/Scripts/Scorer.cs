using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class Scorer : MonoBehaviour
{
    int hits = 0;
    private float elapsedTime = 0f;
    private float score = 0f;
    private float livesleft = 3f;
    public float scoreMultiplier = 10f;
    public UIDocument uiDocument;
    private Label scoreText;
    private Label livesText;
    private Button restartButton;
    public bool finished = false;
    public bool collision = false;
    private Label titleText;
    private Button startButton;
    public static bool started = false;
    void Start()
    {
        scoreText = uiDocument.rootVisualElement.Q<Label>("ScoreLabel");
        livesText = uiDocument.rootVisualElement.Q<Label>("LivesLeft");
        restartButton = uiDocument.rootVisualElement.Q<Button>("RestartButton");
        titleText = uiDocument.rootVisualElement.Q<Label>("TitleLabel");
        startButton = uiDocument.rootVisualElement.Q<Button>("StartButton");
        restartButton.clicked += ReloadScene;
        restartButton.style.display = DisplayStyle.None;
        startButton.clicked += HideTitle;   // removes instructions title page when started
        
    }

    void Update()
    {
        if (started == true && !Dropper.paused) // checks if game has started and is not paused
        {
            if (finished == false)  // checks to ensure player has not died
            {
                elapsedTime += Time.deltaTime;
                score = Mathf.FloorToInt(elapsedTime * scoreMultiplier);
                scoreText.text = "Score: " + score;
                livesText.text = "Lives Left: " + livesleft;
                if (collision == true && livesleft > -1)
                {
                    livesleft--;    // decrease lives left
                }
            }
        }
        else
        {
            if (Dropper.paused || started == false) // if game is paused or unstarted
            {
                score = score;
                elapsedTime = elapsedTime;
                livesleft = livesleft;
            }
            else if (!Dropper.paused && started == false)   // if game not paused and not started (baseline value of variables)
            {
                elapsedTime = 0;
                score = 0;
                livesleft = 0;
            }
        }
    }

    void HideTitle()
    {
        startButton.style.display = DisplayStyle.None;
        titleText.style.display = DisplayStyle.None;
        started = true;
        livesleft = 3;
        Update();
    }
    private void OnCollisionEnter(Collision other)
    {
        collision = true;
        Update();
        if (other.gameObject.tag != "Hit")
        {
            hits++;
            Debug.Log("You've bumped into a thing this many times: " + hits);
            if (hits >= 3)
            {
                Debug.Log("Game Over");
                livesleft = 0;
                livesText.text = "Lives Left: " + livesleft;
                restartButton.style.display = DisplayStyle.Flex;
                finished = true;
                started = false;
            }
        }
        collision = false;
    }
    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
