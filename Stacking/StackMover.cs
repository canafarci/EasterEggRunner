using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackMover : MonoBehaviour
{
    List<StackableEgg> _eggList;
    [SerializeField] float _smoothingFactor, _stackForwardSpacing;
    Transform _mainEggTransform;
    Stacker _stacker;

    private void Awake()
    {
        _mainEggTransform = transform.GetChild(0);
        _eggList = new List<StackableEgg>();
        _stacker =  GetComponent<Stacker>();
    }

    private void OnEnable() => _stacker.OnEggListChanged += OnEggListChanged;
    private void OnDisable() => _stacker.OnEggListChanged -= OnEggListChanged;

    private void Update()
    {
        if (_eggList.Count <= 0) return;


        for (int i = 0; i < _eggList.Count; i++)
        {
            if (i == 0)
            {
                _eggList[i].transform.localPosition = Vector3.Lerp(_eggList[i].transform.localPosition,
                new Vector3(_mainEggTransform.localPosition.x, _mainEggTransform.localPosition.y, _mainEggTransform.localPosition.z),
                _smoothingFactor * Time.fixedDeltaTime);
            }
            else
            {
                _eggList[i].transform.localPosition = Vector3.Lerp(_eggList[i].transform.localPosition,
                new Vector3(_eggList[i - 1].transform.localPosition.x, _mainEggTransform.localPosition.y, _mainEggTransform.localPosition.z),
                _smoothingFactor * Time.fixedDeltaTime);
            }
        }
    }



    private void OnEggListChanged(List<StackableEgg> list, bool addedToList)
    {
        _eggList = list;
    }


    public void EmptyStack()
    {
        _eggList.Clear();
    }

}

