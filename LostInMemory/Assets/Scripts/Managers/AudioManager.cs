using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource backgroundMusicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Background Audio Clip")]
    [SerializeField] private AudioClip bgMusic;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayBackgroundMusic();
    }

    void PlayBackgroundMusic()
    {
        if (backgroundMusicSource != null && bgMusic != null)
        {
            backgroundMusicSource.clip = bgMusic;
            backgroundMusicSource.loop = true;
            backgroundMusicSource.Play();
        }
    }
}