using UnityEngine;
using UnityEngine.UI;

public class SelectLevelUI : MonoBehaviour
{
    public Button[] levelButtons;
    public SceneController sceneController;

    private void Start()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            int levelNumber = i + 1;

            levelButtons[i].interactable = levelNumber <= unlockedLevel;

            int capturedLevel = levelNumber;
            levelButtons[i].onClick.AddListener(() =>
            {
                sceneController.LoadLevel(capturedLevel);
            });
        }
    }
}