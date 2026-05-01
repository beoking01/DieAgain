using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectLevelUI : MonoBehaviour
{
    public Button[] levelButtons;

    private void Start()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            int levelNumber = i + 1;

            levelButtons[i].interactable = LevelManager.Instance.IsLevelUnlocked(levelNumber);

            int capturedLevel = levelNumber;
            levelButtons[i].onClick.AddListener(() =>
            {
                SceneController.LoadLevelOrFallback(capturedLevel);
            });
        }
    }
}