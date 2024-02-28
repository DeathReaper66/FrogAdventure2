using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnableOrDisable : MonoBehaviour
{
    [SerializeField] private GameObject _obj;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (_obj.activeInHierarchy)
                _obj.SetActive(false);
            else
                _obj.SetActive(true);

            Destroy(gameObject);
        }

    }
}
