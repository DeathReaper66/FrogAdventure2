using UnityEngine;

public class DestroyBosses : MonoBehaviour
{

    [SerializeField] private GameObject _bossUI;

    public void DestroyUIBoss()
    {
        _bossUI.SetActive(false);
    }

    public void SelfDisable()
    {
        gameObject.SetActive(false);
    }
}
