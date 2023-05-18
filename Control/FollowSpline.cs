using FluffyUnderware.Curvy.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FollowSpline : MonoBehaviour
{
    [SerializeField] float _smoothingFactor, _splineFollowSpeed, _xBounds;
    InputReader _inputReader;
    Transform _childTransform;
    SplineController _follower;
    bool _controlRemoved = false;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _childTransform = transform.GetChild(0);
        _follower = GetComponent<SplineController>();
    }

    private void Update()
    {
        if (_controlRemoved) return;

        Vector3 localPos = _childTransform.localPosition;

        localPos.x += _inputReader.XChange;

        if (localPos.x < -_xBounds)
            localPos.x = -_xBounds;
        else if (localPos.x > _xBounds)
            localPos.x = _xBounds;

        _childTransform.localPosition = Vector3.Lerp(_childTransform.localPosition, localPos, _smoothingFactor);
    }

    public void RemoveControlAndAdjustPosition(Vector3 position)
    {
        _controlRemoved = true;
        float localX = transform.InverseTransformPoint(position).x;

        _childTransform.DOLocalMoveX(localX, 0.3f);

    }

    public void RemoveControl()
    {
        _controlRemoved = true;
    }

    public void EnableControl() => _controlRemoved = false;
    public void ChangeController(SplineController follower, Transform followTransform)
    {
        _follower = follower;
        _childTransform = followTransform;
    }
}