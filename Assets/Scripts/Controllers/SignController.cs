using TMPro;
using UnityEngine;
using YG;

public class SingController : MonoBehaviour // This class manages the sign interactions.
{
    [SerializeField] private float _distanseToShowText;

    [SerializeField] private  GameObject _canvasForText;
    [SerializeField] private  GameObject _signMenu;

    [SerializeField] private TextMeshProUGUI _text;

    private PlayerController _player;


    private void Start()
    {
        _player = PlayerController.playerInstance;
    }

    private void Update()
    {
        if (_player != null)
        {
            if (_distanseToShowText >= Mathf.Abs(_player.transform.position.x - transform.position.x) && _distanseToShowText >= Mathf.Abs(_player.transform.position.y - transform.position.y))
            {
                _canvasForText.SetActive(true);
                if (!_player.IsMobile)
                {
                    if (Input.GetKeyDown(KeyCode.E) && !PauseLogic.IsPause)
                    {
                        Time.timeScale = 0;
                        _signMenu.SetActive(true);
                    }
                }
                else
                {
                    if (_player.IsEButtonPressed && !PauseLogic.IsPause)
                    {
                        Time.timeScale = 0; 
                        _signMenu.SetActive(true);
                        _player.OnEButtonSates(false);
                    }
                }
            }

            else
            {
                _signMenu.SetActive(false);
            }
        }
        
    }

    /// <summary>
    /// Runs all game processes again.
    /// </summary>
    public void ContinueGame()
    {
        Time.timeScale = 1;
    }


    /// <summary>
    /// Shows adds (Used plugin Youre Games 2).
    /// </summary>
    public void ShowAdds()
    {
        YG2.InterstitialAdvShow();
    }
}
