using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class MagicStoneController : MonoBehaviour // This class manages the magic stone.
{
    [SerializeField] private float _distanseToShowText;
    [SerializeField] private GameObject _canvasForText;

    [SerializeField] GameSaver _gameSaver;

    private PlayerController _player;

    private MagicStoneLogic _magicStoneLogic;

    [HideInInspector]
    public MagicStoneLogic MagicStoneLogic => _magicStoneLogic;


    [SerializeField] Transform _teleportPosition;

    [SerializeField] TextMeshProUGUI _textAboveStone;

    [SerializeField] string _purpose;

    [SerializeField] bool _isSecrete;

    [SerializeField] private int _id;
    public int ID => _id;

    private void Start()
    {
        _player = PlayerController.playerInstance;

        _magicStoneLogic = new MagicStoneLogic();
    }

    private void Update()
    {
        if (_player != null)
        {
            if (_distanseToShowText >= Mathf.Abs(_player.transform.position.x - transform.position.x) && _distanseToShowText >= Mathf.Abs(_player.transform.position.y - transform.position.y))
            {
                _canvasForText.SetActive(true);
                if (!_isSecrete)
                {
                    if (YG2.lang == "ru")
                    {
                        _textAboveStone.text = "Нажмите Е";
                    }
                    else if (YG2.lang == "en")
                    {
                        _textAboveStone.text = "Press E";
                    }
                    if (!_player.IsMobile)
                    {
                        if (Input.GetKeyDown(KeyCode.E) && !PauseLogic.IsPause)
                        {
                            if (_purpose == "Teleport" && _teleportPosition != null)
                            {
                                Teleport();
                            }
                            else if (_purpose == "End")
                            {
                                TheEnd();
                            }
                        }
                    }
                    else
                    {
                        if (_player.IsEButtonPressed && !PauseLogic.IsPause)
                        {
                            if (_purpose == "Teleport" && _teleportPosition != null)
                            {
                                Teleport();
                            }
                            else if (_purpose == "End")
                            {
                                TheEnd();
                            }
                            _player.OnEButtonSates(false);
                        }
                    }
                }
                else
                {
                    if (_magicStoneLogic.IsFound == 0)
                    {
                        if (YG2.lang == "ru")
                        {
                            _textAboveStone.text = "Портал неактивирован";
                        }
                        else if (YG2.lang == "en")
                        {
                            _textAboveStone.text = "Portal isn't activated";
                        }
                    }
                    else if (_magicStoneLogic.IsFound == 1)
                    {
                        if (YG2.lang == "ru")
                        {
                            _textAboveStone.text = "Нажмите Е";
                        }
                        else if (YG2.lang == "en")
                        {
                            _textAboveStone.text = "Press E";
                        }
                        if (!_player.IsMobile)
                        {
                            if (Input.GetKeyDown(KeyCode.E))
                            {
                                if (_purpose == "Teleport" && _teleportPosition != null && !PauseLogic.IsPause)
                                {
                                    Teleport();
                                }
                                else if (_purpose == "End")
                                {
                                    TheEnd();
                                }
                            }
                        }
                        else
                        {
                            if (_player.IsEButtonPressed)
                            {
                                if (_purpose == "Teleport" && _teleportPosition != null && !PauseLogic.IsPause)
                                {
                                    Teleport();
                                }
                                else if (_purpose == "End")
                                {
                                    TheEnd();
                                }
                                _player.OnEButtonSates(false);
                            }
                        }
                    }
                }
            }
            else
            {
                _canvasForText.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Sends player to set position.
    /// </summary>
    public void Teleport()
    {
        _player.transform.position = _teleportPosition.transform.position;

        // Saves
        _gameSaver.SaveMagicStone(this);
        _gameSaver.GlobalSave();
    }

    /// <summary>
    /// Loads final scene.
    /// </summary>
    public void TheEnd()
    {
        SceneManager.LoadScene("TheEnd");
    }
}