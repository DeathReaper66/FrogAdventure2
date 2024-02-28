using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrampolineScene3 : MonoBehaviour
{
    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _anim.SetTrigger("Active");
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
