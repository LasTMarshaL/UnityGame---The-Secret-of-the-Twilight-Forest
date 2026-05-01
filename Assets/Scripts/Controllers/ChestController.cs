using UnityEngine;

public class ChestController : MonoBehaviour
{
    [SerializeField] private int _coinsReward;

    [SerializeField] private GameObject _canvasForText;
    [SerializeField] private float _distanseToShowText;

    private PlayerController _player;

    [SerializeField] private GameSaver _gameSaver;

    [SerializeField] private int _id;
    public int ID => _id;

    private ChestLogic _chestLogic;

    [HideInInspector]
    public ChestLogic ChestLogic => _chestLogic;

    private void Start()
    {
        _chestLogic = new ChestLogic();

        _player = PlayerController.playerInstance;
    }

    private void Update()
    {
        if (_player != null)
        {
            if (_distanseToShowText >= Mathf.Abs(_player.transform.position.x - transform.position.x) && _distanseToShowText >= Mathf.Abs(_player.transform.position.y - transform.position.y) && _chestLogic.IsOpened != 1)
            {
                _canvasForText.SetActive(true);
                if (!_player.IsMobile)
                {
                    if (Input.GetKeyDown(KeyCode.E) && !PauseLogic.IsPause)
                    {
                        ChestWasUsed();
                    }
                }
                else
                {
                    if (_player.IsEButtonPressed && !PauseLogic.IsPause)
                    {
                        ChestWasUsed();

                        _player.OnEButtonSates(false);
                    }
                }
            }
            else
            {
                _canvasForText.SetActive(false);
            }
        }
    }

    private void ChestWasUsed()
    {
        _chestLogic.WasOpened();

        _gameSaver.SaveChest(this);
        _gameSaver.GlobalSave();

        _chestLogic.GetCoinsReward(_coinsReward, _player.Wallet);

        ChestDestroy();
    }


    public void ChestDestroy()
    {
        Destroy(gameObject);
    }
}


