using UnityEngine;

public class PlaySoundMenu : MonoBehaviour
{
    void Start()
    {
        AudioManager.Instance.PlayMenuMusic();
    }
}