using UnityEngine;

public class HealingHeart : MonoBehaviour
{
   [SerializeField] private float _value = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<HealthSystem>().Healing(_value);
            gameObject.SetActive(false);
        }
    }
}
