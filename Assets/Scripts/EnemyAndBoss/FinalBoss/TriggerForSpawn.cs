using UnityEngine;

public class TriggerForSpawn : MonoBehaviour
{
    [SerializeField] private GameObject[] _bosses;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (GameObject boss in _bosses)
            boss.SetActive(true);

        gameObject.SetActive(false);
    }
}

