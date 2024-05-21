using UnityEngine;

public class Pig : MonoBehaviour
{
    [SerializeField] private GameObject _bossEnableTrigger;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Bullet")
        {
            PlayerPrefs.SetInt("Multiplay", 1);
            Destroy(collision.gameObject);
            GetComponent<Animator>().SetTrigger("Die");
        }
    }

    public void Die()
    {
        _bossEnableTrigger.SetActive(true);
        Destroy(gameObject);
    }
}
