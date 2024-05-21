using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Maks : MonoBehaviour
{
    public static float PortalSpawn = 0f;

    [Header("LocalScale")]
    [SerializeField] private float _x = 15;
    [SerializeField] private float _y = 15;
    [SerializeField] private float _z = 15;
    [Header("Params")]
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _damage = 1f;

    [SerializeField] private float _distanceForVison = 1f;

    [SerializeField] private float _distanceForJumpAttack = 1f;
    [SerializeField] private float _dashForce = 10f;
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private float _jumpAttackCooldown = 4f;
    [SerializeField] private float _frameForDashAttackInSec = 0.5f;
    private float _jumpAttackTimer = 0f;

    [SerializeField] private float _distanceForBack = 1f;

    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private BoxCollider2D _box;

    [SerializeField] private Image _healthImage;
    [SerializeField] private GameObject _portal;
    private int _dashCount = 1;
    private bool _firstPhase = true;
    private bool _fhirdPhase = false;

    private float _maxHealth;

    private Animator _anim;
    private Rigidbody2D _body;
    private RaycastHit2D _hit;
    private RaycastHit2D _hit2;
    private RaycastHit2D _hit3;

    private EnemyHealth _enemyHealth;

    private bool _canRun = false;

    private void Awake()
    {
        PortalSpawn = 0f;

        _anim = GetComponent<Animator>();
        _box = GetComponent<BoxCollider2D>();
        _body = GetComponent<Rigidbody2D>();
        _enemyHealth = GetComponent<EnemyHealth>();

        _maxHealth = _enemyHealth._health;
    }

    private void FixedUpdate()
    {
        _healthImage.fillAmount = _enemyHealth._health / _maxHealth;

        if (_enemyHealth._health <= (_maxHealth - 3) && _firstPhase)
        {
            _firstPhase = false;
        }
        else if (_enemyHealth._health <= (_maxHealth - 6) && !_fhirdPhase)
        {
            _fhirdPhase = true;
            _speed += 2f;
            _jumpForce += 2f;
            _dashForce += 2f;
        }

        _jumpAttackTimer += Time.deltaTime;

        if (OnHead() && _firstPhase || !RayForMove() && _firstPhase)
            _canRun = false;

        if (OnHead() && _dashCount == 1f)
        {
            StartCoroutine(Dash());
            _dashCount = 0;
        }

        if (RayForJumpAttack() && _jumpAttackTimer >= _jumpAttackCooldown)
        {
            StartCoroutine(JumpAttack());
            _jumpAttackTimer = 0f;
        }
        else if (!OnHead())
            _canRun = true;

        if (RayForMove() && _canRun)
            transform.Translate(Mathf.Sign(transform.localScale.x) * Vector3.right * _speed / 50f);

        if (PlayerInBackSight())
        {

            if (transform.localScale.x == _x)
                transform.position = new Vector2(transform.position.x, transform.position.y);
            else
                transform.position = new Vector2(transform.position.x, transform.position.y);

            transform.localScale = new Vector3(-transform.localScale.x, _y, _z);
        }

        _anim.SetBool("Run", _canRun);
    }

    private IEnumerator JumpAttack()
    {
        _anim.SetTrigger("Jump");
        _body.velocity = new Vector2(_jumpForce * Mathf.Sign(transform.localScale.x), _jumpForce);
        yield return new WaitForSeconds(_frameForDashAttackInSec);
    }

    private IEnumerator Dash()
    {
        _anim.SetTrigger("Hit");
        _body.velocity = new Vector2(_dashForce * 2f * Mathf.Sign(transform.localScale.x), _body.velocity.y);
        yield return new WaitForSeconds(0.5f);
        _dashCount = 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            collision.GetComponent<HealthSystem>().TakeDamage(_damage);
    }

    private bool RayForMove()
    {
        _hit = Physics2D.BoxCast(_box.bounds.center + transform.right * _distanceForVison * Mathf.Sign(transform.localScale.x) * 1.2f,
        new Vector3(_box.bounds.size.x * _distanceForVison, _box.bounds.size.y * _distanceForVison, _box.bounds.size.z), 0, Vector2.left, 0f, _playerLayer);
        return _hit.collider != null;
    }

    private bool RayForJumpAttack()
    {
        _hit2 = Physics2D.BoxCast(_box.bounds.center + transform.right * _distanceForJumpAttack * Mathf.Sign(transform.localScale.x) * 1.2f,
        new Vector3(_box.bounds.size.x * _distanceForJumpAttack, _box.bounds.size.y / 2f, _box.bounds.size.z), 0, Vector2.left, 0f, _playerLayer);
        return _hit2.collider != null;
    }

    private bool PlayerInBackSight()
    {
        _hit3 = Physics2D.BoxCast(_box.bounds.center - transform.right * _distanceForBack * Mathf.Sign(transform.localScale.x) * 1.2f,
        new Vector3(_box.bounds.size.x * _distanceForBack, _box.bounds.size.y * _distanceForBack, _box.bounds.size.z), 0, Vector2.left, 0f, _playerLayer);
        return _hit3.collider != null;
    }

    private bool OnHead()
    {
        RaycastHit2D _ray = Physics2D.BoxCast(_box.bounds.center, new Vector3(_box.bounds.size.x / 2f, _box.bounds.size.y + 1.5f, _box.bounds.size.z), 0, Vector2.up, 0f, _playerLayer);
        return _ray.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_box.bounds.center + transform.right * _distanceForVison * Mathf.Sign(transform.localScale.x) * 1.2f,
        new Vector3(_box.bounds.size.x * _distanceForVison, _box.bounds.size.y * _distanceForVison, _box.bounds.size.z));

        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(_box.bounds.center + transform.right * _distanceForJumpAttack * Mathf.Sign(transform.localScale.x) * 1.2f,
        new Vector3(_box.bounds.size.x * _distanceForJumpAttack, _box.bounds.size.y / 2f, _box.bounds.size.z));

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(_box.bounds.center - transform.right * _distanceForBack * Mathf.Sign(transform.localScale.x) * 1.2f,
        new Vector3(_box.bounds.size.x * _distanceForBack, _box.bounds.size.y * _distanceForBack, _box.bounds.size.z));

        Gizmos.DrawWireCube(_box.bounds.center, new Vector3(_box.bounds.size.x / 2f, _box.bounds.size.y + 1.5f, _box.bounds.size.z));
    }

    public void EarlyDead()
    {
        PortalSpawn++;
        _playerLayer = 1;

        if (PortalSpawn == 2f)
            _portal.SetActive(true);
    }
}
