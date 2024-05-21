using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _bulletPoint;
    [SerializeField] private float _attackCooldown = 3f;
    private float _attackTimer = 0f;

    private void FixedUpdate()
    {
        _attackTimer += Time.deltaTime;

        if (Input.GetKey(KeyCode.Q))
            Attack();
    }

    public void Attack()
    {
        if (_attackTimer >= _attackCooldown)
        {
            _attackTimer = 0f;
            GameObject _newBullet = Instantiate(_bullet);
            _newBullet.SetActive(true);

            if (gameObject.transform.localScale.x < 0)
                _newBullet.transform.localScale = new Vector3(-_bulletPoint.localScale.x, _bulletPoint.localScale.y, _bulletPoint.localScale.z);
            else
                _newBullet.transform.localScale = _bulletPoint.localScale;

            _newBullet.transform.position = _bulletPoint.position;
            _newBullet.transform.rotation = _bulletPoint.rotation;
        }
    }
}
