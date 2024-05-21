using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [Header("Включаемые объекты")]
    [SerializeField] private List<GameObject> _enableObjects;
    [Header("Выключаемые объекты")]
    [SerializeField] private List<GameObject> _disableObjects;
    [Header("Player, Camera And Value")]
    [SerializeField] private bool _canNewValue = false;
    [SerializeField] private GameObject _player;
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private float _checkpoint;

    private void Awake()
    {
        if (PlayerPrefs.GetFloat("Checkpoint") != 0 && _canNewValue)
        {
            _player.transform.position = transform.position;
            _camera.Follow = _player.transform;

            foreach (GameObject enableObj in _enableObjects)
                enableObj.SetActive(true);

            foreach (GameObject disableObj in _disableObjects)
                disableObj.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerPrefs.SetFloat("Checkpoint", _checkpoint);
        }
    }

}
