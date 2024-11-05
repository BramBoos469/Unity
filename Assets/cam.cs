using UnityEngine;
using UnityEngine.UI;  // Required for UI elements
using UnityEngine.SceneManagement;  // Required for restarting the scene
using System.Collections;

public class CameraSwitchTrigger : MonoBehaviour
{
    // Assign the cameras in the Inspector
    public Camera mainCamera;
    public Camera secondaryCamera;

    // Assign the button and its CanvasGroup in the Inspector
    public Button restartButton;
    public CanvasGroup buttonCanvasGroup;

    // Variable to store whether the game has ended or not
    private bool gameEnded = false;

    private void Start()
    {
        // Ensure the button is initially invisible and not interactable
        buttonCanvasGroup.alpha = 0;
        buttonCanvasGroup.interactable = false;
        buttonCanvasGroup.blocksRaycasts = false;
        restartButton.gameObject.SetActive(false);
    }

    // Check if the player enters the trigger zone
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the player
        if (other.CompareTag("Player") && !gameEnded)
        {
            // Switch cameras
            mainCamera.enabled = false;
            secondaryCamera.enabled = true;

            // Unlock the cursor so the player can move the mouse
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // Start the process to show the button after 2 seconds
            StartCoroutine(ShowRestartButtonAfterDelay(2f));

            // Mark the game as ended
            gameEnded = true;
        }
    }

    // Coroutine to fade in the button after a delay
    private IEnumerator ShowRestartButtonAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Activate the button and start fading it in
        restartButton.gameObject.SetActive(true);
        StartCoroutine(FadeInButton());

        // Add listener to restart the game when the button is clicked
        restartButton.onClick.AddListener(RestartGame);
    }

    // Coroutine to fade in the button smoothly
    private IEnumerator FadeInButton()
    {
        float fadeDuration = 1f;
        float elapsedTime = 0f;

        // Gradually increase the alpha of the CanvasGroup to make the button appear
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            buttonCanvasGroup.alpha = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            yield return null;
        }

        // After fade-in, make the button interactable
        buttonCanvasGroup.interactable = true;
        buttonCanvasGroup.blocksRaycasts = true;
    }

    // Function to restart the game
    private void RestartGame()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
