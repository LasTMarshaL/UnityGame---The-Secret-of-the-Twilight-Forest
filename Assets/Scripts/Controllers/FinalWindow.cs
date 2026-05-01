using PlayerPrefs = RedefineYG.PlayerPrefs; 
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class FinalWindow : MonoBehaviour 
{
    private void Start()
    {
        YG2.ReviewShow();
    }

    public void BackToMenu()
    {
        PauseLogic.ChangePauseState(false);
        ShowAdds();
        PlayerPrefs.SetInt("NewGame", 1);
        SceneManager.LoadScene("MainMenu");
    }

    public void ShowAdds()
    {
        YG2.InterstitialAdvShow();
    }
}
