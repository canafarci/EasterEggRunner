using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public SceneLoad SceneLoader { get { return _sceneLoader; } }
    public References References { get { return _references; } }
    public CameraController CameraController { get { return _cameraController;} }
    public AudioPlayer AudioManager { get { return _audioManager;} }
    public ResourceManager ResourceManager { get { return _resourceManager; } }
    public Fader Fader { get { return _fader; } }
    public AnalyticsManager AnalyticsManager { get { return _analyticsManager; } }

    SceneLoad _sceneLoader;
    GameStart _gameStart;
    References _references;
    CameraController _cameraController;
    AudioPlayer _audioManager;
    ResourceManager _resourceManager;
    Fader _fader;
    AnalyticsManager _analyticsManager;

    public static GameManager Instance { get; private set; }
    void Awake()
    {
        transform.parent = null;

        if (Instance)
            Destroy(gameObject);
        else
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }

        _resourceManager = GetComponent<ResourceManager>();
        _sceneLoader = GetComponent<SceneLoad>();
        _gameStart = GetComponent<GameStart>(); 
        _references = GetComponent<References>();
        _cameraController = GetComponent<CameraController>();
        _audioManager = GetComponentInChildren<AudioPlayer>();
        _fader = GetComponent<Fader>();
        _analyticsManager = GetComponent<AnalyticsManager>();
    }

    private void OnEnable() => SceneManager.activeSceneChanged += OnSceneLoaded;
    private void OnDisable() => SceneManager.activeSceneChanged -= OnSceneLoaded;

    private void OnSceneLoaded(Scene arg0, Scene arg1)
    {
        _gameStart.enabled = true;
    }
}
