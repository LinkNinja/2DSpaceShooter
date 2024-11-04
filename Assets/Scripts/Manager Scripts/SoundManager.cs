using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;

    public AudioSource dialogueSoundEffectSource;

    public AudioSource soundEffectSource;
    public AudioSource musicSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void PlayExplosion(AudioClip clip)
    {
        soundEffectSource.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void PlaySound(AudioClip sound)
    {
        dialogueSoundEffectSource.PlayOneShot(sound);
    }
}
