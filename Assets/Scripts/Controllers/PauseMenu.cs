using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class PauseMenu : MonoBehaviour // This class manages the pause.
{
    [SerializeField] private GameObject _pauseMenu;

    [SerializeField] private GameSaver _gameSaver;

    [SerializeField] private Button _pauseButton;

    [SerializeField] private GameObject _showSaveCanvas;

    [SerializeField] private float _showSaveTime;

    private Coroutine _showSaveCoroutine;

    private void Update()
    {
        if (PauseLogic.IsPause)
        {
            _pauseButton.interactable = false;
        }
        else
        {
            _pauseButton.interactable = true;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            _gameSaver.GlobalSave();
            IsShowSafeCorotineActive();
        }
    }

    /// <summary>
    /// Unpauses game.
    /// </summary
    public void ContinueGame()
    {
        _pauseMenu.SetActive(false);

        Time.timeScale = 1;
        PauseLogic.ChangePauseState(false);
    }

    /// <summary>
    /// Pauses game and shows adds.
    /// </summary>
    public void PauseGame()
    {
        _gameSaver.GlobalSave();
        IsShowSafeCorotineActive();

        _pauseMenu.SetActive(true);

        Time.timeScale = 0;
        PauseLogic.ChangePauseState(true);

        ShowAdds();
    }


    /// <summary>
    /// Sends player to the main menu.
    /// </summary>
    public void BackToMenu()
    {
        ShowAdds();

        _pauseMenu.SetActive(false);

        PauseLogic.ChangePauseState(false);
        Time.timeScale = 1;

        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Shows text, that game was saved.
    /// </summary>
    private IEnumerator ShowSave()
    {
        _showSaveCanvas.SetActive(true);

        yield return new WaitForSecondsRealtime(_showSaveTime);

        _showSaveCanvas.SetActive(false);
    }

    /// <summary>
    /// Checks if the coroutine that shows text about saving is active, and if it is, stops it and starts again.
    /// </summary>
    private void IsShowSafeCorotineActive()
    {
        if (_showSaveCoroutine != null)
        {
            StopCoroutine(_showSaveCoroutine);
        }
        _showSaveCoroutine = StartCoroutine(ShowSave());
    }

    /// <summary>
    /// Shows add (Used plugin Youre Games 2).
    /// </summary>
    public void ShowAdds()
    {
        YG2.InterstitialAdvShow();
    }
}
