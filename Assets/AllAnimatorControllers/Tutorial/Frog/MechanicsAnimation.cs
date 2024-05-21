using UnityEngine;

public class MechanicsAnimation : MonoBehaviour
{
    [SerializeField] private int _animIndex;
    private Animator _anim;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        _anim.SetInteger("Index", _animIndex);
    }
}
