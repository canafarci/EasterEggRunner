using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using FluffyUnderware.Curvy.Controllers;
using System;
using Random = UnityEngine.Random;

public class StackableEgg : MonoBehaviour
{
    public int PositionAtStack;

    [SerializeField] float _frontMovingDistance = 0.01f;
    [SerializeField] SplineController _mainController;

    Collider _collider;
    Stacker _stacker;
    SplineController _thisFollower;
    Vector3 _startlocalRotation;
    EggAnimation _eggAnimation;
    Rigidbody _rigidbody;
    Transform _parent;


    private void Awake()
    {
        _parent = transform.parent.parent.parent;
        _stacker = FindObjectOfType<Stacker>();
        _collider = GetComponent<Collider>();
        _thisFollower = GetComponentInParent<SplineController>();
        _eggAnimation = GetComponentInParent<EggAnimation>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable() => Obstacle.OnEggHitObstacle += OnEggHitObstacle;
    private void OnDisable() => Obstacle.OnEggHitObstacle -= OnEggHitObstacle;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!_collider.isTrigger) return;

            _thisFollower.enabled = true;
            _collider.isTrigger = false;
            PositionAtStack = _stacker.AddEggToStack(this);
            _eggAnimation.ChangeAnimation(true);
            PositionEgg();
        }
    }

    void PositionEgg()
    {
        transform.DOLocalRotate(_startlocalRotation, 0.2f);
        transform.DOLocalMoveY(0f, 0.2f);
        transform.DOLocalMoveZ(0f, 0.2f);

        _thisFollower.Position = (_mainController.Position + _frontMovingDistance + (_frontMovingDistance * PositionAtStack));
        _thisFollower.Speed = GameManager.Instance.References.GameConfig.PlayerSpeed;
    }


    private void OnEggHitObstacle(int hitIndex)
    {
        if (PositionAtStack > hitIndex)
        {           
            StartCoroutine(OnHitObstacle());
        }
    }

    IEnumerator OnHitObstacle()
    {
        //_thisFollower.Speed = 0f;
        //float delay = .5f;
        //_parent.DOJump(_parent.position + new Vector3(Random.Range(.5f, 1.5f), 0f, Random.Range(3f, 6f)),
        //    2f,
        //    1,
        //    delay
        //    );

        //yield return new WaitForSeconds(delay - 0.01f);
        //Vector3 pos = _parent.position;
        //Vector3 oldLocalPos = transform.localPosition;
        //float nearestPos = _thisFollower.Spline.GetNearestPointTF(pos, Space.World);

        //yield return new WaitForSeconds(0.01f);

        //_thisFollower.RelativePosition = nearestPos;
        //yield return new WaitForSeconds(0.01f);
        //transform.position = pos;
        //transform.localPosition = oldLocalPos;
        //_collider.isTrigger = true;


        _eggAnimation.ChangeAnimation(false);
        _thisFollower.Speed = 0f;
        _thisFollower.enabled = false;
        float delay = 0.5f;
        
        _parent.DOJump(_parent.position + new Vector3(Random.Range(-2.5f, 2.5f), 0f, Random.Range(8f, 12f)),
            2f,
            1,
            delay
            );

        yield return new WaitForSeconds(delay);
        _collider.isTrigger = true;


        //float nearestTF = _mainController.Spline.GetNearestPointTF(transform.position, Space.World);
        //// set the corresponding position to nearestTF
        //transform.position = _mainController.Spline.Interpolate(nearestTF, Space.World);

    }
}
