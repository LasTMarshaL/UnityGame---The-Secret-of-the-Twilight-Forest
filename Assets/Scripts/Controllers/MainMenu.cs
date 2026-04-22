using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;
using PlayerPrefs = RedefineYG.PlayerPrefs; // Make PlayerPrefs save game to the web platform cloud.

public class MainMenu : MonoBehaviour // This class manages the main menu, options menu and language settings.
{
    [SerializeField] private Toggle _tutorialToggle;
    private bool _tutorial;

    private bool _wasOptionsMenuOpened = false;

    [SerializeField] private Slider _musicVolumeSlider;
    [SerializeField] private AudioMixer _musicAudioMixer;

    [SerializeField] private Slider _soundVolumeSlider;
    [SerializeField] private AudioMixer _soundAudioMixer;

    [SerializeField] private Button _continueButton;

    private bool _isContectedToServer = false;

    private void OnEnable()
    {
        YG2.onGetSDKData += LoadOptions;

        if (YG2.saves != null)
        {
            LoadOptions();
        }
    }
    private void OnDisable() => YG2.onGetSDKData -= LoadOptions;

    private void Update()
    {
        if (_isContectedToServer)
        {
            if (_continueButton != null)
            {
                if (!PlayerPrefs.HasKey("NewGame") || PlayerPrefs.GetInt("NewGame") == 1)
                {
                    _continueButton.interactable = false;
                }
                else
                {
                    _continueButton.interactable = true;
                }
            }
        }
    }

    /// <summary>
    /// Launches next gaming scene.
    /// </summary>
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// Turns on / off tutarial menu after launching game proccess.
    /// </summary>
    public void SkipTutorial()
    {
        if (_isContectedToServer)
        {
            if (_wasOptionsMenuOpened)
            {
                _tutorial = !_tutorial;

                if (_tutorial)
                {
                    PlayerPrefs.SetInt("Tutorial", 1);
                    PlayerPrefs.Save();
                }
                else
                {
                    PlayerPrefs.SetInt("Tutorial", 0);
                    PlayerPrefs.Save();
                }
            }
        }
    }

    /// <summary>
    /// Manages sounds audio volume.
    /// </summary>
    public void SoundAudioVolume()
    {
        if (_isContectedToServer)
        {
            _soundAudioMixer.SetFloat("SoundsVolume", _soundVolumeSlider.value);
            PlayerPrefs.SetFloat("SoundsVolume", _soundVolumeSlider.value);
            PlayerPrefs.Save();
        }
    }

    /// <summary>
    /// Manages music audio volume.
    /// </summary>
    public void MusicAudioVolume()
    {
        if (_isContectedToServer)
        {
            _musicAudioMixer.SetFloat("MusicVolume", _musicVolumeSlider.value);
            PlayerPrefs.SetFloat("MusicVolume", _musicVolumeSlider.value);
            PlayerPrefs.Save();
        }
    }

    /// <summary>
    /// Sets the russian language for game.
    /// </summary>
    public void SetRussianLanguage()
    {
        YG2.SwitchLanguage("ru");
    }

    /// <summary>
    /// Sets the english language for game.
    /// </summary>
    public void SetEnglishLanguage()
    {
        YG2.SwitchLanguage("en");
    }

    /// <summary>
    /// Checks if optionsmenu opened (it is needed to control SkipTutorial method and toggle).
    /// </summary>
    public void OpenMenuFlag()
    {
        _wasOptionsMenuOpened = !_wasOptionsMenuOpened;
    }

    /// <summary>
    /// Loads parametes from options menu.
    /// </summary>
    public void LoadOptions()
    {
        if (PlayerPrefs.HasKey("SoundsVolume"))
        {
            _soundVolumeSlider.value = PlayerPrefs.GetFloat("SoundsVolume");
            _soundAudioMixer.SetFloat("SoundsVolume", _soundVolumeSlider.value);
        }
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            _musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
            _musicAudioMixer.SetFloat("MusicVolume", _musicVolumeSlider.value);
        }

        if (PlayerPrefs.HasKey("Tutorial"))
        {
            if (_tutorialToggle != null)
            {
                if (PlayerPrefs.GetInt("Tutorial") == 1)
                {
                    _tutorial = true;
                }
                else if (PlayerPrefs.GetInt("Tutorial") == 0)
                {
                    _tutorial = false;
                }
                else
                {
                    _tutorial = _tutorialToggle.isOn;
                }
            }
        }
        else
        {
            _tutorial = true;
            _tutorialToggle.isOn = _tutorial;

            PlayerPrefs.SetInt("Tutorial", 1);
            PlayerPrefs.Save();
        }

        _isContectedToServer = true;
    }
}
