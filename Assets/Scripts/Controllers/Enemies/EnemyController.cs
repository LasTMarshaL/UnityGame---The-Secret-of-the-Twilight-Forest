using UnityEngine;

public class EnemyController : MonoBehaviour // This class manages the enemy's behavior.
{
    [SerializeField] private Rigidbody2D _enemyRididBody2D;

    [SerializeField] private int _enemyWalkingSpeed;
    [SerializeField] private int _enemyRunningSpeed;
    [SerializeField] private float _distanceOfHearing;

    [HideInInspector]
    public Vector3 enemyPosition => transform.position;

    [SerializeField] private Transform _enemyPlace;

    private bool _isGround = true;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _groundCheckRadius = 0.2f;

    [SerializeField] private float _localScaleX;
    [SerializeField] private float _localScaleY;
    [SerializeField] private float _localScaleZ;

    [SerializeField] private Animator _enemyAnimator;

    [SerializeField] protected float enemyDistanceBetweenPlayer;
    [SerializeField] protected float enemyDefinePlayerDistance;

    [SerializeField] private float _enemyAttackRange;
    [SerializeField] private Transform _enemyAttackPoint;
    [SerializeField] private LayerMask _playerMasks;

    [HideInInspector]
    public bool IsAgressive { get; private set; } = true;

    [SerializeField] private Transform _enemySpawnMagicFistPoint;
    [SerializeField] private GameObject _magicFist;

    [SerializeField] private float enemyTimeBetweenAttack;
    [SerializeField] private float enemyTimeBetweenMagicFist;

    [HideInInspector]
    public float enemyAttackTimer { get; private set; } = 0;
    [HideInInspector]
    public float enemyMagicFistTimer { get; private set; } = 0;

    [SerializeField] private int _enemyDamage;

    private EnemyHealthLogic _enemyHealthLogic;
    public EnemyHealthLogic EnemyHealth => _enemyHealthLogic;

    [SerializeField] private int _enemyMaxHealth;
    [SerializeField] private int _enemyCurrentHealth;

    protected PlayerController player;

    [HideInInspector]
    public bool playerIsAttacked { get; private set; } = false;

    [SerializeField] protected AudioSource enemyAudioSource;

    [SerializeField] private AudioClip _enemyGetDamageSound;
    [SerializeField] private AudioClip _enemyAttackSound;
    [SerializeField] private AudioClip _playerBlockedAttackSound;
    [SerializeField] private AudioClip _enemySwordAttackCrySound;
    [SerializeField] private AudioClip _enemyMagicFistAttackCrySound;
    [SerializeField] private AudioClip _enemyDeathSound;


    [SerializeField] private int _coinsFromEnemy;

    [SerializeField] private int _id;
    public int ID => _id;

    [SerializeField] protected GameSaver gameSaver;

    protected virtual void Awake()
    {
        _enemyHealthLogic = new EnemyHealthLogic(_enemyCurrentHealth, _enemyMaxHealth);
    }

    protected virtual void Start()
    {
        player = PlayerController.playerInstance;

        _enemyAnimator = GetComponent<Animator>();
        enemyAudioSource = GetComponent<AudioSource>();

        enemyAttackTimer = enemyTimeBetweenAttack;
        enemyMagicFistTimer = enemyTimeBetweenMagicFist;
    }
    protected virtual void Update()
    {
        _isGround = Physics2D.OverlapCircle((Vector2)transform.position, _groundCheckRadius, _groundLayer);

        enemyAttackTimer += Time.deltaTime;
        enemyMagicFistTimer += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        EnemyMove();
    }

    private void EnemyMove()
    {
        if (player != null)
        {
            if (_enemyHealthLogic.IsKilled == 0)
            {

                if (_distanceOfHearing > Mathf.Abs(player.transform.position.x - transform.position.x) && (_distanceOfHearing > Mathf.Abs(player.transform.position.y - transform.position.y)))
                {
                    enemyAudioSource.enabled = true;
                }
                else
                {
                    enemyAudioSource.enabled = false;
                }
                if (IsAgressive)
                {
                    if (enemyDefinePlayerDistance >= Mathf.Abs(player.transform.position.x - transform.position.x) && (enemyDefinePlayerDistance >= Mathf.Abs(player.transform.position.y - transform.position.y) && enemyDistanceBetweenPlayer < Mathf.Abs(player.transform.position.x - transform.position.x)))
                    {
                        if (enemyMagicFistTimer >= enemyTimeBetweenMagicFist && player.Health.IsAlive && _isGround)
                        {
                            _enemyRididBody2D.linearVelocity = new Vector2(0, _enemyRididBody2D.linearVelocity.y);
                            _enemyAnimator.SetBool("IsRunning", false);
                            _enemyAnimator.SetBool("IsWalking", false);
                            _enemyAnimator.SetTrigger("Attack1");
                            enemyMagicFistTimer = 0;
                        }
                        else
                        {
                            _enemyAnimator.SetBool("IsRunning", true);
                            if (player.transform.position.x > transform.position.x)
                            {
                                transform.localScale = new Vector3(_localScaleX, _localScaleY, _localScaleY);
                                _enemyRididBody2D.linearVelocity = new Vector2(_enemyRunningSpeed * Time.deltaTime, _enemyRididBody2D.linearVelocity.y);
                            }
                            else if (player.transform.position.x < transform.position.x)
                            {
                                transform.localScale = new Vector3(-_localScaleX, _localScaleY, _localScaleY);
                                _enemyRididBody2D.linearVelocity = new Vector2(-_enemyRunningSpeed * Time.deltaTime, _enemyRididBody2D.linearVelocity.y);
                            }
                            else
                            {
                                _enemyAnimator.SetBool("IsRunning", false);
                            }
                        }

                    }
                    else if (enemyDefinePlayerDistance >= Mathf.Abs(player.transform.position.x - transform.position.x) && enemyDistanceBetweenPlayer >= Mathf.Abs(player.transform.position.x - transform.position.x) && (enemyDefinePlayerDistance >= Mathf.Abs(player.transform.position.y - transform.position.y)))
                    {
                        _enemyRididBody2D.linearVelocity = new Vector2(0, _enemyRididBody2D.linearVelocity.y);
                        _enemyAnimator.SetBool("IsRunning", false);
                        _enemyAnimator.SetBool("IsWalking", false);
                        EnemyAttack();
                    }
                    else
                    {
                        _enemyAnimator.SetBool("IsRunning", false);
                        _enemyAnimator.SetBool("IsWalking", true);
                        if (_enemyPlace.transform.position.x > transform.position.x + 1)
                        {
                            transform.localScale = new Vector3(_localScaleX, _localScaleY, _localScaleY);
                            _enemyRididBody2D.linearVelocity = new Vector2(_enemyWalkingSpeed * Time.deltaTime, _enemyRididBody2D.linearVelocity.y);
                        }
                        else if (_enemyPlace.transform.position.x < transform.position.x - 1)
                        {
                            transform.localScale = new Vector3(-_localScaleX, _localScaleY, _localScaleZ);
                            _enemyRididBody2D.linearVelocity = new Vector2(-_enemyWalkingSpeed * Time.deltaTime, _enemyRididBody2D.linearVelocity.y);
                        }
                        else
                        {
                            _enemyRididBody2D.linearVelocity = new Vector2(0, _enemyRididBody2D.linearVelocity.y);
                            _enemyAnimator.SetBool("IsWalking", false);
                        }
                    }
                }
            }
        }
        
    }

    private void EnemyWalkingSounds()
    {
        if (enemyAudioSource.enabled == true)
        {
            enemyAudioSource.Play();
        }
    }

    private void EnemyAttack()
    {
        if (enemyAttackTimer >= enemyTimeBetweenAttack && player.Health.IsAlive && _isGround)
        {
            if (player.transform.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(_localScaleX, _localScaleY, _localScaleY);
            }
            else if (player.transform.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(-_localScaleX, _localScaleY, _localScaleY);
            }
            _enemyAnimator.SetTrigger("Attack0");
            enemyAttackTimer = 0;
        }
    }

    private void EnemySwordAttackCrySound()
    {
        enemyAudioSource.PlayOneShot(_enemySwordAttackCrySound);
    }


    private void EnemyMagicFistAttackCrySound()
    {
        enemyAudioSource.PlayOneShot(_enemyMagicFistAttackCrySound);
    }


    private void EnemyDeathSound()
    {
        enemyAudioSource.PlayOneShot(_enemyDeathSound);
    }


    private void EnemyAttackProcess()
    {
        Collider2D[] hitedPlayers = Physics2D.OverlapCircleAll(_enemyAttackPoint.position, _enemyAttackRange, _playerMasks);

        foreach (Collider2D player in hitedPlayers)
        {
            player.GetComponent<PlayerController>().PlayerGetDamage(_enemyDamage, enemyPosition, transform);
            playerIsAttacked = true;
        }

        playerIsAttacked = false;
        enemyAttackTimer = 0;
    }

    protected virtual void EnemyMagicFistSpawnProcess()
    {
        if (!player) return;

        Vector2 enemyDirection = player.transform.position - transform.position;
        float angle = Mathf.Atan2(enemyDirection.y - 1f, enemyDirection.x) * Mathf.Rad2Deg;
        _enemySpawnMagicFistPoint.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        Instantiate(_magicFist, _enemySpawnMagicFistPoint.position, _enemySpawnMagicFistPoint.rotation);

    }

    private void OnDrawGizmosSelected()
    {
        if (!_enemyAttackPoint)
        {
            return;
        }
        Gizmos.DrawWireSphere(_enemyAttackPoint.position, _enemyAttackRange);
    }
    

    public void EnemyGetDamage(int damage)
    {
       _enemyHealthLogic.TakeDamage(damage);

        if (_enemyHealthLogic.IsKilled == 0)
        {
            if (_enemyHealthLogic.Health > 0)
            {
                _enemyAnimator.SetTrigger("GetDamage");
            }
            else
            {
                _enemyHealthLogic.WasKilled();

                _enemyAnimator.SetBool("Death", true);

                player.Wallet.GetCoins(_coinsFromEnemy);

                gameSaver.SaveGameEnemy(this);
                gameSaver.GlobalSave();

                Invoke(nameof(EnemyDie), 2f);
            }
        }
    }


    private void EnemyGetDamageSound()
    {
        enemyAudioSource.PlayOneShot(_enemyGetDamageSound);
    }


    public void ChangeAgressiveSate(bool state)
    {
        IsAgressive = state;
    }


    protected virtual void EnemyDie()
    {
        this.enabled = false;
        Invoke(nameof(EnemyDestroy), 1f);
    }

    public void EnemyDestroy()
    {
        Destroy(gameObject);
    }


    public void LoadAttackTimerData(float attackTime)
    {
        enemyAttackTimer = attackTime;
    }


    public void LoadMagicFistTimerData(float magicFistTime)
    {
        enemyMagicFistTimer = magicFistTime;
    }
}
