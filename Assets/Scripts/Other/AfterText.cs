using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterText : MonoBehaviour
{
    [SerializeField] private GameObject _playerAndStatics;
    [SerializeField] private GameObject _awakePanel;

    private void Awake()
    {
        StartCoroutine(enumerator());
    }

    private IEnumerator enumerator()
    {
        yield return new WaitForSeconds(5.5f);
        _awakePanel.SetActive(true);
        _playerAndStatics.SetActive(true);
        gameObject.SetActive(false);
    }
}
