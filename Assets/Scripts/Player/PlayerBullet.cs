using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private float _lifeTime = 3f;
    private int _damage = 1;
    private float _lifeTimeTimer = 0f;

    private void FixedUpdate()
    {
        _lifeTimeTimer += Time.deltaTime;

        if (_lifeTimeTimer >= _lifeTime)
        {
            _lifeTimeTimer = 0f;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(_damage);
            Destroy(gameObject);
        }
        else if (collision.tag == "Ground")
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            collision.collider.gameObject.GetComponent<EnemyHealth>().TakeDamage(_damage);
            Destroy(gameObject);
        }
        else if (collision.collider.tag == "Ground")
            Destroy(gameObject);
    }
}
