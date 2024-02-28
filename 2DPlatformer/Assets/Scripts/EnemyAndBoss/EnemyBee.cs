using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class EnemyBee : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _speed = 1;
    private float _attackTimer = 0;

    [Header("BoxCast")]
    [SerializeField] private float _distance = 1;
    [SerializeField] private float _distance2 = 1;
    [SerializeField] private BoxCollider2D _box;
    [SerializeField] private LayerMask _playerLayer;
    private Animator _anim;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            collision.GetComponent<HealthSystem>().TakeDamage(_damage);
    }

    private void Update()
    {
        _attackTimer += Time.deltaTime;

        if (PlayerInSight() && _attackTimer > 2f)
        {
            _anim.SetTrigger("Attack");
            StartCoroutine(enumerator());
            _attackTimer = 0;
        }
    }

    private bool PlayerInSight()
    {
        RaycastHit2D _ray = Physics2D.BoxCast(_box.bounds.center - transform.up * _distance * _distance2,
            new Vector3(_box.bounds.size.x, _box.bounds.size.y * _distance, _box.bounds.size.z), 0, Vector2.down, _playerLayer);

        return _ray.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_box.bounds.center - transform.up * _distance * _distance2,
            new Vector3(_box.bounds.size.x, _box.bounds.size.y * _distance, _box.bounds.size.z));
    }

    private void Attack()
    {
        transform.Translate(0, -_speed / 100, 0);
    }
    private void GoBack()
    {
        transform.Translate(0, _speed / 100, 0);
    }

    private IEnumerator enumerator()
    {
        InvokeRepeating(nameof(Attack), 0.05f, 20);
        yield return new WaitForSeconds(1f);
        InvokeRepeating(nameof(GoBack), 0.05f, 20);
    }
}
