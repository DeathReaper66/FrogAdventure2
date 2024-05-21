using TMPro;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private TMP_Text _textMeshPro;
    [SerializeField] private int _coinNumber; 
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

        if (_coinNumber <= PlayerPrefs.GetInt("Coin"))
            gameObject.SetActive(false);
    }
}
