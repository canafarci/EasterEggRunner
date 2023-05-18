using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnalyticsManager : MonoBehaviour
{
    Dictionary<string, TimeCounter> timeCounterDict = new Dictionary<string, TimeCounter>();
    string _activeStage;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        //TimeCounter counter = new TimeCounter();
        //timeCounterDict["totalLevel"] = counter;

        //SendStageStartProgressionEvent("totalLevel");
    }

    public void SendStageStartProgressionEvent(string stageName)
    {
        //TimeCounter counter = new TimeCounter();
        //timeCounterDict[stageName] = counter;
        //_activeStage = stageName;

        //Dictionary<string, object> parameters = new Dictionary<string, object>()
        //    {
        //        { "levelName", SceneManager.GetActiveScene().name },
        //        { "stageName", stageName },
        //        { "progressionStatus", "started" },
        //        { "timeSpent", 0f},
        //        { "gameplayLoopIndex", PlayerPrefs.GetInt(PrefKeys.GameplayLoop) }
        //    };

        //Events.CustomData("sceneProgression", parameters);
    }


    public void SendStageEndProgressionEvent(string stageName)
    {
        //float time = timeCounterDict[stageName].EndCounting();

        //TimeCounter counter = timeCounterDict[stageName];
        //timeCounterDict.Remove(stageName);
        //counter = null;

        //Dictionary<string, object> parameters = new Dictionary<string, object>()
        //    {
        //        { "levelName", SceneManager.GetActiveScene().name },
        //        { "stageName", stageName },
        //        { "progressionStatus", "finished" },
        //        {"timeSpent", time},
        //        {"gameplayLoopIndex", PlayerPrefs.GetInt(PrefKeys.GameplayLoop) }
        //    };

        //Events.CustomData("sceneProgression", parameters);
    }

    private void OnApplicationPause(bool pause)
    {
        //if (pause)
        //{
        //    float time = timeCounterDict[_activeStage].EndCounting();

        //    Dictionary<string, object> parameters = new Dictionary<string, object>()
        //    {
        //        { "levelName", SceneManager.GetActiveScene().name },
        //        { "stageName", _activeStage },
        //        { "timeSpent", time },
        //        { "gameplayLoopIndex", PlayerPrefs.GetInt(PrefKeys.GameplayLoop)}
        //    };

        //    Events.CustomData("applicationPauseEvent", parameters);
        //}
    }
}
