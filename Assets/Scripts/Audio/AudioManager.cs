using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfx2DSource;

    [Header("Music")]
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip gameplayMusic;

    [Header("Sound Effects")]
    [SerializeField] private List<SoundData> sounds = new List<SoundData>();

    private Dictionary<SoundType, SoundData> soundDictionary;

    private const string BGM_VOLUME_KEY = "BGM_VOLUME";
    private const string SFX_VOLUME_KEY = "SFX_VOLUME";

    private float bgmVolume = 1f;
    private float sfxVolume = 1f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        InitDictionary();
        LoadVolume();
    }

    private void InitDictionary()
    {
        soundDictionary = new Dictionary<SoundType, SoundData>();

        foreach (SoundData sound in sounds)
        {
            if (!soundDictionary.ContainsKey(sound.soundType))
            {
                soundDictionary.Add(sound.soundType, sound);
            }
        }
    }

    private void LoadVolume()
    {
        bgmVolume = PlayerPrefs.GetFloat(BGM_VOLUME_KEY, 1f);
        sfxVolume = PlayerPrefs.GetFloat(SFX_VOLUME_KEY, 1f);

        bgmSource.volume = bgmVolume;
        sfx2DSource.volume = sfxVolume;
    }

    public void PlayMenuMusic()
    {
        PlayMusic(menuMusic);
    }

    public void PlayGameplayMusic()
    {
        PlayMusic(gameplayMusic);
    }

    public void PlayMusic(AudioClip clip)
    {
        if (clip == null) return;

        if (bgmSource.clip == clip && bgmSource.isPlaying)
            return;

        bgmSource.clip = clip;
        bgmSource.loop = true;
        bgmSource.volume = bgmVolume;
        bgmSource.Play();
    }

    public void StopMusic()
    {
        bgmSource.Stop();
    }

    public void PlaySFX(SoundType soundType)
    {
        if (!soundDictionary.ContainsKey(soundType))
        {
            Debug.LogWarning("Sound not found: " + soundType);
            return;
        }

        SoundData sound = soundDictionary[soundType];

        if (sound.clip == null) return;

        sfx2DSource.pitch = sound.pitch;
        sfx2DSource.PlayOneShot(sound.clip, sound.volume * sfxVolume);
    }

    public void PlaySFX3D(SoundType soundType, Vector3 position)
    {
        if (!soundDictionary.ContainsKey(soundType))
        {
            Debug.LogWarning("Sound not found: " + soundType);
            return;
        }

        SoundData sound = soundDictionary[soundType];

        if (sound.clip == null) return;

        GameObject tempAudio = new GameObject("3D Sound - " + soundType);
        tempAudio.transform.position = position;

        AudioSource audioSource = tempAudio.AddComponent<AudioSource>();
        audioSource.clip = sound.clip;
        audioSource.volume = sound.volume * sfxVolume;
        audioSource.pitch = sound.pitch;

        audioSource.spatialBlend = 1f;
        audioSource.minDistance = 2f;
        audioSource.maxDistance = 20f;
        audioSource.rolloffMode = AudioRolloffMode.Linear;

        audioSource.Play();

        Destroy(tempAudio, sound.clip.length + 0.2f);
    }

    public void SetBGMVolume(float volume)
    {
        bgmVolume = volume;
        bgmSource.volume = bgmVolume;

        PlayerPrefs.SetFloat(BGM_VOLUME_KEY, bgmVolume);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
        sfx2DSource.volume = sfxVolume;

        PlayerPrefs.SetFloat(SFX_VOLUME_KEY, sfxVolume);
        PlayerPrefs.Save();
    }

    public float GetBGMVolume()
    {
        return bgmVolume;
    }

    public float GetSFXVolume()
    {
        return sfxVolume;
    }
}