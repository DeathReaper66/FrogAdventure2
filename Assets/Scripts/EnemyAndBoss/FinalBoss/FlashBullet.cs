using UnityEngine;

public class FlashBullet : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _damage = 1f;

    private void FixedUpdate()
    {
        transform.Translate(Mathf.Sign(transform.localScale.x) * _speed / 50f, 0f, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<HealthSystem>().TakeDamage(_damage);
            Destroy(gameObject);
        }
        else if (collision.tag == "Ground")
            Destroy(gameObject);
    }
}
