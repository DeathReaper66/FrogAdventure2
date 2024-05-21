using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RunoBoss : MonoBehaviour
{
    [Header("MainParams")]
    [SerializeField] private float _speed;
    [SerializeField] private float _attackCooldown;
    [SerializeField] private float _jumpForce = 100000f;
    [SerializeField] private float _health = 10f;
    [SerializeField] private Image _healthImage;
    [Header("ComponentsAndRock")]
    [SerializeField] private BoxCollider2D _box;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private float _distance;
    [SerializeField] private GameObject _rock;
    [SerializeField] private Transform _transformForRock;
    [SerializeField] private BoxCollider2D _wallCollider;
    [SerializeField] private GameObject _platform;
    private Animator _anim;
    private bool _attacking = false;
    private float _attackTimer = 0f;
    private float _attackValue = 1f;
    [SerializeField] private float _rockTimeOfLife = 4f;
    private float _rockLifeTimer = 0f;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        _attackTimer += Time.deltaTime;

        if (_rock.activeInHierarchy)
        {
            _rockLifeTimer += Time.deltaTime;

            if (_rockLifeTimer >= _rockTimeOfLife)
                _rock.SetActive(false);
        }

        if (PlayerInSight())
            _attacking = true;

        if (_attacking && _attackTimer >= _attackCooldown)
        {
            if (_attackValue == 1f)
            {
                _anim.SetTrigger("Attack");
                _attackValue = 0f;
            }

            transform.Translate(_speed * -transform.right * Mathf.Sign(transform.localScale.x) / 10);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            _rock.transform.position = _transformForRock.position;
            _rock.SetActive(true);
            _attacking = false;
            _attackTimer = 0f;
            _rockLifeTimer = 0f;
            _attackValue = 1f;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            _anim.SetTrigger("WallHit");

            if (_health < 6f)
            {
                StartCoroutine(Jump());

            }
        }

        if (collision.collider.tag == "Rock")
        {
            collision.collider.gameObject.SetActive(false);

            _healthImage.fillAmount = _health / 10;

            if (_health == 0)
            {
                _anim.SetTrigger("Die");
                _speed = 0f;
            }
            else
            {
                _anim.SetTrigger("Hit");
                _health--;
            }
        }
    }

    private IEnumerator Jump()
    {
        _platform.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        GetComponent<Rigidbody2D>().AddForce(new Vector2(GetComponent<Rigidbody2D>().velocity.x, _jumpForce));
        yield return new WaitForSeconds(2f);
        _platform.SetActive(false);
    }

    private bool PlayerInSight()
    {
        RaycastHit2D _hit = Physics2D.BoxCast(_box.bounds.center - transform.right * _distance * 2.5f * Mathf.Sign(transform.localScale.x),
        new Vector3(_box.bounds.size.x * _distance, _box.bounds.size.y, _box.bounds.size.z), 0, Vector2.left, 0f, _playerLayer);

        return _hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_box.bounds.center - transform.right * _distance * 2.5f * Mathf.Sign(transform.localScale.x),
            new Vector3(_box.bounds.size.x * _distance, _box.bounds.size.y, _box.bounds.size.z));
    }

    public void Destroy()
    {
        Destroy(gameObject);
        _wallCollider.enabled = true;
    }
}
