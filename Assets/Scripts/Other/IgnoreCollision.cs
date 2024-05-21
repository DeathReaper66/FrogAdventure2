using UnityEngine;

public class IgnoreCollision : MonoBehaviour
{
    [SerializeField] private int _layer1;
    [SerializeField] private int _layer2;

    private void Awake()
    {
        Physics2D.IgnoreLayerCollision(_layer1, _layer2, true);
    }
}
