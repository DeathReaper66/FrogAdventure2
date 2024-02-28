using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkingChicken : MonoBehaviour
{
    [SerializeField] private GameObject[] _texts;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(enumerator());
            Destroy(gameObject.GetComponent<BoxCollider2D>());
        }
    }

    private IEnumerator enumerator()
    {
        _texts[0].gameObject.SetActive(true);
        yield return new WaitForSeconds(4);

        _texts[0].SetActive(false);
        _texts[1].gameObject.SetActive(true);
        yield return new WaitForSeconds(4);

        _texts[1].SetActive(false);
        _texts[2].gameObject.SetActive(true);
        yield return new WaitForSeconds(4);

        _texts[2].SetActive(false);
        _texts[3].gameObject.SetActive(true);
        yield return new WaitForSeconds(4);

        _texts[3].SetActive(false);
        _texts[4].gameObject.SetActive(true);
        yield return new WaitForSeconds(4);

        _texts[4].SetActive(false);
        _texts[5].gameObject.SetActive(true);
        yield return new WaitForSeconds(4);
        _texts[5].SetActive(false);
    }
}
