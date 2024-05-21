using Cinemachine;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayers : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private float _minx, _maxX, _minY, _maxY;
    private List<GameObject> _players = new List<GameObject>();

    private void Start()
    {
        Vector2 randomPos = new Vector2(Random.Range(_minx, _maxY), Random.Range(_minY, _maxY));
      GameObject Player = PhotonNetwork.Instantiate(_player.name, randomPos, Quaternion.identity);
        _players.Add(Player);

        foreach (GameObject player in _players)
        {
            player.SetActive(false);
            player.SetActive(true);
        }
    }
}
