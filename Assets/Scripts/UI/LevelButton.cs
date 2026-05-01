using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelButton : MonoBehaviour
{
    [SerializeField] private int levelIndex = 1;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private GameObject lockIcon;
    [SerializeField] private Sprite lockedBackground;
    [SerializeField] private Sprite unlockedBackground;
    [SerializeField] private bool playUnlockEffectOnLoad = true;
    [SerializeField] private float unlockScaleMultiplier = 1.1f;
    [SerializeField] private float unlockAnimationDuration = 0.18f;

    private Button button;
    private RectTransform rectTransform;
    private Vector3 originalScale;
    private Coroutine unlockAnimationRoutine;

    private void Awake()
    {
        button = GetComponent<Button>();
        rectTransform = GetComponent<RectTransform>();

        if (backgroundImage == null)
        {
            backgroundImage = button.targetGraphic as Image;
        }

        if (lockIcon == null)
        {
            Transform lockIconTransform = transform.Find("LockIcon");

            if (lockIconTransform != null)
            {
                lockIcon = lockIconTransform.gameObject;
            }
        }

        if (rectTransform != null)
        {
            originalScale = rectTransform.localScale;
        }

        button.onClick.AddListener(LoadLevel);
    }

    private void OnEnable()
    {
        UpdateButtonState(playUnlockEffectOnLoad);
    }

    private void OnDestroy()
    {
        if (button != null)
        {
            button.onClick.RemoveListener(LoadLevel);
        }

        if (unlockAnimationRoutine != null)
        {
            StopCoroutine(unlockAnimationRoutine);
            unlockAnimationRoutine = null;
        }
    }

    private void UpdateButtonState(bool allowUnlockEffect)
    {
        if (button == null)
        {
            return;
        }

        bool isUnlocked = levelIndex >= 1 && LevelManager.Instance.IsLevelUnlocked(levelIndex);
        button.interactable = isUnlocked;

        if (backgroundImage != null)
        {
            Sprite desiredBackground = isUnlocked ? unlockedBackground : lockedBackground;

            if (desiredBackground != null)
            {
                backgroundImage.sprite = desiredBackground;
            }
        }

        if (lockIcon != null)
        {
            lockIcon.SetActive(!isUnlocked);
        }

        if (isUnlocked && allowUnlockEffect && LevelManager.Instance.CurrentUnlockedLevel == levelIndex)
        {
            PlayUnlockEffect();
        }
        else if (rectTransform != null)
        {
            rectTransform.localScale = originalScale;
        }
    }

    private void LoadLevel()
    {
        if (levelIndex < 1 || !LevelManager.Instance.IsLevelUnlocked(levelIndex))
        {
            return;
        }

        SceneController.LoadLevelOrFallback(levelIndex);
    }

    private void PlayUnlockEffect()
    {
        if (rectTransform == null)
        {
            return;
        }

        if (unlockAnimationRoutine != null)
        {
            StopCoroutine(unlockAnimationRoutine);
        }

        unlockAnimationRoutine = StartCoroutine(UnlockAnimationRoutine());
    }

    private IEnumerator UnlockAnimationRoutine()
    {
        float halfDuration = Mathf.Max(0.01f, unlockAnimationDuration * 0.5f);
        Vector3 targetScale = originalScale * unlockScaleMultiplier;

        float elapsed = 0f;
        while (elapsed < halfDuration)
        {
            float t = elapsed / halfDuration;
            rectTransform.localScale = Vector3.Lerp(originalScale, targetScale, t);
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        elapsed = 0f;
        while (elapsed < halfDuration)
        {
            float t = elapsed / halfDuration;
            rectTransform.localScale = Vector3.Lerp(targetScale, originalScale, t);
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        rectTransform.localScale = originalScale;
        unlockAnimationRoutine = null;
    }
}