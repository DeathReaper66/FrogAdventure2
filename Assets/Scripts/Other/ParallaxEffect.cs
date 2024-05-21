using System;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] private Transform _followingTarget;
    [SerializeField, Range(0f, 1f)] private float _parralaxStranght;
    [SerializeField] private bool _disableVerticalParallax;
    private Vector3 _targetPreviousPosition;

    private void Awake()
    {
        _targetPreviousPosition = _followingTarget.position;
    }

    private void FixedUpdate()
    {
        var deltaVector3 = _followingTarget.position - _targetPreviousPosition;

        if (_disableVerticalParallax)
            deltaVector3.y = 0;

        _targetPreviousPosition = _followingTarget.position;

        transform.position += deltaVector3 * _parralaxStranght;
    }
}
