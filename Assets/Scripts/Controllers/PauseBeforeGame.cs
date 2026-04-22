using UnityEngine;
using YG;
using PlayerPrefs = RedefineYG.PlayerPrefs; // Make PlayerPrefs save game to the web platform cloud.

public class PauseBeforeGame : MonoBehaviour // This class manages the pause before game starts.
{
    [SerializeField] private GameObject _pauseBeforeGame;

    private void OnEnable()
    {
        YG2.onGetSDKData += ShowPauseText;

        if (YG2.saves != null)
        {
            ShowPauseText();
        }
    }
    private void OnDisable() => YG2.onGetSDKData -= ShowPauseText;


    /// <summary>
    /// Set pause if player opened save with turned off tutorial menu.
    /// </summary>
    public void ShowPauseText()
    {
        if (PlayerPrefs.GetInt("Tutorial") == 1 && PlayerPrefs.GetInt("NewGame") == 1)
        {
            _pauseBeforeGame.SetActive(false);
        }
        else
        {
            _pauseBeforeGame.SetActive(true);

            PauseLogic.ChangePauseState(true);
            Time.timeScale = 0;
        }
    }

    /// <summary>
    /// Unpauses game.
    /// </summary>
    public void ContinueGame()
    {
        _pauseBeforeGame.SetActive(false);

        Time.timeScale = 1;
        PauseLogic.ChangePauseState(false);
    }
}
