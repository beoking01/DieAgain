using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static void LoadLevelOrFallback(int levelIndex)
    {
        Time.timeScale = 1f;

        string sceneName = "Level_" + levelIndex.ToString("00");

        if (TryLoadScene(sceneName))
        {
            return;
        }

        Debug.LogWarning("Scene '" + sceneName + "' could not be loaded. Returning to SelectLevel.");

        if (!TryLoadScene("SelectLevel"))
        {
            Debug.LogWarning("Fallback scene 'SelectLevel' could not be loaded.");
        }
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void LoadSelectLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SelectLevel");
    }

    public void LoadLevel(int levelIndex)
    {
        LoadLevelOrFallback(levelIndex);
    }

    public void ReloadCurrentLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1f;

        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = currentIndex + 1;

        if (nextIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextIndex);
        }
        else
        {
            SceneManager.LoadScene("SelectLevel");
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private static bool TryLoadScene(string sceneName)
    {
        if (!Application.CanStreamedLevelBeLoaded(sceneName))
        {
            return false;
        }

        SceneManager.LoadScene(sceneName);
        return true;
    }

    public void Resume()
    {
        GameManager.Instance.TogglePause();
    }
}