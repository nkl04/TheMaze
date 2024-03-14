
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header ("---------Audio Source-----------")]
    [SerializeField ] AudioSource musicSource;
    [SerializeField ] AudioSource SFXSource;

    [Header ("---------Audio Clip-----------")]
    public AudioClip background;
    public AudioClip quizbackground;
    public AudioClip correctAns;
    public AudioClip incorrectAns;
    public AudioClip jump1a;
    public AudioClip jump1b;
    public AudioClip jump1c;
    public AudioClip jump2a;
    public AudioClip jump2b;
    public AudioClip win;
    public AudioClip lose;
    public AudioClip death;
    public AudioClip action;
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
        musicSource.clip=background;
        musicSource.Play();
    }
    public void StopMusic()
    {
        musicSource.Stop();
    }
}
