using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class PauseMenu : MonoBehaviour 
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

    public void ContinueGame()
    {
        _pauseMenu.SetActive(false);

        Time.timeScale = 1;
        PauseLogic.ChangePauseState(false);
    }

    public void PauseGame()
    {
        _gameSaver.GlobalSave();
        IsShowSafeCorotineActive();

        _pauseMenu.SetActive(true);

        Time.timeScale = 0;
        PauseLogic.ChangePauseState(true);

        ShowAdds();
    }

    public void BackToMenu()
    {
        ShowAdds();

        _pauseMenu.SetActive(false);

        PauseLogic.ChangePauseState(false);
        Time.timeScale = 1;

        SceneManager.LoadScene("MainMenu");
    }

    private IEnumerator ShowSave()
    {
        _showSaveCanvas.SetActive(true);

        yield return new WaitForSecondsRealtime(_showSaveTime);

        _showSaveCanvas.SetActive(false);
    }

    private void IsShowSafeCorotineActive()
    {
        if (_showSaveCoroutine != null)
        {
            StopCoroutine(_showSaveCoroutine);
        }
        _showSaveCoroutine = StartCoroutine(ShowSave());
    }

    public void ShowAdds()
    {
        YG2.InterstitialAdvShow();
    }
}
