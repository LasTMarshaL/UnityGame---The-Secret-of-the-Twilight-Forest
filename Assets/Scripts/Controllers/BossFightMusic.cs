using UnityEngine;

public class BossFightMusic : MonoBehaviour 
{
    [SerializeField] private AudioSource _audioSource;

    [HideInInspector]
    public AudioSource audioSource => _audioSource;

    [SerializeField] private AudioClip _bossFightingMusic;
    [SerializeField] private AudioClip _gamePlayMusic;

    [HideInInspector]
    public bool IsBossFighting { get; private set; }

    public void StartBossFight()
    {
        if (_audioSource.isPlaying)
        {
            _audioSource.Stop();
        }

        _audioSource.clip = _bossFightingMusic;
        _audioSource.Play();

        IsBossFighting = true;
    }

    public void EndBossFight()
    {
        if (_audioSource.isPlaying)
        {
            _audioSource.Stop();
        }

        _audioSource.clip = _gamePlayMusic;
        _audioSource.Play();

        IsBossFighting = false;
    }
}
