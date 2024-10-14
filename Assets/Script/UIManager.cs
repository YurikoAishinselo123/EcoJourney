using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    //public GameObject pauseMenu;         // Reference to the pause menu UI
    public PointManager pointManager;

    void Start()
    {
        // Find and cache the PointManager
        //pauseMenu.SetActive(false);     
        UpdateScore();                   // Update the score display at start
    }

    void Update()
    {
        UpdateScore();
        Debug.Log("Point : " + pointManager.GetScore());
    }

    public void UpdateScore()
    {
        if (pointManager != null)
        {
            scoreText.text = "" + pointManager.GetScore().ToString();
        }
    }

    // Method to show the pause menu
    public void ShowPauseMenu()
    {
        //pauseMenu.SetActive(true);
        Time.timeScale = 0;  // Pause the game
    }

    // Method to hide the pause menu and resume the game
    public void ResumeGame()
    {
        //pauseMenu.SetActive(false);
        Time.timeScale = 1;  // Resume the game
    }
}
