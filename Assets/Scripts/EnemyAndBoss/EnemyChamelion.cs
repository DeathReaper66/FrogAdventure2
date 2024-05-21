using UnityEngine;

public class EnemyChamelion : MonoBehaviour
{
    [Header("LocalScale")]
    [SerializeField] private float _x = 7;
    [SerializeField] private float _y = 7;
    [SerializeField] private float _z = 7;
    [Header("Params")]
    [SerializeField] private float _speed;
    [SerializeField] private float _damage = 1f;
    [SerializeField] private float _attackCooldown = 3f;
    private float _attackTimer = 0f;
    [SerializeField] private float _distanceForVison = 1f;
    [SerializeField] private float _distanceForAttack = 1f;
    [SerializeField] private float _distanceForBack = 1f;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private BoxCollider2D _box;

    private EnemyHealth _chamelionHealth;
    private Animator _anim;
    private bool _canRun = false;
    private RaycastHit2D _hit;
    private RaycastHit2D _hit1;
    private RaycastHit2D _hit2;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _box = GetComponent<BoxCollider2D>();
        _chamelionHealth = GetComponent<EnemyHealth>();
    }

    private void FixedUpdate()
    {
        _attackTimer += Time.deltaTime;

        if (PlayerInAttackSight())
        {
            _canRun = false;

            if (_attackTimer >= _attackCooldown && _chamelionHealth._health > 0)
            {
                _anim.SetTrigger("Attack");
                _attackTimer = 0f;
            }
        }
        else
            _canRun = true;

        if (PlayerInSight() && _canRun)
            transform.Translate(Mathf.Sign(transform.localScale.x) * Vector3.left * _speed / 50f);

        if (PlayerInBackSight())
        {
            if (transform.localScale.x == _x)
                transform.position = new Vector2(transform.position.x + 3f, transform.position.y);
            else
                transform.position = new Vector2(transform.position.x - 3f, transform.position.y);

            transform.localScale = new Vector3(-transform.localScale.x, _y, _z);
        }

        _anim.SetBool("Running", PlayerInSight() && _canRun);
    }

    public void Attack()
    {
        if (PlayerInAttackSight())
            _hit1.collider.GetComponent<HealthSystem>().TakeDamage(_damage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            collision.GetComponent<HealthSystem>().TakeDamage(_damage);
    }

    private bool PlayerInSight()
    {
        _hit = Physics2D.BoxCast(_box.bounds.center - transform.right * _distanceForVison * Mathf.Sign(transform.localScale.x),
        new Vector3(_box.bounds.size.x * _distanceForVison, _box.bounds.size.y, _box.bounds.size.z), 0, Vector2.left, 0f, _playerLayer);
        return _hit.collider != null;
    }

    private bool PlayerInAttackSight()
    {
        _hit1 = Physics2D.BoxCast(_box.bounds.center - transform.right * _distanceForAttack * Mathf.Sign(transform.localScale.x),
        new Vector3(_box.bounds.size.x * _distanceForAttack, _box.bounds.size.y / 2f, _box.bounds.size.z), 0, Vector2.left, 0f, _playerLayer);
        return _hit1.collider != null;
    }

    private bool PlayerInBackSight()
    {
        _hit2 = Physics2D.BoxCast(_box.bounds.center + transform.right * _distanceForBack * Mathf.Sign(transform.localScale.x),
        new Vector3(_box.bounds.size.x * _distanceForBack, _box.bounds.size.y / 2f, _box.bounds.size.z), 0, Vector2.left, 0f, _playerLayer);
        return _hit2.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_box.bounds.center - transform.right * _distanceForVison * Mathf.Sign(transform.localScale.x),
        new Vector3(_box.bounds.size.x * _distanceForVison, _box.bounds.size.y, _box.bounds.size.z));

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(_box.bounds.center - transform.right * _distanceForAttack * Mathf.Sign(transform.localScale.x),
        new Vector3(_box.bounds.size.x * _distanceForAttack, _box.bounds.size.y / 2f, _box.bounds.size.z));

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(_box.bounds.center + transform.right * _distanceForBack * Mathf.Sign(transform.localScale.x),
        new Vector3(_box.bounds.size.x * _distanceForBack, _box.bounds.size.y / 2f, _box.bounds.size.z));
    }
}