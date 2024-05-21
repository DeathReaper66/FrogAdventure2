using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] public int _health = 1;
    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
            _anim.SetTrigger("Die");
        else
            _anim.SetTrigger("Hit");
    }

    public void DisableEnemy()
    {
        gameObject.SetActive(false);
    }
}
