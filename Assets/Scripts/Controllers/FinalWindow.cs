using PlayerPrefs = RedefineYG.PlayerPrefs; // Make PlayerPrefs save game to the web platform cloud.
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class FinalWindow : MonoBehaviour // This class shws final menu after player finished this game.
{
    private void Start()
    {
        YG2.ReviewShow();
    }

    /// <summary>
    /// Sends player to the main menu.
    /// </summary>
    public void BackToMenu()
    {
        PauseLogic.ChangePauseState(false);
        ShowAdds();
        PlayerPrefs.SetInt("NewGame", 1);
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Shows adds (plugin YG2 is used).
    /// </summary>
    public void ShowAdds()
    {
        YG2.InterstitialAdvShow();
    }
}
