using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using FluffyUnderware.Curvy.Controllers;
using System;

public class EndGameTrigger : MonoBehaviour
{
    [SerializeField] Transform _targetTransform;
    SplineController _follower, _mainFollower;
    EndGameStacker _endGameStacker;
    Transform _parentTransform;

    public static event Action OnEndGameReach;
    private void Awake()
    {
        _endGameStacker = FindObjectOfType<EndGameStacker>();
        _follower = GetComponentInParent<SplineController>();
        _mainFollower = FindObjectOfType<Stacker>().transform.GetComponent<SplineController>();
        _parentTransform = _mainFollower.transform;
    }

    private void OnEnable() => EndGameTrigger.OnEndGameReach += OnEndGameReached;
    private void OnDisable() => EndGameTrigger.OnEndGameReach -= OnEndGameReached;
    public void OnEndGameReachedTrigger()
    {
        OnEndGameReach?.Invoke();
        _mainFollower.Speed = 0f;
        _mainFollower.enabled = false;
        StartCoroutine(EndGameStackRoutine());
    }

    private void OnEndGameReached()
    {
        _follower.enabled = false;
        _follower.Speed = 0f;
    }

    IEnumerator EndGameStackRoutine()
    {
        Transform mainEggTransform = _parentTransform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform;
        _endGameStacker.Stack.Push(mainEggTransform);
        mainEggTransform.parent = null;

        foreach (StackableEgg egg in _parentTransform.GetComponent<Stacker>().EggList)
        {
            _endGameStacker.Stack.Push(egg.transform);
            egg.transform.parent = null;
            print("loop");
        }

        FindObjectOfType<StackMover>().EmptyStack();

        yield return new WaitForSeconds(0.1f);

        foreach (Transform tr in _endGameStacker.Stack)
        {
            Vector3 endPos = _targetTransform.position;

            Vector3 intermediatePos = new Vector3((endPos.x + tr.position.x) / 2f, endPos.y + 3f, (endPos.z + tr.position.z) / 2f);
            Vector3[] path = { tr.position, intermediatePos, endPos };

            print(endPos + " " + intermediatePos + " " + tr.localPosition);

            tr.DOPath(path, 0.5f, PathType.Linear, PathMode.Full3D);
            tr.DOScale(new Vector3(0.2f, 0.2f, 0.2f), 0.37f);

            yield return new WaitForSeconds(0.5f);
            _endGameStacker.Fx.Play();
        }
        

        foreach (Transform tr in _endGameStacker.Stack)
        {
            tr.parent = _targetTransform;
        }

        FindObjectOfType<StartEndGame>().StartEndGameBehaviour();
        foreach (GiftEggTrigger get in FindObjectsOfType<GiftEggTrigger>())
        {
            get.SetEndStack();
        }
    }


























    //public void StackItem()
    //{
    //    item.transform.parent = transform;
    //    Vector3 endPos = _targetTransform.position;
    //    Vector3 intermediatePos = new Vector3(endPos.x / 2f, endPos.y + 1f, endPos.z / 2f);
    //    Vector3[] path = { intermediatePos, endPos };

    //    item.transform.DOLocalPath(path, .5f, PathType.CatmullRom, PathMode.Full3D);
    //    item.transform.DOLocalRotate(new Vector3(-90f, -180f, item.ItemTargetRotation), 0.5f);
    //    StartCoroutine(DotweenFX(item));
    //    _stack.Push(transform);
    //}

    //IEnumerator DotweenFX(StackableItem item)
    //{
    //    yield return new WaitForSeconds(0.5f);
    //    item.transform.DOPunchScale(new Vector3(10f, 20f, 10f), 0.3f);
    //}
}
