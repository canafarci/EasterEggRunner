using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] protected GameObject _fx;
    Stacker _stacker;
    public static event Action<int> OnEggHitObstacle;

    private void Awake() => _stacker = FindObjectOfType<Stacker>();
    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnPlayerEnterObstacle(other);
        }
    }

    protected virtual void OnPlayerEnterObstacle(Collider other)
    {
        StackableEgg stackableEgg = other.transform.GetComponent<StackableEgg>();
        if (stackableEgg == null) return;

        OnEggHitObstacle?.Invoke(stackableEgg.PositionAtStack);
        _stacker.RemoveEggFromStack(stackableEgg, _fx);
        //GetComponent<Collider>().enabled = false;
    }
}