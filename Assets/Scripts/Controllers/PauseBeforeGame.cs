using UnityEngine;
using YG;
using PlayerPrefs = RedefineYG.PlayerPrefs;

public class PauseBeforeGame : MonoBehaviour
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

    public void ContinueGame()
    {
        _pauseBeforeGame.SetActive(false);

        Time.timeScale = 1;
        PauseLogic.ChangePauseState(false);
    }
}
