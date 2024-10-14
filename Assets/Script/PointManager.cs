using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : MonoBehaviour
{
    private int score = 0;  // Variable to keep track of the score

    // Method to add points
    public void AddPoints(int points)
    {
        score += points;
        Debug.Log("Score: " + score);  // Print the score to the console (you can replace this with UI update)
    }

    // Method to get the current score
    public int GetScore()
    {
        return score;
    }
}
