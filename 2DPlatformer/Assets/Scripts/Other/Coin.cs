using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private TMP_Text _textMeshPro;
    public static int NumberOfCoin; 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            NumberOfCoin++;
            Destroy(gameObject);

            _textMeshPro.text = NumberOfCoin.ToString();
        }
    }

    private void Awake()
    {
        NumberOfCoin = PlayerPrefs.GetInt("Coin");
        _textMeshPro.text = NumberOfCoin.ToString();
    }
}
