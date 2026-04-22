using TMPro;
using UnityEngine;
using YG;

public class SecretEnemyController : EnemyController // This class manages the secret enemy.
{
    [SerializeField] private GameObject _dialogCanvas;
    [SerializeField] private GameObject _indicatorsCanvas;

    [SerializeField] protected TextMeshProUGUI dialogEnemyPhrase;

    [SerializeField] protected string[] russianPhrases;
    [SerializeField] protected string[] englishPhrases;

    private int _phraseCounterRussian = 1;
    private int _phraseCounterEnglish = 1;

    protected override void Awake()
    {
        base.Awake();
        ChangeAgressiveSate(false);
    }
    protected override void Start()
    {
        if (YG2.lang == "ru")
        {
            dialogEnemyPhrase.text = russianPhrases[0];
        }
        else if (YG2.lang == "en")
        {
            dialogEnemyPhrase.text = englishPhrases[0];
        }

        base.Start();
    }

    protected override void Update()
    {
        if (!IsAgressive && enemyDefinePlayerDistance >= Mathf.Abs(player.transform.position.x - transform.position.x) && (enemyDefinePlayerDistance >= Mathf.Abs(player.transform.position.y - transform.position.y)))
        {
            Dialog();
        }

        base.Update();
    }

    /// <summary>
    /// Handles the dialog sequence with the enemy, updating UI elements, managing game pause state, and displaying
    /// dialog phrases based on the current language and input method.
    /// </summary>
    protected virtual void Dialog()
    {
        PauseLogic.ChangePauseState(true);
        Time.timeScale = 0;

        _dialogCanvas.SetActive(true);

        if (YG2.lang == "ru")
        {
            if(!player.IsMobile)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (_phraseCounterRussian < russianPhrases.Length)
                    {
                        dialogEnemyPhrase.text = russianPhrases[_phraseCounterRussian];
                        _phraseCounterRussian++;
                    }
                    else
                    {
                        ChangeAgressiveSate(true);

                        _dialogCanvas.SetActive(false);

                        PauseLogic.ChangePauseState(false);
                        Time.timeScale = 1;
                    }
                }
            }
            else
            {

                if (player.IsEButtonPressed)
                {
                    if (_phraseCounterRussian < russianPhrases.Length)
                    {
                        dialogEnemyPhrase.text = russianPhrases[_phraseCounterRussian];
                        _phraseCounterRussian++;

                        player.OnEButtonSates(false);
                    }
                    else
                    {
                        ChangeAgressiveSate(true);

                        _dialogCanvas.SetActive(false);

                        player.OnEButtonSates(false);

                        PauseLogic.ChangePauseState(false);
                        Time.timeScale = 1;
                    }
                }
            }
            
        }
        else if (YG2.lang == "en")
        {
            if (!player.IsMobile)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (_phraseCounterEnglish < englishPhrases.Length)
                    {
                        dialogEnemyPhrase.text = englishPhrases[_phraseCounterEnglish];
                        _phraseCounterEnglish++;
                    }
                    else
                    {
                        ChangeAgressiveSate(true);

                        _dialogCanvas.SetActive(false);

                        PauseLogic.ChangePauseState(false);
                        Time.timeScale = 1;
                    }
                }
            }
            else
            {
                if (player.IsEButtonPressed)
                {
                    if (_phraseCounterEnglish < englishPhrases.Length)
                    {
                        dialogEnemyPhrase.text = englishPhrases[_phraseCounterEnglish];
                        _phraseCounterEnglish++;

                        player.OnEButtonSates(false);
                    }
                    else
                    {
                        ChangeAgressiveSate(true);

                        _dialogCanvas.SetActive(false);

                        player.OnEButtonSates(false);

                        PauseLogic.ChangePauseState(false);
                        Time.timeScale = 1;
                    }
                }
            }
        }
    }
}
