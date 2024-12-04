using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip, float volume)
    {
        if(audioSource != null)
        {
            audioSource.PlayOneShot(clip, volume);
        }
    }
}
