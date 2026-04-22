using UnityEngine;

public class BossFightMusic : MonoBehaviour // This class manages the music during the boss fight.
{
    [SerializeField] private AudioSource _audioSource;

    [HideInInspector]
    public AudioSource audioSource => _audioSource;

    [SerializeField] private AudioClip _bossFightingMusic;
    [SerializeField] private AudioClip _gamePlayMusic;

    [HideInInspector]
    public bool IsBossFighting { get; private set; }

    /// <summary>
    /// Begins the boss fight by playing the boss music and updating the boss fight state.
    /// </summary>
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

    /// <summary>
    /// Ends the boss fight, restores gameplay music, and updates the boss fight state.
    /// </summary>
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
