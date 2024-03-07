using UnityEngine;
using Framework;
public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource effectAudioSource;

    protected override void Awake()
    {
        musicAudioSource.loop = true;
        effectAudioSource.loop = false;
    }
    private void PlayMusic(AudioClip audioClip)
    {
        musicAudioSource.clip = audioClip;
        musicAudioSource.Play();
    }

    private void StopMusic()
    {
        musicAudioSource.Stop();
    }

    private void PlayEffect(AudioClip audioClip)
    {
        effectAudioSource.PlayOneShot(audioClip);
    }

    private void SetEffectVolunm(float _index)
    {
        effectAudioSource.volume = _index;
    }

    private void SetMusicVolunm(float _index )
    {
        musicAudioSource.volume = _index;
    }
}
