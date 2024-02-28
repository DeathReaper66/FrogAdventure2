using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableMechanic : MonoBehaviour
{
    [SerializeField] private GameObject _mechanicButton;
    [SerializeField] private GameObject _massage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            StartCoroutine(enumerator());
    }

    private IEnumerator enumerator()
    {
        _mechanicButton.SetActive(true);
        _massage.SetActive(true);
        yield return new WaitForSeconds(5);
        _massage.SetActive(false);
        gameObject.SetActive(false);
    }
}
