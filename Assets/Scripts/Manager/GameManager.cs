using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool isPaused { get; private set; }
    public bool isGameActive { get; private set; } // Quản lý trạng thái chơi

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject); // Đảm bảo chỉ có 1 Instance

        Time.timeScale = 1f;
        isGameActive = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isGameActive)
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;

        if (isPaused) UIManager.Instance.ShowPauseMenu();
        else UIManager.Instance.HidePauseMenu();
    }

    public void WinLevel()
    {
        if (!isGameActive) return;
        
        FinishGame();
        UnlockNextLevel();
        // Kiểm tra xem Scene hiện tại có UIManager không trước khi gọi
        if (UIManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX(SoundType.GameWin);
            UIManager.Instance.ShowWinGameUI();
        }
    }

    public void GameOver()
    {
        if (!isGameActive) return;

        isGameActive = false;

        if (UIManager.Instance != null)
        {
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlaySFX(SoundType.GameOver);
            }

            UIManager.Instance.ShowOverGameUI();
        }

        Time.timeScale = 0f;
    }

    private void FinishGame()
    {
        isGameActive = false;
        Time.timeScale = 0f;
    }

    private void UnlockNextLevel()
    {
        int currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
        // Giả sử index 0 là Main Menu, các Level bắt đầu từ index 1
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        if (currentBuildIndex >= unlockedLevel)
        {
            PlayerPrefs.SetInt("UnlockedLevel", currentBuildIndex + 1);
            PlayerPrefs.Save();
        }
    }
}