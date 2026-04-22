using UnityEngine;

public class MagicFistController : MonoBehaviour // This class manages the magic fist.
{
    [SerializeField] private int _magicFistDeathTime;
    [SerializeField] private int _magicFistSpeed;
    [SerializeField] private int _magicFistDamage;

    private void Start()
    {
        Invoke(nameof(Death), _magicFistDeathTime);
    }

    private void Update()
    {
        transform.Translate(Vector3.right * _magicFistSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision.TryGetComponent(out PlayerController player))
        { 
            Death();
            collision.gameObject.GetComponent<PlayerController>().PlayerGetDamage(_magicFistDamage, transform.position, transform);
        }
    }

    /// <summary>
    /// Removes object
    /// </summary>
    private void Death()
    {
        Destroy(gameObject);
    }
}