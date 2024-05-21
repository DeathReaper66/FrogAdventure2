using UnityEngine;

public class Duck : MonoBehaviour
{
    [Header("LocalScale")]
    [SerializeField] private float _x = 7;
    [SerializeField] private float _y = 7;
    [SerializeField] private float _z = 7;
    [Header("Params")]
    [SerializeField] private Transform _leftTarget;
    [SerializeField] private Transform _rightTarget;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _damage = 1f;
    [SerializeField] private float _jumpCooldown = 3f;
    private float _jumpTimer = 0f;
    [SerializeField] private LayerMask _playerLayer;
    private bool _leftMove = true;
    private Animator _anim;
    [SerializeField] private BoxCollider2D _enemyBox;
    private Rigidbody2D _enemyRb;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _enemyBox = GetComponent<BoxCollider2D>();
        _enemyRb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (isDead())
        {
            _anim.SetTrigger("Die");
            _enemyBox.enabled = false;
        }
        else
            Move();
    }

    private void Move()
    {
        _jumpTimer += Time.deltaTime;

        if (_leftMove)
        {
            if (transform.position.x > _leftTarget.position.x && _jumpTimer > _jumpCooldown)
            {
                _jumpTimer = 0f;
                _anim.SetTrigger("Attack");
                gameObject.transform.localScale = new Vector3(_x, _y, _z);
                _enemyRb.velocity = new Vector2(_jumpForce / 2 * -Mathf.Sign(transform.localScale.x), _jumpForce);
            }
            else if (transform.position.x < _leftTarget.position.x)
                _leftMove = false;
        }
        else if (!_leftMove)
        {
            if (transform.position.x < _rightTarget.position.x && _jumpTimer > _jumpCooldown)
            {
                _jumpTimer = 0f;
                _anim.SetTrigger("Attack");
                gameObject.transform.localScale = new Vector3(-_x, _y, _z);
                _enemyRb.velocity = new Vector2(_jumpForce / 2 * -Mathf.Sign(transform.localScale.x), _jumpForce);
            }
            else if (transform.position.x > _rightTarget.position.x)
                _leftMove = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            collision.GetComponent<HealthSystem>().TakeDamage(_damage);
    }
    private bool isDead()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(_enemyBox.bounds.center + Vector3.up, _enemyBox.bounds.size / 3, 0, Vector2.up, 0.01f, _playerLayer);
        return raycastHit2D.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_enemyBox.bounds.center + Vector3.up, _enemyBox.bounds.size / 3);
    }

    public void Destroy()
    {
        gameObject.SetActive(false);
    }
}
