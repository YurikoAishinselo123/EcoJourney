using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float startTime = 300f; // Set the starting time in seconds (e.g., 300 seconds = 5 minutes)
    private float timeRemaining;    // Time remaining for countdown
    public TextMeshProUGUI timerText; // Reference to the TextMeshPro text for displaying the timer
    public GameObject gameOverMenu; // Reference to the Game Over UI panel
    private bool isGameOver = false; // Flag to check if game is over
    public TextMeshProUGUI rubbishScore;
    public PointManager pointManager;
  

    void Start()
    {
        timeRemaining = startTime; // Initialize the timer
        gameOverMenu.SetActive(false); // Hide the game over menu at start
    }

    void Update()
    {
        if (!isGameOver)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime; // Decrease time remaining
                UpdateTimerDisplay(); // Update the displayed timer
            }
            else
            {
                timeRemaining = 0; // Ensure it doesn't go negative
                GameOver(); // Trigger game over
            }
        }
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);

        timerText.text = string.Format("{0:D2}:{1:D2}", minutes, seconds);
    }

    void GameOver()
    {
        isGameOver = true; // Set the game over flag
        rubbishScore.text = "" + pointManager.GetScore().ToString();
        gameOverMenu.SetActive(true); // Show game over menu
        Time.timeScale = 0; // Pause the game
    }

    public void ResetTimer()
    {
        // Reset the timer and hide the game over menu
        timeRemaining = startTime;
        gameOverMenu.SetActive(false);
        isGameOver = false;

        // Resume time before reloading the scene
        Time.timeScale = 1;

        // Reload the scene
        SceneManager.LoadScene("GamePlay"); // Replace with your scene name
    }
}
