using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GiftEggTrigger : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] int _eggsLeftToStack;
    
    [SerializeField] Slider _slider1, _slider2;
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] bool _isLeftTrigger;

    Stack<Transform> _stack;
    FollowSpline _follower;

    private void Awake()
    {
        _follower = FindObjectOfType<FollowSpline>();
    }

    public void SetEndStack()
    {
        _stack = FindObjectOfType<EndGameStacker>().Stack;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Child"))
        {
            StartCoroutine(PositionEggs());
        }
    }

    IEnumerator PositionEggs()
    {
        GetComponent<Collider>().enabled = false;
        _follower.RemoveControl();
        PauseChild pauser = FindObjectOfType<PauseChild>();
        float eggDelay = 1.2f;

        pauser.PauseMovement();
        if (_isLeftTrigger)
            GameManager.Instance.CameraController.ActivateCamera(CameraStrings.SecondCameraLeft);
        else
            GameManager.Instance.CameraController.ActivateCamera(CameraStrings.SecondCameraRight);

        yield return new WaitForSeconds(eggDelay);


        for (int i = 0; i < _eggsLeftToStack; i++)
        {
            print(_stack.Count);

            if (!(_stack.Count > 0))
            {
                FindObjectOfType<FinishGame>().FinishGameBehaviour();
                yield break;
            }

            Transform tr = _stack.Pop();
            tr.parent = null;


            Vector3 endPos = _target.position;

            Vector3 intermediatePos = new Vector3((endPos.x + tr.position.x) / 2f, endPos.y + 10f, (endPos.z + tr.position.z) / 2f);
            Vector3[] path = { tr.position, intermediatePos, endPos };

            

            tr.DOPath(path, eggDelay, PathType.Linear, PathMode.Full3D);
            tr.DOScale(new Vector3(1f, 1f, 1f), eggDelay / 2f);

            //_slider1.DOValue((float)(i + 1) / (float)_eggsLeftToStack, .5f);
            //_slider2.DOValue((float)(i + 1) / (float)_eggsLeftToStack, .5f);
            transform.DOPunchScale(new Vector3(.004f, .004f, .004f), .5f);
            yield return new WaitForSeconds(.5f);
            _slider1.value = ((float)(i + 1) / (float)_eggsLeftToStack);
            _slider2.value = ((float)(i + 1) / (float)_eggsLeftToStack);
            
            _text.text = (i + 1 ).ToString() + "/" + _eggsLeftToStack.ToString();

            Vector3 scaleFactor = new Vector3(0.25f, 0.25f, 0.25f);
            Vector3 parentScale = _target.transform.parent.localScale;
            _target.DOPunchScale(new Vector3(scaleFactor.x  / parentScale.x, scaleFactor.y / parentScale.y, scaleFactor.z / parentScale.z), .5f, 1);
            
        }

        if (_stack.Count == 0)
        {
            FindObjectOfType<FinishGame>().FinishGameBehaviour();
            yield break;
        }
        


        GameManager.Instance.CameraController.ActivateCamera(CameraStrings.SecondCamera);
        yield return new WaitForSeconds(.75f);
        pauser.ContinueMovement();
        _follower.EnableControl();
    }
}
