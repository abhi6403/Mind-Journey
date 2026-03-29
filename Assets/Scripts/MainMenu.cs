using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Load the first level when Play button is pressed
    public void PlayGame()
    {
        SceneManager.LoadScene("Level_AnxietyForest");
    }

    // Quit the game when Quit button is pressed
    public void QuitGame()
    {
        Debug.Log("Game Quit");

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stops play mode in Unity
        #else
        Application.Quit(); // Quits the built game
        #endif
    }
}