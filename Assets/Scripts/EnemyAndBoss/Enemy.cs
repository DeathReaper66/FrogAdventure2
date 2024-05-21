using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("LocalScale")]
    [SerializeField] private float _x = 7;
    [SerializeField] private float _y = 7;
    [SerializeField] private float _z = 7;
    [Header("Params")]
    [SerializeField] private Transform _leftTarget;
    [SerializeField] private Transform _rightTarget;
    [SerializeField] private float _speed;
    [SerializeField] private float _damage = 1f;
    [SerializeField] private LayerMask _playerLayer;
    private bool _leftMove = true;
    private Animator _anim;
    [SerializeField] private BoxCollider2D _enemyBox;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _enemyBox = GetComponent<BoxCollider2D>();
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
        if (_leftMove)
        {
            if (transform.position.x > _leftTarget.position.x)
            {
                gameObject.transform.localScale = new Vector3(_x, _y, _z);
                gameObject.transform.Translate(-_speed / 100, 0, 0);
            }
            else
                _leftMove = false;
        }
        else if (!_leftMove)
        {
            if (transform.position.x < _rightTarget.position.x)
            {
                gameObject.transform.localScale = new Vector3(-_x, _y, _z);
                gameObject.transform.Translate(_speed / 100, 0, 0);
            }
            else
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
