using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StackFX : MonoBehaviour
{
    [SerializeField] float _scaleTweenDuration = 0.075f;
    Stacker _stacker;
    private void Awake() => _stacker = GetComponent<Stacker>();
    private void OnEnable() => _stacker.OnEggListChanged += OnEggListChanged;
    private void OnDisable() => _stacker.OnEggListChanged -= OnEggListChanged;
    private void OnEggListChanged(List<StackableEgg> list, bool addedToList)
    {
        if (!addedToList) return;

        Sequence sequence = DOTween.Sequence();
        

        for (int i = list.Count - 1; i >= 0; i--)
        {
            sequence.Append(list[i].transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), _scaleTweenDuration));
            sequence.Append(list[i].transform.DOScale(new Vector3(1f, 1f, 1f), _scaleTweenDuration));
        }

        sequence.Append(transform.GetChild(0).GetChild(0).GetChild(0).DOScale(new Vector3(1.5f, 1.5f, 1.5f), _scaleTweenDuration));
        sequence.Append(transform.GetChild(0).GetChild(0).GetChild(0).DOScale(new Vector3(1f, 1f, 1f), _scaleTweenDuration));
    }
}
