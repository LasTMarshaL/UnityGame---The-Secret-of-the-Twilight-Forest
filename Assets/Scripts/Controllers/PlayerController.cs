using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using YG;

public class PlayerController : MonoBehaviour
{
    [Header("Initial stats")] 
    [SerializeField] private int _initialHealthLevel;
    [SerializeField] private int _initialDamageLevel;
    [SerializeField] private int _initialSpeedLevel;

    [SerializeField] private int _initialCoins;

    private PlayerStatsLogic _playerStatsLogic;
    private PlayerWalletLogic _playerWalletLogic;
    private PlayerHealthLogic _playerHealthLogic;

    [HideInInspector]
    public PlayerStatsLogic Stats => _playerStatsLogic;
    [HideInInspector]
    public PlayerWalletLogic Wallet => _playerWalletLogic;
    [HideInInspector]
    public PlayerHealthLogic Health => _playerHealthLogic;


    [Header("Phisical Components")]
    [SerializeField] public Rigidbody2D Rigidbody2D { get; private set; }
    [SerializeField] private float _jumpForce;


    [SerializeField] private float _localScaleX;
    [SerializeField] private float _localScaleY;
    [SerializeField] private float _localScaleZ;

    [HideInInspector]
    public float LocalScaleX => _localScaleX;
    [HideInInspector]
    public float LocalScaleY => _localScaleY;
    [HideInInspector]
    public float LocalScaleZ => _localScaleZ;


    [SerializeField] private float _knockBackForce;
    [SerializeField] private float _knockBackDuration;

    private bool _isGround = true;
    [SerializeField] private LayerMask _groundLayer;

    private bool _isLadder = false;
    [SerializeField] private LayerMask _ladderLayer;

    [SerializeField] private float _groundCheckRadius = 0.2f;

    private Vector2 _moveVelocity; 

    [Header("Attack")]
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackRange;

    [SerializeField] private LayerMask _enemyMasks; 

    [SerializeField] private float _timeBetweenAttack;
    public float AttackTimer { get; private set; } = 0;

    [Header("Block")] 
    private bool _isBlocking = false;

    [HideInInspector]
    public bool isBlocking
    {
        get { return _isBlocking; }
        set
        {
            _isBlocking = value;
            _animator.SetBool("IsBlocking", _isBlocking);
        }
    }

    [HideInInspector]
    public float BlockTimer { get; private set; } = 0;
    [SerializeField] private float _timeBetweenBlock;


    [Header("Save")] 
    [SerializeField] private GameSaver _gameSaver;
    [SerializeField] private GameObject _showSaveCanvas;
    [SerializeField] private int _showSaveTime;


    [Header("UI")] 
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private TextMeshProUGUI _сoinsCounter;

    [SerializeField] private Slider _healthIndicator;
    [SerializeField] private Slider _attackIndicator;
    [SerializeField] private Slider _blockIndicator;

    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private GameObject _mobileStuff;

    [HideInInspector]
    public bool IsMobile { get; private set; }

    private bool _isJumpButtonPressed = false;
    private bool _isAttackButtonPressed = false;
    private bool _isBlockButtonPressed = false;

    [HideInInspector]
    public bool IsEButtonPressed { get; private set; } 

    [SerializeField] private GameObject _deathMenu;

    [Header("Animations")] 
    [SerializeField] private Animator _animator;

    [Header("Audio")] 
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private AudioClip _stepSound;
    [SerializeField] private AudioClip _swordAttackSound;
    [SerializeField] private AudioClip _getDamageSound;
    [SerializeField] private AudioClip _jumpSound;
    [SerializeField] private AudioClip _blockedAttackSound;
    [SerializeField] private AudioClip _magicFistAttackSound;
    [SerializeField] private AudioClip _startBlockSound;

    [HideInInspector]
    public static PlayerController playerInstance; 

    public float AddsCoinsTimer { get; private set; }
    public float AddsHealthTimer { get; private set; }


    private void Awake()
    {
        _playerStatsLogic = new PlayerStatsLogic(_initialHealthLevel, _initialDamageLevel, _initialSpeedLevel);
        _playerWalletLogic = new PlayerWalletLogic(_initialCoins);
        _playerHealthLogic = new PlayerHealthLogic(_playerStatsLogic.Health, _playerStatsLogic.Health);

        Rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

        playerInstance = this;
    }

    private void Start() 
    {
        AttackTimer = _timeBetweenAttack;
        BlockTimer = _timeBetweenBlock;

        AddsCoinsTimer = 30;
        AddsHealthTimer = 30;

    
        if (YG2.envir.deviceType == "mobile") 
        {
            IsMobile = true;
            _mobileStuff.SetActive(true);
        }
        else if (YG2.envir.deviceType == "desktop") 
        {
            IsMobile = false;
            _mobileStuff.SetActive(false);
        }
    }

    private void Update()
    {
        _isGround = Physics2D.OverlapCircle(transform.position, _groundCheckRadius, _groundLayer);
        _isLadder = Physics2D.OverlapCircle((Vector2)transform.position + Vector2.up * 0.1f, _groundCheckRadius, _ladderLayer);
        Debug.DrawRay((Vector2)transform.position + Vector2.up * 0.1f, Vector2.down * 0.1f, Color.red);

        if (_isLadder)
        {
            Rigidbody2D.gravityScale = 0;
            _animator.SetBool("IsGround", false);
        }
        else
        {
            Rigidbody2D.gravityScale = 1.4f;
            _animator.SetBool("IsGround", _isGround);
        }

        AddsHealthTimer += Time.unscaledDeltaTime;
        AddsCoinsTimer += Time.unscaledDeltaTime;

        AttackTimer += Time.deltaTime;
        _attackIndicator.value = AttackTimer;

        BlockTimer += Time.deltaTime;
        _blockIndicator.value = BlockTimer;

        RefreshCoinsCounterText();

        if (Input.GetKeyDown(KeyCode.R) && !PauseLogic.IsPause && !IsMobile)
        {
            _gameSaver.GlobalSave();
            StartCoroutine(ShowSave());
        }

        PlayerJump();
        PlayerAttack();
        PlayerBlockStart();
        IndicatorPlayerHealth();
    }

    private void FixedUpdate() 
    {
        PlayerMove();
        PlayerClimb();
    }

    public void RefreshCoinsCounterText()
    {
        string prefix;

        switch (YG2.lang)
        {
            case "ru":
                prefix = "Монеты: ";
                break;
            case "en":
                prefix = "Coins: ";
                break;
            default:
                prefix = "Coins: "; 
                break;
        }

        _сoinsCounter.text = prefix + _playerWalletLogic.Coins.ToString();
    }

    private void PlayerMove()
    {
        if (!IsMobile)
        {
            _moveVelocity.x = Input.GetAxis("Horizontal");
            if (_moveVelocity.x < 0)
            {
                transform.localScale = new Vector3(-LocalScaleX, LocalScaleY, LocalScaleZ);
            }
            else if (_moveVelocity.x > 0)
            {
                transform.localScale = new Vector3(LocalScaleX, LocalScaleY, LocalScaleZ);
            }

            Rigidbody2D.linearVelocity = new Vector2(_moveVelocity.x * _playerStatsLogic.Speed * Time.deltaTime, Rigidbody2D.linearVelocity.y);

            if (_moveVelocity.x != 0 && _isGround)
            {
                _animator.SetBool("IsWalking", true);
            }
            else
            {
                _animator.SetBool("IsWalking", false);
            }
        }
        else
        {
            if (_joystick.Horizontal < 0)
            {
                transform.localScale = new Vector3(-LocalScaleX, LocalScaleY, LocalScaleZ);
            }
            else if (_joystick.Horizontal > 0)
            {
                transform.localScale = new Vector3(LocalScaleX, LocalScaleY, LocalScaleZ);
            }

            Rigidbody2D.linearVelocity = new Vector2(_joystick.Horizontal * _playerStatsLogic.Speed * Time.deltaTime, Rigidbody2D.linearVelocity.y);

            if (_joystick.Horizontal != 0 && _isGround)
            {
                _animator.SetBool("IsWalking", true);
            }
            else
            {
                _animator.SetBool("IsWalking", false);
            }
        }
    }

    private void PlayerJump()
    {
        if (_isGround && !_animator.GetBool("IsAttacking") && !_animator.GetBool("IsBlocking") && !_isLadder)
        {
            if (!IsMobile)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _audioSource.PlayOneShot(_jumpSound);
                    _animator.SetTrigger("Jump");

                    Rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
                }

            }
            else
            {
                if (_isJumpButtonPressed)
                {
                    _audioSource.PlayOneShot(_jumpSound);
                    _animator.SetTrigger("Jump");

                    Rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse); 

                    _isJumpButtonPressed = false;
                }
            }
        }
    }

    public void OnJumpButtonDown()
    {
        if (_isGround)
        {
            _isJumpButtonPressed = true;
        }
    }


    private void PlayerClimb()
    {
        if (_isLadder)
        {
            if (!IsMobile)
            {
                _moveVelocity.y = Input.GetAxis("Vertical");
                Rigidbody2D.linearVelocityY = _moveVelocity.y * _playerStatsLogic.Speed * Time.deltaTime;
            }
            else
            {
                Rigidbody2D.linearVelocityY = _joystick.Vertical * _playerStatsLogic.Speed * Time.deltaTime;
            }
        }
    }

    private void PlayerStepSounds()
    {
        _audioSource.PlayOneShot(_stepSound);
    }

    private void PlayerAttack()
    {
        if (!IsMobile)
        {
            if (Input.GetMouseButtonDown(0) && AttackTimer >= _timeBetweenAttack && _isGround && !isBlocking && !_isLadder && !EventSystem.current.IsPointerOverGameObject() && !PauseLogic.IsPause)
            {
                List<string> triggers = new List<string> { "Attack1", "Attack2", "Attack3" };
                System.Random rand = new System.Random();

                _animator.SetTrigger(triggers[rand.Next(triggers.Count)]);

                Collider2D[] hitedEnemyes = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _enemyMasks);

                foreach (Collider2D enemy in hitedEnemyes)
                {
                    enemy.GetComponent<EnemyController>().EnemyGetDamage(_playerStatsLogic.Damage);
                }

                AttackTimer = 0;
            }
        }
        else
        {
            if (_isAttackButtonPressed && AttackTimer >= _timeBetweenAttack && _isGround && !isBlocking && !_isLadder && !PauseLogic.IsPause)
            {
                List<string> triggers = new List<string> { "Attack1", "Attack2", "Attack3" };
                System.Random rand = new System.Random();

                _animator.SetTrigger(triggers[rand.Next(triggers.Count)]);

                Collider2D[] hitedEnemyes = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _enemyMasks); 

                foreach (Collider2D enemy in hitedEnemyes)
                {
                    enemy.GetComponent<EnemyController>().EnemyGetDamage(_playerStatsLogic.Damage);
                }

                AttackTimer = 0;
                _isAttackButtonPressed = false;
            }
        }
    }

    public void OnAttackButtonDown()
    {
        if (AttackTimer >= _timeBetweenAttack)
        {
            _isAttackButtonPressed = true;
        }
    }

    public void PlayerSwordAttackSound()
    {
        _audioSource.PlayOneShot(_swordAttackSound);
    }

    private void OnDrawGizmosSelected()
    {
        if (!_attackPoint)
        {
            return;
        }
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }

    protected void IndicatorPlayerHealth()
    {
        if (_playerHealthLogic.Health <= 0)
        {
            _healthIndicator.value = 0;
        }
        _healthIndicator.value = (float)_playerHealthLogic.Health / _playerStatsLogic.Health;

        if (_playerHealthLogic.Health < 0)
        {
            _healthText.text = "0";
        }
        else
        {
            _healthText.text = _playerHealthLogic.Health.ToString();
        }
    }

    public void PlayerGetDamage(int damage, Vector3 enemyPosition, Transform enemyTransform)
    {
        Vector2 knockbackDirection = (transform.position - enemyPosition).normalized;
        StartCoroutine(PlayerKnockBack(knockbackDirection));

        Vector2 directionToEnemy = (enemyTransform.position - transform.position).normalized;
        float dot = Vector2.Dot(transform.right * transform.localScale.x, directionToEnemy);

        if (dot > 0 && isBlocking)
        {
            _audioSource.PlayOneShot(_blockedAttackSound);
        }
        else
        {
            _audioSource.PlayOneShot(_magicFistAttackSound);
            _playerHealthLogic.TakeDamage(damage);

            _animator.SetTrigger("Hurt");

            if (!_playerHealthLogic.IsAlive)
            {
                PlayerDie();
            }
        }
    }

    private void PlayerGetDamageSound()
    {
        _audioSource.PlayOneShot(_getDamageSound);
    }

    private IEnumerator PlayerKnockBack(Vector2 direction)
    {
        float startTime = Time.time;

        while (Time.time < startTime + _knockBackDuration)
        {
            transform.position += (Vector3)(direction * _knockBackForce * Time.deltaTime);
            yield return null;
        }

    }

    private void PlayerDie()
    {
        _deathMenu.SetActive(true);
    }

    private void PlayerBlockStart()
    {
        if (!IsMobile)
        {
            if (Input.GetMouseButtonDown(1) && BlockTimer >= _timeBetweenBlock && _isGround && !EventSystem.current.IsPointerOverGameObject() && !PauseLogic.IsPause)
            {
                _audioSource.PlayOneShot(_startBlockSound);

                _isBlocking = true;
                BlockTimer = 0;

                _animator.SetTrigger("Block");
            }
        }
        else
        {
            if (_isBlockButtonPressed && BlockTimer >= _timeBetweenBlock && _isGround && !PauseLogic.IsPause)
            {
                _audioSource.PlayOneShot(_startBlockSound);

                _isBlocking = true;
                BlockTimer = 0;

                _animator.SetTrigger("Block");

                _isBlockButtonPressed = false;
            }
        }
    }

    public void OnBlockButtonDown()
    {
        if (BlockTimer >= _timeBetweenBlock)
        {
            _isBlockButtonPressed = true;
        }
    }

    private void PlayerBlockFinish()
    {
        isBlocking = false;
    }

    private IEnumerator ShowSave()
    {
        _showSaveCanvas.SetActive(true);
        yield return new WaitForSeconds(_showSaveTime); 
        _showSaveCanvas.SetActive(false);
    }

    public void OnEButtonSates(bool flag)
    {
        IsEButtonPressed = flag;
    }

    public void LoadAttackTimerData(float attackTime)
    {
        AttackTimer = attackTime;
    }

    public void LoadBlockTimerData(float blockTime)
    {
        BlockTimer = blockTime;
    }

    public void LoadAddsCoinsTimer(float addsCoinsTime)
    {
        AddsCoinsTimer = addsCoinsTime;
    }

    public void LoadHealthCoinsTimer(float addsHealthTime)
    {
        AddsHealthTimer = addsHealthTime;
    }
}
