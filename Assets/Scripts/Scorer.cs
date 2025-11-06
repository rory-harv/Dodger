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

    void Start()
    {
        scoreText = uiDocument.rootVisualElement.Q<Label>("ScoreLabel");
        livesText = uiDocument.rootVisualElement.Q<Label>("LivesLeft");
        restartButton = uiDocument.rootVisualElement.Q<Button>("RestartButton");
        restartButton.clicked += ReloadScene;
        restartButton.style.display = DisplayStyle.None;
    }

    void Update()
    {
        if (finished == false)
        {
            elapsedTime += Time.deltaTime;
            score = Mathf.FloorToInt(elapsedTime * scoreMultiplier);
            scoreText.text = "Score: " + score;
            livesText.text = "Lives Left: " + livesleft;
            if (collision == true && livesleft > -1)
            {
                livesleft--;
            }
        }
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
            }
        }
        collision = false;
    }
    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
