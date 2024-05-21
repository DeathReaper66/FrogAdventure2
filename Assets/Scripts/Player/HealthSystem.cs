using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private Image _currenthealth;
    [SerializeField] private DeathMenu _deathMenu;
    private Animator _anim;
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        Physics2D.IgnoreLayerCollision(8, 10, false);
        _anim = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(float _damage)
    {
        _currenthealth.fillAmount -= Mathf.Clamp(_damage / 10, 0, 1);

        StartCoroutine(Invulnerability());

        if (_currenthealth.fillAmount <= 0)
        {
            Death();
        }
        else
            _anim.SetTrigger("hurt");
    }

    public void Healing(float _healthValue)
    {
        _currenthealth.fillAmount += Mathf.Clamp(_healthValue / 10, 0, 1);
    }

    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(8, 10, true);
        yield return new WaitForSeconds(1f);
        Physics2D.IgnoreLayerCollision(8, 10, false);
    }

    public void Death()
    {
        _anim.SetTrigger("die");
        _rigidbody2D.gravityScale = 0f;
    }

    //for die animation
    public void EnableDeathMenu()
    {
        Destroy(gameObject);
        _deathMenu.ShowDeathMenu();
    }
}
