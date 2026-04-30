using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI")]
    public GameObject pauseMenu;
    public GameObject winGameUI;
    public GameObject overGameUI;

    [Header("Result Character Animation")]
    public Animator resultCharacterAnimator;

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
        HideAll();
        winGameUI.SetActive(true);

        PlayAnimation("Idle");
    }

    public void ShowOverGameUI()
    {
        HideAll();
        overGameUI.SetActive(true);

        PlayAnimation("Die1");
    }

    private void PlayAnimation(string stateName)
    {
        if (resultCharacterAnimator == null) return;

        resultCharacterAnimator.Play(stateName, 0, 0f);
        resultCharacterAnimator.Update(0f);
    }
}