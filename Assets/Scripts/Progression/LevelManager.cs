using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;

    public static LevelManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<LevelManager>();

                if (instance == null)
                {
                    GameObject managerObject = new GameObject("LevelManager");
                    instance = managerObject.AddComponent<LevelManager>();
                }
            }

            return instance;
        }
    }

    public GameData GameData { get; private set; }

    public int CurrentUnlockedLevel
    {
        get { return GameData != null ? GameData.unlockedLevel : 1; }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        LoadData();
    }

    public void LoadData()
    {
        GameData = SaveSystem.Load();
    }

    public bool IsLevelUnlocked(int level)
    {
        if (level < 1)
        {
            return false;
        }

        return level <= CurrentUnlockedLevel;
    }

    public void CompleteLevel(int currentLevel)
    {
        if (currentLevel < 1)
        {
            return;
        }

        if (GameData == null)
        {
            GameData = new GameData();
        }

        if (currentLevel >= CurrentUnlockedLevel)
        {
            GameData.unlockedLevel = currentLevel + 1;
            SaveSystem.Save(GameData);
        }
    }
}