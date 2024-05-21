using UnityEngine;

public class RefreshEnemy : MonoBehaviour
{
    [SerializeField] private float _timeOfRespawn;
    [SerializeField] private GameObject _enemy;
    private float _timer = 0f;

    private void Update()
    {
        if (!_enemy.activeInHierarchy)
        {
            _timer += Time.deltaTime;

            if (_timer > _timeOfRespawn)
            {
                _enemy.SetActive(true);
                _timer = 0f;
            }
        }
    }
}
