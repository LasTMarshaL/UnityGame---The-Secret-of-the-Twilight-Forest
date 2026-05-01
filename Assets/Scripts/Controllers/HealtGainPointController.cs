using UnityEngine;

public class HealtGainPointController : MonoBehaviour
{

    [SerializeField] private int _healthReward;

    [SerializeField] private GameObject _canvasForText;
    [SerializeField] private float _distanseToShowText;

    private PlayerController _player;

    [SerializeField] private GameSaver _gameSaver;

    [SerializeField] private int _id;
    public int ID => _id;

    private HealthGainPointLogic _healthGainPointLogic;

    [HideInInspector]
    public HealthGainPointLogic HealthGainPointLogic => _healthGainPointLogic;

    private void Start()
    {
        _player = PlayerController.playerInstance;

        _healthGainPointLogic = new HealthGainPointLogic();
    }

    private void Update()
    {
        if (_player != null)
        {
            if (_distanseToShowText >= Mathf.Abs(_player.transform.position.x - transform.position.x) && _distanseToShowText >= Mathf.Abs(_player.transform.position.y - transform.position.y) && _healthGainPointLogic.IsUsed == 0)
            {
                _canvasForText.SetActive(true);
                if (!_player.IsMobile)
                {
                    if (Input.GetKeyDown(KeyCode.E) && !PauseLogic.IsPause)
                    {
                        HealthGainPointWasUsed();
                    }
                }
                else
                {
                    if (_player.IsEButtonPressed && !PauseLogic.IsPause)
                    {
                        HealthGainPointWasUsed();

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

    private void HealthGainPointWasUsed()
    {
        _healthGainPointLogic.WasUsed();

        _gameSaver.SaveHealthGainPoint(this);
        _gameSaver.GlobalSave();

        _healthGainPointLogic.GetHealthReward(_healthReward, _player.Health);

        HealthGainPointDestroy();
    }


    public void HealthGainPointDestroy()
    {
        Destroy(gameObject);
    }
}
