
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    [Header("---------Audio Source-----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("---------Audio Clip-----------")]
    // Music
    public AudioClip background;
    public AudioClip quizbackground;
    public AudioClip correctAns;
    public AudioClip incorrectAns;
    // SoundEffects
    public AudioClip jump1a;
    public AudioClip jump1b;
    public AudioClip jump1c;
    public AudioClip jump2a;
    public AudioClip jump2b;
    public AudioClip win;
    public AudioClip lose;
    public AudioClip death;
    public AudioClip action;

    private float volumeMusic = 1f;
    private float volumeSFX = 1f;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {

        PlayBackgroundMusic(background);
    }
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void PlayBackgroundMusic(AudioClip background)
    {
        musicSource.clip = background;
        musicSource.Play();
    }
    public void StopMusic()
    {
        musicSource.Stop();
    }
    public void ChangeVolumeMusic()
    {
        volumeMusic += .1f;
        if (volumeMusic > 1f)
        {
            volumeMusic = 0f;
        }
        musicSource.volume = volumeMusic;
    }
    public float GetvolumeMusic()
    {
        return volumeMusic;
    }
    public void ChangeVolumeSFX()
    {
        volumeSFX += .1f;
        if (volumeSFX > 1f)
        {
            volumeSFX = 0f;
        }
        SFXSource.volume = volumeSFX;
    }
    public float GetvolumeSFX()
    {
        return volumeSFX;
    }
}
