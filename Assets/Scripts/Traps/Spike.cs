using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField] private float _damage = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            collision.GetComponent<HealthSystem>().TakeDamage(_damage);
    }
}
