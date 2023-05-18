using FluffyUnderware.Curvy.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Stacker : MonoBehaviour
{
    public List<StackableEgg> EggList { get { return _eggList;}}

    List<StackableEgg> _eggList = new List<StackableEgg>();
    public event Action<List<StackableEgg>, bool> OnEggListChanged;

    private void OnEnable() => SpeedTrigger.OnSpeedChange += OnSpeedChange;
    private void OnDisable() => SpeedTrigger.OnSpeedChange -= OnSpeedChange;
    public int AddEggToStack(StackableEgg egg)
    {
        _eggList.Add(egg);

        OnEggListChanged.Invoke(_eggList, true);
        return _eggList.IndexOf(egg);
    }

    public void RemoveEggFromStack(StackableEgg egg, GameObject fx)
    {
        int hitIndex = egg.PositionAtStack;

        for (int i = _eggList.Count - 1; i >= 0; i--)
        {
            if (hitIndex <= i)
            {
                _eggList[i].PositionAtStack = 0;
                _eggList.RemoveAt(i);
            }
        }

        egg.GetComponent<EggFX>().PlayDestructionFX(fx);
        Destroy(egg.transform.parent.parent.parent.gameObject);
        OnEggListChanged.Invoke(_eggList, false);
    }

    private void OnSpeedChange(float speed, float time)
    {
        for (int i = 0; i < _eggList.Count; i++)
        {
            SplineController follower = _eggList[i].transform.GetComponentInParent<SplineController>();
            DOTween.To(() => follower.Speed, x => follower.Speed = x, speed, time);
        }

        SplineController thisFollower = transform.GetComponent<SplineController>();
        DOTween.To(() => thisFollower.Speed, x => thisFollower.Speed = x, speed, time);
    }

    
}
