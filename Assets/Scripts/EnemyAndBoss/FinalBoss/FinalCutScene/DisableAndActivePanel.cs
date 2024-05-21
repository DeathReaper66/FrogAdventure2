using UnityEngine;

public class DisableAndActivePanel : MonoBehaviour
{
    [SerializeField] private GameObject[] _panels;
    [SerializeField] private float _timeForDisablePanel = 5f;
    [SerializeField] private int _num = 0;
    [SerializeField] private bool _canDisableSelf;
    private float _timer = 0f;
    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        _timer += Time.deltaTime;

        if (_timer >= _timeForDisablePanel)
        {
            _timer = 0f;

            if (_canDisableSelf)
                _anim.SetInteger("Disable", _num);

            if (_panels != null)
            {
                foreach (GameObject panel in _panels)
                    panel.SetActive(true);
            }
        }
    }
}
