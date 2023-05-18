using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using FluffyUnderware.Curvy.Controllers;

public class StartEndGame : MonoBehaviour
{
    SplineController _follower;
    Animator _animator;

    private void Awake()
    {
        _follower = GetComponent<SplineController>();
        _animator = GetComponentInChildren<Animator>();
    }
    public void StartEndGameBehaviour()
    {
        StartCoroutine(EndGame());
    }

    IEnumerator EndGame()
    {
        GameManager.Instance.CameraController.ActivateCamera(CameraStrings.SecondCamera);

        Tween tween1 = transform.DOLocalRotate(new Vector3(0f, 180f, 0f), .5f);
        yield return tween1.WaitForCompletion();

        _animator.Play("Walk");
        FindObjectOfType<FollowSpline>().ChangeController(_follower, transform.GetChild(0));
        _follower.Speed = GameManager.Instance.References.GameConfig.EndGameSpeed;

    }
}
