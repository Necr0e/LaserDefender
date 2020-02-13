using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    private int currentLevel = 0;
    private const float LoadDelay = 2f;
    
    public void LoadStartMenu()
    {
        FindObjectOfType<GameSession>().ResetGame();
        SceneManager.LoadScene(0);
    }
//TODO: get score to work after reset.
    public void LoadNextLevel()
    {
        currentLevel += 1;
        SceneManager.LoadScene(currentLevel);
    }

    public void LoadGameOver()
    {
        currentLevel = SceneManager.sceneCountInBuildSettings;
        StartCoroutine(DelayLevelLoad());
    }

    IEnumerator DelayLevelLoad()
    {
        yield return new WaitForSeconds(LoadDelay);
        SceneManager.LoadScene(currentLevel - 2);
    }

    public void LoadCreditsPage()
    {
        currentLevel = SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(currentLevel - 1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
