using UnityEngine;

public class StayAtButton : MonoBehaviour
{
    [SerializeField] private GameObject _wall;
    [SerializeField] private float _frame = 0.01f;
    [SerializeField] private float _minWallY;
    [SerializeField] private float _maxWallY;
    private float _minButtonY;
    private float _maxButtonY;
    private bool _work = false;

    private void Awake()
    {
        _maxButtonY = gameObject.transform.position.y;
        _minButtonY = _maxButtonY - 0.1f;
    }

    private void FixedUpdate()
    {
        if (!_work && _wall.transform.position.y > _minWallY)
            _wall.transform.position -= new Vector3(0f, _frame, 0);
        else if (_work && _wall.transform.position.y < _maxWallY)
            _wall.transform.position += new Vector3(0f, _frame, 0);

        if (!_work && gameObject.transform.position.y < _maxButtonY)
            gameObject.transform.position += new Vector3(0f, _frame / 10, 0);
        else if (_work && gameObject.transform.position.y > _minButtonY)
            gameObject.transform.position -= new Vector3(0f, _frame / 10, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
            _work = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _work = false;
    }
}
