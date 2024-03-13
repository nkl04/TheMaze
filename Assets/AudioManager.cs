
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header ("---------Audio Source-----------")]
    [SerializeField ] AudioSource musicSource;
    [SerializeField ] AudioSource SFXSource;

    [Header ("---------Audio Clip-----------")]
    public AudioClip background;
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
        musicSource.clip=background;
        musicSource.Play();
    }
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
