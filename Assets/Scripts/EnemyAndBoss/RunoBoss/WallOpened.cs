using UnityEngine;

public class WallOpened : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GetComponent<Animator>().SetTrigger("Active");
        }
    }
}
