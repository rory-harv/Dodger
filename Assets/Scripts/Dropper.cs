using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class Dropper : MonoBehaviour
{
    [SerializeField] float timeToWait = 5f;
    public UIDocument uiDocument;
    MeshRenderer myMeshRenderer;
    Rigidbody myRigidBody;
    public static bool paused = false;
    private Button pauseButton;
    void Start()
    {
        myMeshRenderer = GetComponent<MeshRenderer>();
        myRigidBody = GetComponent<Rigidbody>();

        myMeshRenderer.enabled = false;
        myRigidBody.useGravity = false;

        pauseButton = uiDocument.rootVisualElement.Q<Button>("PauseButton");
        pauseButton.style.display = DisplayStyle.None;
    }

    void Update()
    {
        if (paused == false && Scorer.started)  // checks if game is paused after started
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                paused = true;  // initiates bool conditional
            }

            if (Time.time > timeToWait && paused == false)
            {
                myMeshRenderer.enabled = true;
                myRigidBody.useGravity = true;
            }
        }
        if (paused == true)
        {
            pauseButton.style.display = DisplayStyle.Flex;

            myMeshRenderer.enabled = false; // pauses objects
            myRigidBody.useGravity = false;

            pauseButton.clicked += UnPause;
        }
    }

    void UnPause()
    {
        pauseButton.style.display = DisplayStyle.None;  // resumes game
        paused = false;
    }
}
