using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] private Rigidbody2D _playerRb;
    private bool _leftMove = true;
    private Animator _anim;
    private BoxCollider2D _enemyBox;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _enemyBox = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        Physics2D.IgnoreLayerCollision(9,10,true);

        if (Dead())
        {
            _anim.SetTrigger("Die");
            _enemyBox.enabled = false;
            _playerRb.AddForce(Vector2.up * 700);
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
    private bool Dead()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(_enemyBox.bounds.max, _enemyBox.bounds.size, 0, Vector2.up, 0.01f, _playerLayer);
        return raycastHit2D.collider != null;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
