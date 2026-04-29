using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject pauseMenu;
    public GameObject winGameUI;
    public GameObject overGameUI;

    private void Awake()
    {
        Instance = this;

        HideAll();
    }

    public void HideAll()
    {
        pauseMenu.SetActive(false);
        winGameUI.SetActive(false);
        overGameUI.SetActive(false);
    }

    public void ShowPauseMenu()
    {
        pauseMenu.SetActive(true);
    }

    public void HidePauseMenu()
    {
        pauseMenu.SetActive(false);
    }

    public void ShowWinGameUI()
    {
        winGameUI.SetActive(true);
    }

    public void ShowOverGameUI()
    {
        overGameUI.SetActive(true);
    }
}