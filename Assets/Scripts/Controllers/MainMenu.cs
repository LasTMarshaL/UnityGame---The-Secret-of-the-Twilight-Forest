using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;
using PlayerPrefs = RedefineYG.PlayerPrefs;

public class MainMenu : MonoBehaviour
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

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

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

    public void SoundAudioVolume()
    {
        if (_isContectedToServer)
        {
            _soundAudioMixer.SetFloat("SoundsVolume", _soundVolumeSlider.value);
            PlayerPrefs.SetFloat("SoundsVolume", _soundVolumeSlider.value);
            PlayerPrefs.Save();
        }
    }

    public void MusicAudioVolume()
    {
        if (_isContectedToServer)
        {
            _musicAudioMixer.SetFloat("MusicVolume", _musicVolumeSlider.value);
            PlayerPrefs.SetFloat("MusicVolume", _musicVolumeSlider.value);
            PlayerPrefs.Save();
        }
    }

    public void SetRussianLanguage()
    {
        YG2.SwitchLanguage("ru");
    }

    public void SetEnglishLanguage()
    {
        YG2.SwitchLanguage("en");
    }

    public void OpenMenuFlag()
    {
        _wasOptionsMenuOpened = !_wasOptionsMenuOpened;
    }

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
