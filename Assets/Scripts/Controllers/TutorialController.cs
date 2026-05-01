using UnityEngine;
using YG;
using PlayerPrefs = RedefineYG.PlayerPrefs;
public class Tutorial : MonoBehaviour
{
    [SerializeField] private GameObject _tutorial;
    [SerializeField] private GameObject _mobileTutorial;

    private PlayerController _player;

    private void OnEnable()
    {
        YG2.onGetSDKData += ShowTutorial;

        if (YG2.saves != null)
        {
            ShowTutorial();
        }
    }
    private void OnDisable() => YG2.onGetSDKData -= ShowTutorial;

    private void Start()
    {
        _player = PlayerController.playerInstance;
    }

    public void ShowTutorial()
    {
        if (PlayerPrefs.GetInt("Tutorial") == 0 || PlayerPrefs.GetInt("NewGame") == 0 || !PlayerPrefs.HasKey("NewGame"))
        {
            _tutorial.SetActive(false);
            _mobileTutorial.SetActive(false);
            PauseLogic.ChangePauseState(false);
        }
        else
        {
            if (YG2.envir.deviceType == "mobile")
            {
                _mobileTutorial.SetActive(true);
                PauseLogic.ChangePauseState(true);
                Time.timeScale = 0;
            }
            else if (YG2.envir.deviceType == "desktop")
            {
                _tutorial.SetActive(true);
                PauseLogic.ChangePauseState(true);
                Time.timeScale = 0;
            }
        }
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
        PauseLogic.ChangePauseState(false);
    }
}
