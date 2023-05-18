using FluffyUnderware.Curvy.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartMoveEgg : MonoBehaviour
{
    SplineController _follower;
    private void Awake() => _follower = GetComponent<SplineController>();
    private void OnEnable() => GameStart.OnGameStart += OnGameStart;
    private void OnDisable() => GameStart.OnGameStart -= OnGameStart;

    private void OnGameStart() => _follower.Speed = GameManager.Instance.References.GameConfig.PlayerSpeed;


}
