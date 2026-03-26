using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Music")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioClip musicClip; 

    [Header("SFX")]
    [SerializeField] AudioSource sfxSource;

    [Header("Clips")]
    [SerializeField] AudioClip jumpSFX;
    [SerializeField] AudioClip hitSFX;
    [SerializeField] AudioClip deathSFX;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        musicSource.clip = musicClip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlayMusic(AudioClip clip)
    {
        if (musicSource.clip == clip) return; // already playing
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }
    public void PlayJump()  => sfxSource.PlayOneShot(jumpSFX);
    public void PlayHit()   => sfxSource.PlayOneShot(hitSFX);
    public void PlayDeath() => sfxSource.PlayOneShot(deathSFX);

    public void SetMusicVolume(float volume) => musicSource.volume = volume;
}