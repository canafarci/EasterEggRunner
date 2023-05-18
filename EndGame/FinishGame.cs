using DG.Tweening;
using FluffyUnderware.Curvy.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishGame : MonoBehaviour
{
    SplineController _follower;
    Animator _animator;
    [SerializeField] GameObject _fx, _basket;

    private void Awake()
    {
        _follower = GetComponent<SplineController>();
        _animator = GetComponentInChildren<Animator>();
    }

    public void FinishGameBehaviour()
    {
        _follower.Speed = 0f;
        transform.GetChild(0).DOLocalRotate(new Vector3(0f, -180f, 0f), .5f);
        _animator.Play("Dance");
        _fx.SetActive(true);
        _basket.SetActive(false);
        GameManager.Instance.CameraController.ActivateCamera(CameraStrings.SecondCameraFinal);
        StartCoroutine(DelayedLoadNextLevel());

    }

    IEnumerator DelayedLoadNextLevel()
    {
        yield return new WaitForSeconds(4f);
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (sceneIndex == 0)
            SceneManager.LoadScene(1);
        else if (sceneIndex == 1)
            SceneManager.LoadScene(0);
    }
}
