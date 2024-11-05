using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // For UI elements
using UnityEngine.Audio; // For audio control

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;   // Reference to the Pause Menu UI
    public Slider audioSlider;       // Slider to control audio volume
    public Slider sensitivitySlider; // Slider to control mouse sensitivity
    public AudioMixer audioMixer;    // Reference to the audio mixer
    public float mouseSensitivity = 1.0f; // Default mouse sensitivity

    private bool isPaused = false; // To check if the game is paused

    void Start()
    {
        // Set initial values for sliders
        audioSlider.onValueChanged.AddListener(SetVolume);
        sensitivitySlider.onValueChanged.AddListener(SetSensitivity);
        sensitivitySlider.value = mouseSensitivity;

        // Initially, the pause menu should be hidden
        pauseMenuUI.SetActive(false);
    }

    void Update()
    {
        // Open/close the pause menu when pressing Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void SetVolume(float volume)
    {
        // Set the audio volume in the mixer (Mixer parameter needs to be exposed)
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20); // Convert linear value to decibel
    }

    public void SetSensitivity(float sensitivity)
    {
        // Set the mouse sensitivity based on the slider value
        mouseSensitivity = sensitivity;
    }

    public void ResumeGame()
    {
        // Deactivate pause menu and resume the game
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Resume the game
        isPaused = false;
    }

    public void PauseGame()
    {
        // Activate pause menu and stop the game
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Pause the game
        isPaused = true;
    }

    public void QuitGame()
    {
        // Quit the application (useful for standalone builds)
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
