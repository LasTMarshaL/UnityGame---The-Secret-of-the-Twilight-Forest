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
        //Debug.DrawRay((Vector2)transform.position + Vector2.up * 0.1f, Vector2.down * 0.1f, Color.red);
        _isGround = Physics2D.OverlapCircle((Vector2)transform.position, _groundCheckRadius, _groundLayer);

        enemyAttackTimer += Time.deltaTime;
        enemyMagicFistTimer += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        EnemyMove();
    }

    /// <summary>
    /// Handles enemy movement.
    /// </summary>
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

    /// <summary>
    /// Plays the enemy walking sound if the audio source is enabled. Used in animation event.
    /// </summary>
    private void EnemyWalkingSounds()
    {
        if (enemyAudioSource.enabled == true)
        {
            enemyAudioSource.Play();
        }
    }

    /// <summary>
    /// Performs an enemy attack if the attack timer has elapsed, the player is not alive, and the enemy is on the ground.
    /// </summary>
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

    /// <summary>
    /// Plays this audio effect. Used in animation event.
    /// </summary>
    private void EnemySwordAttackCrySound()
    {
        enemyAudioSource.PlayOneShot(_enemySwordAttackCrySound);
    }

    /// <summary>
    /// Plays this audio effect. Used in animation event.
    /// </summary>
    private void EnemyMagicFistAttackCrySound()
    {
        enemyAudioSource.PlayOneShot(_enemyMagicFistAttackCrySound);
    }

    /// <summary>
    /// Plays this audio effect. Used in animation event.
    /// </summary>
    private void EnemyDeathSound()
    {
        enemyAudioSource.PlayOneShot(_enemyDeathSound);
    }

    /// <summary>
    /// Processes the enemy attack by detecting players within range and applying damage. Usedin animation event.
    /// </summary>
    /// <remarks>Uses Physics2D.OverlapCircleAll to find players in the attack area and resets the attacktimer after processing.</remarks>
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

    /// <summary>
    /// Spawns a magic fist projectile at the enemy's spawn point, oriented toward the player.
    /// </summary>
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
    
    /// <summary>
    /// Applies damage to the enemy, triggers animations, handles death logic, rewards the player, and saves game state if the enemy is killed.
    /// </summary>
    /// <param name="damage">The amount of damage to apply to the enemy.</param>
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

    /// <summary>
    /// Plays this audio effect. Used in animation event.
    /// </summary>
    private void EnemyGetDamageSound()
    {
        enemyAudioSource.PlayOneShot(_enemyGetDamageSound);
    }

    /// <summary>
    /// Sets the aggressive state of the object.
    /// </summary>
    /// <param name="state">true to set the object as aggressive; otherwise, false.</param>
    public void ChangeAgressiveSate(bool state)
    {
        IsAgressive = state;
    }

    /// <summary>
    /// Disables the enemy and schedules its destruction after a delay.
    /// </summary>
    protected virtual void EnemyDie()
    {
        this.enabled = false;
        Invoke(nameof(EnemyDestroy), 1f);
    }

    /// <summary>
    /// Destroys the enemy GameObject.
    /// </summary>
    public void EnemyDestroy()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Sets the enemy attack timer to the specified value.
    /// </summary>
    /// <param name="attackTime">The value to assign to the enemy attack timer.</param>
    public void LoadAttackTimerData(float attackTime)
    {
        enemyAttackTimer = attackTime;
    }

    /// <summary>
    /// Sets the timer value for the enemy's Magic Fist ability.
    /// </summary>
    /// <param name="magicFistTime">The time value to assign to the Magic Fist timer.</param>
    public void LoadMagicFistTimerData(float magicFistTime)
    {
        enemyMagicFistTimer = magicFistTime;
    }
}
